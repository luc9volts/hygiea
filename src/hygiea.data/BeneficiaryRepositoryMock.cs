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
                    CoveredProcedures = new List<string>() { "S001", "S002", "S003" }
                },
                [2] = new Beneficiary
                {
                    Id = 2,
                    Name = "BENEVIDES",
                    CoveredProcedures = new List<string>() { "S004", "S005", "S006" }
                },
                [3] = new Beneficiary
                {
                    Id = 3,
                    Name = "BEN-HUR",
                    CoveredProcedures = new List<string>() { "S007", "S008", "S009" }
                },
                [4] = new Beneficiary
                {
                    Id = 4,
                    Name = "Teste",
                    CoveredProcedures = new List<string>() { "S010", "S011", "S012" }
                }
            };
        }

        public Beneficiary GetBy(int id) => _beneficiaries[id];
    }
}
