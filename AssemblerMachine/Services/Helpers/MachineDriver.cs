using System.Runtime.InteropServices;

namespace AssemblerMachine.Services.Helpers
{
    public static class MachineDriver
    {
        const string Driver = "MachineDriver.dll";
        
        [DllImport(Driver, EntryPoint = "?Assemble@@YAXV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Assemble(Int64 data);
        
    }
}
