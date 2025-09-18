using System.Collections.Generic;
using serviceoncologie.Data.Models;

namespace serviceoncologie.Repository
{
    public interface IDecisionStafRepository
    {
        IEnumerable<DecisionStaf> GetAll();
        DecisionStaf GetById(int id);
        void Add(DecisionStaf decisionStaf);
        void Update(DecisionStaf decisionStaf);
        void Delete(int id);
        IEnumerable<int> GetDecisionsWithAdmission();
        Consultation GetConsultationByDecisionId(int id);
        IEnumerable<DecisionStaf> GetDecisionsByDossierId(int dossierId);

    }
}
