

using DSLNG.PEAR.Services.Requests.Artifact;
using DSLNG.PEAR.Services.Responses.Artifact;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IArtifactService
    {
        GetSeriesResponse GetSeries(GetSeriesRequest request);
    }
}
