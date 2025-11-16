using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ISaveLoadService<T>
{

    void SaveData(T data, string id);
    T LoadData(string id);
}

