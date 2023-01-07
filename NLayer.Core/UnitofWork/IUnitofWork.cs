using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.UnitofWork
{
    public interface IUnitofWork
    {
        Task CommitAsync(); // save change asenkron
        void Commit(); // save change
    }
}
