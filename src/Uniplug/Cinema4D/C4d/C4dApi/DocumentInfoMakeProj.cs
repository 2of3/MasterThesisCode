/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 3.0.2
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace C4d {

public class DocumentInfoMakeProj : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal DocumentInfoMakeProj(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(DocumentInfoMakeProj obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~DocumentInfoMakeProj() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          C4dApiPINVOKE.delete_DocumentInfoMakeProj(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public DocumentInfoMakeProj() : this(C4dApiPINVOKE.new_DocumentInfoMakeProj(), true) {
  }

  public Filename sfile {
    set {
      C4dApiPINVOKE.DocumentInfoMakeProj_sfile_set(swigCPtr, Filename.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = C4dApiPINVOKE.DocumentInfoMakeProj_sfile_get(swigCPtr);
      Filename ret = (cPtr == global::System.IntPtr.Zero) ? null : new Filename(cPtr, false);
      return ret;
    } 
  }

  public Filename dfile {
    set {
      C4dApiPINVOKE.DocumentInfoMakeProj_dfile_set(swigCPtr, Filename.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = C4dApiPINVOKE.DocumentInfoMakeProj_dfile_get(swigCPtr);
      Filename ret = (cPtr == global::System.IntPtr.Zero) ? null : new Filename(cPtr, false);
      return ret;
    } 
  }

  public bool allowCopy {
    set {
      C4dApiPINVOKE.DocumentInfoMakeProj_allowCopy_set(swigCPtr, value);
    } 
    get {
      bool ret = C4dApiPINVOKE.DocumentInfoMakeProj_allowCopy_get(swigCPtr);
      return ret;
    } 
  }

  public bool allowGUI {
    set {
      C4dApiPINVOKE.DocumentInfoMakeProj_allowGUI_set(swigCPtr, value);
    } 
    get {
      bool ret = C4dApiPINVOKE.DocumentInfoMakeProj_allowGUI_get(swigCPtr);
      return ret;
    } 
  }

}

}
