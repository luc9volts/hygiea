using System.Collections.Generic;

namespace hygiea.domain
{
    public class Beneficiary
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public IEnumerable<string> CoveredProcedures { get; init; }
    }
}