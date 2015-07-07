

using System.Collections.Generic;
namespace DSLNG.PEAR.Services.Responses.Artifact
{
    public class GetArtifactsToSelectResponse
    {
        public IList<ArtifactResponse> Artifacts { get; set; }

        public class ArtifactResponse
        {
            public int id { get; set; }
            public string Name { get; set; }
        }
    }
}
