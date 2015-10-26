using GitHubSample.Data.Infrastructure;
using GitHubSample.Data.Repository;
using GitHubSample.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubSample.Services
{
    public class PracticeRequirementService : IPracticeRequirementService
    {
        private readonly IPracticeRequirementRepository practiceRequirementRepository;
        private readonly IUnitOfWork unitOfWork;

        public PracticeRequirementService(IPracticeRequirementRepository practiceRequirementRepository, IUnitOfWork unitOfWork)
        {
            this.practiceRequirementRepository = practiceRequirementRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Model.PracticeRequirement> GetAll()
        {
            return this.practiceRequirementRepository.GetAll();
        }

        public Model.PracticeRequirement GetById(int id)
        {
            return this.practiceRequirementRepository.GetById(id);
        }

        public void Add(Model.PracticeRequirement practiceRequirement)
        {
            this.practiceRequirementRepository.Add(practiceRequirement);

            this.unitOfWork.Commit();
        }

        public void Update(Model.PracticeRequirement practiceRequirement)
        {
            this.practiceRequirementRepository.Update(practiceRequirement);

            this.unitOfWork.Commit();
        }

        public void Delete(Model.PracticeRequirement practiceRequirement)
        {
            this.practiceRequirementRepository.Delete(practiceRequirement);

            this.unitOfWork.Commit();
        }


        public IEnumerable<Model.PracticeRequirement> GetByRequirementsByPracticeId(int practiceId)
        {
            return this.practiceRequirementRepository.GetByRequirementsByPracticeId(practiceId);
        }
    }
}
