using GitHubSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubSample.Services.IServices
{
    public interface IProcessPerspectiveService
	{
        IEnumerable<ProcessPerspective> GetAll();
        ProcessPerspective GetById(int id);
        void Add(ProcessPerspective process);
        void Update(ProcessPerspective process);
        void Delete(ProcessPerspective process);
	} 
}
