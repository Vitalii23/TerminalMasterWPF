using System.Collections.Generic;

namespace TerminalMasterWPF.DML
{
    interface IDataManipulationLanguage<T>
    {
        void Add(T element);

        void Update(T element);

        bool Delete(T element);

        void OrderBy(T element, bool trigger);

        IList<T> List();

        T Get(int value);
    }
}
