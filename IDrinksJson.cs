using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksInfo
{
    public interface IDrinksJson
    {
        public string GetName();

        public void ChangeName(string name);
    }
}
