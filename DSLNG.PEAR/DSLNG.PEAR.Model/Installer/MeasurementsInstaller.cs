

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
            var caseMeasurement = new Measurement
            {
                Id = 1,
                IsActive = true,
                Name = "Case",
                Remark = "Cases"
            };

            _context.Measurements.Add(caseMeasurement);
        }
    }
}
