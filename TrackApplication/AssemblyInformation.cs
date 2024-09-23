/**
 * @Author: Ebad Hassan
 * @Date:   2024-09-20 11:03:54
 * @Last Modified by:   Ebad Hassan
 * @Last Modified time: 2024-09-20 11:10:48
 */

using System.Reflection;
namespace InvoiceBulkRegisteration.TrackApplication
{
    public class AssemblyInformation
    {
        AssemblyInformation(string Product, string Description, string Version) { } 
        public static readonly AssemblyInformation Current = new AssemblyInformation(typeof(AssemblyInformation).Assembly);
        public AssemblyInformation(Assembly assembly) : this(
            assembly.GetCustomAttribute<AssemblyProductAttribute>().Product,
            assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description,
            assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version
            )
        {
        }
    }
}
