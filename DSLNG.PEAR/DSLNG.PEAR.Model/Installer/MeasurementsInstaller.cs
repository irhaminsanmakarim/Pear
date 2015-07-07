

using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;
namespace DSLNG.PEAR.Data.Installer
{
    class MeasurementsInstaller
    {
        private DataContext _context;
        public MeasurementsInstaller(DataContext context)
        {
            _context = context;
        }
        public void Install()
        {
            var meas1 = new Measurement {
                Id = 1,
                Name = "tbtu",
                IsActive = true
            };
            var meas2 = new Measurement
            {
                Id = 2,
                Name = "MMSCF",
                IsActive = true
            };
            
            var meas3 = new Measurement
            {
                Id = 3,
                Name = "MT",
                IsActive = true
            };
            var meas4 = new Measurement
            {
                Id = 4,
                Name = "%",
                IsActive = true
            };
            var meas5 = new Measurement
            {
                Id = 5,
                Name = "days",
                IsActive = true
            };
            var meas6 = new Measurement
            {
                Id = 6,
                Name = "Case",
                IsActive = true
            };
            var meas7 = new Measurement
            {
                Id = 7,
                Name = "Cargo",
                IsActive = true
            };
            var meas8 = new Measurement
            {
                Id = 8,
                Name = "USD(Mio)",
                IsActive = true
            };
            var meas9 = new Measurement
            {
                Id = 9,
                Name = "Times",
                IsActive = true
            };
            var meas10 = new Measurement
            {
                Id = 10,
                Name = "USD/mmbtu",
                IsActive = true
            };
            var meas11 = new Measurement
            {
                Id = 11,
                Name = "Number",
                IsActive = true
            };
            var meas12 = new Measurement
            {
                Id = 12,
                Name = "USD/pax",
                IsActive = true
            };
            var meas13 = new Measurement
            {
                Id = 13,
                Name = "USD",
                IsActive = true
            };
            var meas14 = new Measurement
            {
                Id = 14,
                Name = "hour",
                IsActive = true
            };
            var meas15 = new Measurement
            {
                Id = 15,
                Name = "MMSCFD",
                IsActive = true
            };
            var meas16 = new Measurement
            {
                Id = 16,
                Name = "MMBBL",
                IsActive = true
            };
            var meas17 = new Measurement
            {
                Id = 17,
                Name = "USD/bbl",
                IsActive = true
            };
            
            _context.Measurements.Add(meas1);
            _context.Measurements.Add(meas2);
            _context.Measurements.Add(meas3);
            _context.Measurements.Add(meas4);
            _context.Measurements.Add(meas5);
            _context.Measurements.Add(meas6);
            _context.Measurements.Add(meas7);
            _context.Measurements.Add(meas8);
            _context.Measurements.Add(meas9);
            _context.Measurements.Add(meas10);
            _context.Measurements.Add(meas11);
            _context.Measurements.Add(meas12);
            _context.Measurements.Add(meas13);
            _context.Measurements.Add(meas14);
            _context.Measurements.Add(meas15);
            _context.Measurements.Add(meas16);
            _context.Measurements.Add(meas17);

        }
    }
}
