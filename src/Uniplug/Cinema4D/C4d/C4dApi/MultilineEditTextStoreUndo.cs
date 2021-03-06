/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 3.0.2
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace C4d {

public class MultilineEditTextStoreUndo : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal MultilineEditTextStoreUndo(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(MultilineEditTextStoreUndo obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~MultilineEditTextStoreUndo() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          C4dApiPINVOKE.delete_MultilineEditTextStoreUndo(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public BaseContainer undoData {
    set {
      C4dApiPINVOKE.MultilineEditTextStoreUndo_undoData_set(swigCPtr, BaseContainer.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = C4dApiPINVOKE.MultilineEditTextStoreUndo_undoData_get(swigCPtr);
      BaseContainer ret = (cPtr == global::System.IntPtr.Zero) ? null : new BaseContainer(cPtr, false);
      return ret;
    } 
  }

  public DescID id {
    set {
      C4dApiPINVOKE.MultilineEditTextStoreUndo_id_set(swigCPtr, DescID.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = C4dApiPINVOKE.MultilineEditTextStoreUndo_id_get(swigCPtr);
      DescID ret = (cPtr == global::System.IntPtr.Zero) ? null : new DescID(cPtr, false);
      return ret;
    } 
  }

  public MULTILINEEDITTEXTMESSAGEFLAGS flags {
    set {
      C4dApiPINVOKE.MultilineEditTextStoreUndo_flags_set(swigCPtr, (int)value);
    } 
    get {
      MULTILINEEDITTEXTMESSAGEFLAGS ret = (MULTILINEEDITTEXTMESSAGEFLAGS)C4dApiPINVOKE.MultilineEditTextStoreUndo_flags_get(swigCPtr);
      return ret;
    } 
  }

  public MultilineEditTextStoreUndo() : this(C4dApiPINVOKE.new_MultilineEditTextStoreUndo(), true) {
  }

}

}
