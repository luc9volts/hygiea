using System.Collections.Generic;
using hygiea.domain;

namespace hygiea.data
{
    public sealed class BeneficiaryRepositoryMock : BeneficiaryRepository
    {
        private readonly Dictionary<int, Beneficiary> _beneficiaries;

        public BeneficiaryRepositoryMock()
        {
            _beneficiaries = new Dictionary<int, Beneficiary>
            {
                [1] = new Beneficiary
                {
                    Id = 1,
                    Name = "BENEDITO",
                    CoveredProcedures = new List<string>() { "P001", "P002", "P003" }
                },
                [2] = new Beneficiary
                {
                    Id = 2,
                    Name = "BENEVIDES",
                    CoveredProcedures = new List<string>() { "P004", "P005", "P006" }
                },
                [3] = new Beneficiary
                {
                    Id = 3,
                    Name = "BEN-HUR",
                    CoveredProcedures = new List<string>() { "P007", "P008", "P009" }
                }
            };
        }

        public Beneficiary GetBy(int id) => _beneficiaries[id];
    }
}
