/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 3.0.2
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace C4d {

public class AutoRWLocker : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal AutoRWLocker(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(AutoRWLocker obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~AutoRWLocker() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          C4dApiPINVOKE.delete_AutoRWLocker(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public AutoRWLocker() : this(C4dApiPINVOKE.new_AutoRWLocker__SWIG_0(), true) {
  }

  public AutoRWLocker(AutoRWLock arg0, bool write_lock) : this(C4dApiPINVOKE.new_AutoRWLocker__SWIG_1(AutoRWLock.getCPtr(arg0), write_lock), true) {
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
  }

  public AutoRWLocker(AutoRWLock arg0) : this(C4dApiPINVOKE.new_AutoRWLocker__SWIG_2(AutoRWLock.getCPtr(arg0)), true) {
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
  }

  public void DoLock(AutoRWLock arg0, bool write_lock) {
    C4dApiPINVOKE.AutoRWLocker_DoLock__SWIG_0(swigCPtr, AutoRWLock.getCPtr(arg0), write_lock);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
  }

  public void DoLock(AutoRWLock arg0) {
    C4dApiPINVOKE.AutoRWLocker_DoLock__SWIG_1(swigCPtr, AutoRWLock.getCPtr(arg0));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Unlock() {
    C4dApiPINVOKE.AutoRWLocker_Unlock(swigCPtr);
  }

}

}
