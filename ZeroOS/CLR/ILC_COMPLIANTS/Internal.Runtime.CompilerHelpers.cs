using System;

namespace Internal.Runtime.CompilerHelpers
{
    using System.Runtime;

    // A class that the compiler looks for that has helpers to initialize the
    // process. The compiler can gracefully handle the helpers not being present,
    // but the class itself being absent is unhandled. Let's add an empty class.
    class StartupCodeHelpers
    {
        // A couple symbols the generated code will need we park them in this class
        // for no particular reason. These aid in transitioning to/from managed code.
        // Since we don't have a GC, the transition is a no-op.
        [RuntimeExport("RhpReversePInvoke2")]
        static void RhpReversePInvoke2() { }
        [RuntimeExport("RhpFallbackFailFast")]
        static void RhpFallbackFailFast() { }
        [RuntimeExport("RhpReversePInvokeReturn2")]
        static void RhpReversePInvokeReturn2() { }
        [RuntimeExport("RhpReversePInvoke")]
        static void RhpReversePInvoke() { }
        [RuntimeExport("RhpReversePInvokeReturn")]
        static void RhpReversePInvokeReturn() { }
        [System.Runtime.RuntimeExport("__fail_fast")]
        static void FailFast() { while (true) ; }
        [System.Runtime.RuntimeExport("RhpPInvoke")]
        static void RphPinvoke() { }
        [System.Runtime.RuntimeExport("RhpPInvokeReturn")]
        static void RphPinvokeReturn() { }
    }
}