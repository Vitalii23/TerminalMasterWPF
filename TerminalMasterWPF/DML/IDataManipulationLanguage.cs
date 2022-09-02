using System.Collections.Generic;

namespace TerminalMasterWPF.DML
{
    interface IDataManipulationLanguage<T>
    {
        void Add(T element);

        void Update(T element);

        void Delete(T element);
    }
}
