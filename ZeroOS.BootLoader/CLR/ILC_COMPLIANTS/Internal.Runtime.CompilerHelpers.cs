using System;
using Internal.TypeSystem;

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

    public static class ThrowHelpers
    {
        public static void ThrowOverflowException()
        {
        }

        public static void ThrowIndexOutOfRangeException()
        {
        }

        public static void ThrowNullReferenceException()
        {
        }

        public static void ThrowDivideByZeroException()
        {
        }

        public static void ThrowArrayTypeMismatchException()
        {
        }

        public static void ThrowPlatformNotSupportedException()
        {
        }

        public static void ThrowNotImplementedException()
        {
        }

        public static void ThrowNotSupportedException()
        {
        }

        public static void ThrowBadImageFormatException(ExceptionStringID id)
        {
        }

        public static void ThrowTypeLoadException(ExceptionStringID id, string className, string typeName)
        {
        }

        public static void ThrowTypeLoadExceptionWithArgument(ExceptionStringID id, string className, string typeName, string messageArg)
        {
        }

        public static void ThrowMissingMethodException(ExceptionStringID id, string methodName)
        {
        }

        public static void ThrowMissingFieldException(ExceptionStringID id, string fieldName)
        {
        }

        public static void ThrowFileNotFoundException(ExceptionStringID id, string fileName)
        {
        }

        public static void ThrowInvalidProgramException(ExceptionStringID id)
        {
        }

        public static void ThrowInvalidProgramExceptionWithArgument(ExceptionStringID id, string methodName)
        {
        }

        public static void ThrowArgumentException()
        {
        }

        public static void ThrowArgumentOutOfRangeException()
        {
        }
    }
}