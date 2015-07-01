using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Services.Requests.Conversion;
using DSLNG.PEAR.Services.Responses.Conversion;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IConversionService
    {
        GetConversionResponse GetConversion(GetConversionRequest request);
        GetConversionsResponse GetConversions(GetConversionsRequest request);
        CreateConversionResponse Create(CreateConversionRequest request);
        UpdateConversionResponse Update(UpdateConversionRequest request);
        DeleteConversionResponse Delete(int Id);
    }
}
