using iTextSharp.text;
using iTextSharp.text.pdf;
using Svg;
using Svg.Transforms;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace DSLNG.PEAR.Web.Extensions
{
    public class Exporter
    {
        private readonly string _FontFamilyName;
        private const string DefaultFileName = "Chart";
        private const string PdfMetaCreator = "Exporting Module for Highcharts JS";
        public string ContentDisposition { get; private set; }
        public string ContentType { get; private set; }
        public string FileName { get; private set; }
        public string Name { get; private set; }
        public string Svg { get; private set; }
        public int Width { get; private set; }

        public Exporter(string fileName, string type, int width, string svg, string fontFamilyName)
        {
            _FontFamilyName = fontFamilyName;
            string extension;

            this.ContentType = type.ToLower();
            this.Name = fileName;
            this.Svg = svg;
            this.Width = width;

            switch (ContentType)
            {
                case "image/jpeg":
                    extension = "jpg";
                    break;
                case "image/png":
                    extension = "png";
                    break;

                case "application/pdf":
                    extension = "pdf";
                    break;

                case "image/svg+xml":
                    extension = "svg";
                    break;
                default:
                    throw new ArgumentException(
                      string.Format("Invalid type specified: '{0}'.", type));
            }

            this.FileName = string.Format("{0}.{1}", string.IsNullOrEmpty(fileName) ? DefaultFileName : fileName, extension);

            this.ContentDisposition =  string.Format("attachment; filename={0}", this.FileName);
        }

        private SvgDocument CreateSvgDocument() {
            SvgDocument svgDoc;

            using (MemoryStream streamSvg = new MemoryStream(
                Encoding.UTF8.GetBytes(this.Svg)))
            {
                // Create and return SvgDocument from stream.
                svgDoc = SvgDocument.Open(streamSvg);
            }

            // Scale SVG document to requested width.
            svgDoc.Transforms = new SvgTransformCollection();
            float scalar = (float)this.Width / (float)svgDoc.Width;
            svgDoc.Transforms.Add(new SvgScale(scalar, scalar));
            svgDoc.Width = new SvgUnit(svgDoc.Width.Type, svgDoc.Width * scalar);
            svgDoc.Height = new SvgUnit(svgDoc.Height.Type, svgDoc.Height * scalar);

            if (!string.IsNullOrEmpty(_FontFamilyName))
            {
                System.Drawing.FontFamily fontFamily = new System.Drawing.FontFamily(_FontFamilyName);
                foreach (var child in svgDoc.Children)
                {
                    SetFont(child, fontFamily);
                }
            }

            return svgDoc;
        }

        private void SetFont(SvgElement element, System.Drawing.FontFamily fontFamily)
        {
            foreach (var child in element.Children)
            {
                SetFont(child, fontFamily); //Call this function again with the child, this will loop
                //until the element has no more children
            }

            if (element is SvgText)
            {
                var textElement = (SvgText)element;
                textElement.Font = new System.Drawing.Font(fontFamily.Name, textElement.Font.Size,
                    textElement.Font.Style, textElement.Font.Unit, textElement.Font.GdiCharSet);
            }
        }

        public void WriteToHttpResponse(HttpResponseBase httpResponse)
        {
            httpResponse.ClearContent();
            httpResponse.ClearHeaders();
            httpResponse.ContentType = this.ContentType;
            httpResponse.AddHeader("Content-Disposition", this.ContentDisposition);
            WriteToStream(httpResponse.OutputStream);
        }

        internal void WriteToStream(Stream outputStream)
        {
            switch (this.ContentType)
            {
                case "image/jpeg":
                    CreateSvgDocument().Draw().Save(
                      outputStream,
                      ImageFormat.Jpeg);
                    break;

                case "image/png":
                    // PNG output requires a seekable stream.
                    using (MemoryStream seekableStream = new MemoryStream())
                    {
                        CreateSvgDocument().Draw().Save(
                            seekableStream,
                            ImageFormat.Png);
                        seekableStream.WriteTo(outputStream);
                    }
                    break;

                case "application/pdf":
                    SvgDocument svgDoc = CreateSvgDocument();

                    // Create PDF document.
                    using (Document pdfDoc = new Document())
                    {
                        // Scalar to convert from 72 dpi to 150 dpi.
                        float dpiScalar = 150f / 72f;

                        // Set page size. Page dimensions are in 1/72nds of an inch.
                        // Page dimensions are scaled to boost dpi and keep page
                        // dimensions to a smaller size.
                        pdfDoc.SetPageSize(new Rectangle(
                          svgDoc.Width / dpiScalar,
                          svgDoc.Height / dpiScalar));

                        // Set margin to none.
                        pdfDoc.SetMargins(0f, 0f, 0f, 0f);

                        // Create PDF writer to write to response stream.
                        using (PdfWriter pdfWriter = PdfWriter.GetInstance(
                            pdfDoc,
                            outputStream))
                        {
                            // Configure PdfWriter.
                            pdfWriter.SetPdfVersion(PdfWriter.PDF_VERSION_1_5);
                            pdfWriter.CompressionLevel = PdfStream.DEFAULT_COMPRESSION;

                            // Add meta data.
                            pdfDoc.AddCreator(PdfMetaCreator);
                            pdfDoc.AddTitle(this.Name);

                            // Output PDF document.
                            pdfDoc.Open();
                            pdfDoc.NewPage();

                            // Create image element from SVG image.
                            Image image = Image.GetInstance(svgDoc.Draw(), ImageFormat.Bmp);
                            image.CompressionLevel = PdfStream.DEFAULT_COMPRESSION;

                            // Must match scaling performed when setting page size.
                            image.ScalePercent(100f / dpiScalar);

                            // Add image element to PDF document.
                            pdfDoc.Add(image);
                            pdfWriter.Close();
                            pdfDoc.Close();
                        }
                    }

                    break;

                case "image/svg+xml":
                    using (StreamWriter writer = new StreamWriter(outputStream))
                    {
                        writer.Write(this.Svg);
                        writer.Flush();
                    }

                    break;

                default:
                    throw new InvalidOperationException(string.Format(
                      "ContentType '{0}' is invalid.", this.ContentType));
            }

            outputStream.Flush();
            outputStream.Close();
        }
    }
}