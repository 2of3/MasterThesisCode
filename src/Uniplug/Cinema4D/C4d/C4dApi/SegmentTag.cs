/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 3.0.2
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace C4d {

public class SegmentTag : VariableTag {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal SegmentTag(global::System.IntPtr cPtr, bool cMemoryOwn) : base(C4dApiPINVOKE.SegmentTag_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(SegmentTag obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          throw new global::System.MethodAccessException("C++ destructor does not have public access");
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public Segment GetDataAddressR() {
    global::System.IntPtr cPtr = C4dApiPINVOKE.SegmentTag_GetDataAddressR(swigCPtr);
    Segment ret = (cPtr == global::System.IntPtr.Zero) ? null : new Segment(cPtr, false);
    return ret;
  }

  public Segment GetDataAddressW() {
    global::System.IntPtr cPtr = C4dApiPINVOKE.SegmentTag_GetDataAddressW(swigCPtr);
    Segment ret = (cPtr == global::System.IntPtr.Zero) ? null : new Segment(cPtr, false);
    return ret;
  }

}

}
