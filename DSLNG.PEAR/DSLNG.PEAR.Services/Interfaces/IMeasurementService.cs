using DSLNG.PEAR.Services.Requests.Measurement;
using DSLNG.PEAR.Services.Responses.Measurement;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IMeasurementService
    {
        CreateMeasurementResponse Create(CreateMeasurementRequest request);
        UpdateMeasurementResponse Update(UpdateMeasurementRequest request);
        DeleteMeasurementResponse Delete(int id);
        GetMeasurementResponse GetMeasurement(GetMeasurementRequest request);
        GetMeasurementsResponse GetMeasurements(GetMeasurementsRequest request);
    }
}
