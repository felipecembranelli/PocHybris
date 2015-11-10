using GitHubSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubSample.Services.IServices
{
    public interface IPracticeRequirementService
	{
        IEnumerable<PracticeRequirement> GetAll();
        PracticeRequirement GetById(int id);
        void Add(PracticeRequirement practiceRequirement);
        void Update(PracticeRequirement practiceRequirement);
        void Delete(PracticeRequirement practiceRequirement);
        IEnumerable<PracticeRequirement> GetByRequirementsByPracticeId(int practiceId);
	} 
}
