/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 3.0.2
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace C4d {

public class GetCustomIconData : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal GetCustomIconData(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(GetCustomIconData obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~GetCustomIconData() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          C4dApiPINVOKE.delete_GetCustomIconData(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public GetCustomIconData() : this(C4dApiPINVOKE.new_GetCustomIconData(), true) {
  }

  public SWIGTYPE_p_IconData dat {
    set {
      C4dApiPINVOKE.GetCustomIconData_dat_set(swigCPtr, SWIGTYPE_p_IconData.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = C4dApiPINVOKE.GetCustomIconData_dat_get(swigCPtr);
      SWIGTYPE_p_IconData ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_IconData(cPtr, false);
      return ret;
    } 
  }

  public bool filled {
    set {
      C4dApiPINVOKE.GetCustomIconData_filled_set(swigCPtr, value);
    } 
    get {
      bool ret = C4dApiPINVOKE.GetCustomIconData_filled_get(swigCPtr);
      return ret;
    } 
  }

}

}
