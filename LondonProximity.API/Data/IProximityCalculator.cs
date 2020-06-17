using System.Collections.Generic;
using System.Threading.Tasks;

namespace LondonProximity.API
{
    public interface IProximityCalculator
    {
        Task<IEnumerable<LondonJobApplicant>> AllApplicantsAsync();
    }
}
