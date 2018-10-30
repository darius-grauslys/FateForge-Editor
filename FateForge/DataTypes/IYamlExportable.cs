using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FateForge.DataTypes
{
    interface IYamlExportable
    {
        /// <summary>
        /// Only implement for data writen into objective.yaml
        /// </summary>
        /// <returns></returns>
        string GetYamlString_For_Objective();
        /// <summary>
        /// Only implement for data writen into events.yaml
        /// </summary>
        /// <returns></returns>
        string GetYamlString_For_Event();
        /// <summary>
        /// Only implement for data writen into conditions.yaml
        /// </summary>
        /// <returns></returns>
        string GetYamlString_For_Condition();

        /// <summary>
        /// Only implement if inheriting class is an ObjectiveField or similar.
        /// </summary>
        /// <returns></returns>
        string GetYamlString_As_Objective(int indexOf);
        /// <summary>
        /// Only implement if inheriting class is an EventField or similar.
        /// </summary>
        /// <returns></returns>
        string GetYamlString_As_Event(int indexOf);
        /// <summary>
        /// Only implement if inheriting class is a ConditionField or similar.
        /// </summary>
        /// <returns></returns>
        string GetYamlString_As_Condition(int indexOf);
    }
}
