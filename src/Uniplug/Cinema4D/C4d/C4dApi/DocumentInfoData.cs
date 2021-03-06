/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 3.0.2
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace C4d {

public class DocumentInfoData : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal DocumentInfoData(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(DocumentInfoData obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~DocumentInfoData() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          C4dApiPINVOKE.delete_DocumentInfoData(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public DocumentInfoData() : this(C4dApiPINVOKE.new_DocumentInfoData(), true) {
  }

  public int type {
    set {
      C4dApiPINVOKE.DocumentInfoData_type_set(swigCPtr, value);
    } 
    get {
      int ret = C4dApiPINVOKE.DocumentInfoData_type_get(swigCPtr);
      return ret;
    } 
  }

  public int fileformat {
    set {
      C4dApiPINVOKE.DocumentInfoData_fileformat_set(swigCPtr, value);
    } 
    get {
      int ret = C4dApiPINVOKE.DocumentInfoData_fileformat_get(swigCPtr);
      return ret;
    } 
  }

  public BaseDocument doc {
    set {
      C4dApiPINVOKE.DocumentInfoData_doc_set(swigCPtr, BaseDocument.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = C4dApiPINVOKE.DocumentInfoData_doc_get(swigCPtr);
      BaseDocument ret = (cPtr == global::System.IntPtr.Zero) ? null : new BaseDocument(cPtr, false);
      return ret;
    } 
  }

  public Filename filename {
    set {
      C4dApiPINVOKE.DocumentInfoData_filename_set(swigCPtr, Filename.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = C4dApiPINVOKE.DocumentInfoData_filename_get(swigCPtr);
      Filename ret = (cPtr == global::System.IntPtr.Zero) ? null : new Filename(cPtr, false);
      return ret;
    } 
  }

  public BaseList2D bl {
    set {
      C4dApiPINVOKE.DocumentInfoData_bl_set(swigCPtr, BaseList2D.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = C4dApiPINVOKE.DocumentInfoData_bl_get(swigCPtr);
      BaseList2D ret = (cPtr == global::System.IntPtr.Zero) ? null : new BaseList2D(cPtr, false);
      return ret;
    } 
  }

  public bool gui_allowed {
    set {
      C4dApiPINVOKE.DocumentInfoData_gui_allowed_set(swigCPtr, value);
    } 
    get {
      bool ret = C4dApiPINVOKE.DocumentInfoData_gui_allowed_get(swigCPtr);
      return ret;
    } 
  }

  public SWIGTYPE_p_void data {
    set {
      C4dApiPINVOKE.DocumentInfoData_data_set(swigCPtr, SWIGTYPE_p_void.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = C4dApiPINVOKE.DocumentInfoData_data_get(swigCPtr);
      SWIGTYPE_p_void ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_void(cPtr, false);
      return ret;
    } 
  }

}

}
