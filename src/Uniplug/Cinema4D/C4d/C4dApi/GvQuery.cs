/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 3.0.2
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace C4d {

public class GvQuery : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal GvQuery(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(GvQuery obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          throw new global::System.MethodAccessException("C++ destructor does not have public access");
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public GvCalcFlags GetFlags() {
    GvCalcFlags ret = (GvCalcFlags)C4dApiPINVOKE.GvQuery_GetFlags(swigCPtr);
    return ret;
  }

  public void SetFlags(GvCalcFlags flags) {
    C4dApiPINVOKE.GvQuery_SetFlags(swigCPtr, (int)flags);
  }

  public void SetError(GvError error) {
    C4dApiPINVOKE.GvQuery_SetError(swigCPtr, (int)error);
  }

  public GvError GetError() {
    GvError ret = (GvError)C4dApiPINVOKE.GvQuery_GetError(swigCPtr);
    return ret;
  }

}

}
