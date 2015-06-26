using DSLNG.PEAR.Services.Requests.Pillar;
using DSLNG.PEAR.Services.Responses.Pillar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IPillarService 
    {
        GetPillarResponse GetPillar(GetPillarRequest request);
        GetPillarsResponse GetPillars(GetPillarsRequest request);
        CreatePillarResponse Create(CreatePillarRequest request);
        UpdatePillarResponse Update(UpdatePillarRequest request);
        DeletePillarResponse Delete(int id);
    }
}
