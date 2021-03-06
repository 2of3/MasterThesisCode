/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 3.0.2
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace C4d {

public class GvWorld : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal GvWorld(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(GvWorld obj) {
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

  public GvNodeMaster AllocNodeMaster(BaseList2D arg0, bool add_to_list, bool send_messages) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.GvWorld_AllocNodeMaster__SWIG_0(swigCPtr, BaseList2D.getCPtr(arg0), add_to_list, send_messages);
    GvNodeMaster ret = (cPtr == global::System.IntPtr.Zero) ? null : new GvNodeMaster(cPtr, false);
    return ret;
  }

  public GvNodeMaster AllocNodeMaster(BaseList2D arg0, bool add_to_list) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.GvWorld_AllocNodeMaster__SWIG_1(swigCPtr, BaseList2D.getCPtr(arg0), add_to_list);
    GvNodeMaster ret = (cPtr == global::System.IntPtr.Zero) ? null : new GvNodeMaster(cPtr, false);
    return ret;
  }

  public GvNodeMaster AllocNodeMaster(BaseList2D arg0) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.GvWorld_AllocNodeMaster__SWIG_2(swigCPtr, BaseList2D.getCPtr(arg0));
    GvNodeMaster ret = (cPtr == global::System.IntPtr.Zero) ? null : new GvNodeMaster(cPtr, false);
    return ret;
  }

  public void FreeNodeMaster(SWIGTYPE_p_p_GvNodeMaster master) {
    C4dApiPINVOKE.GvWorld_FreeNodeMaster(swigCPtr, SWIGTYPE_p_p_GvNodeMaster.getCPtr(master));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
  }

  public GvNodeGUI AllocNodeGUI(SWIGTYPE_p_GvShape shape, SWIGTYPE_p_GvShape group, int user_area_id) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.GvWorld_AllocNodeGUI(swigCPtr, SWIGTYPE_p_GvShape.getCPtr(shape), SWIGTYPE_p_GvShape.getCPtr(group), user_area_id);
    GvNodeGUI ret = (cPtr == global::System.IntPtr.Zero) ? null : new GvNodeGUI(cPtr, false);
    return ret;
  }

  public void FreeNodeGUI(SWIGTYPE_p_p_GvNodeGUI gui) {
    C4dApiPINVOKE.GvWorld_FreeNodeGUI(swigCPtr, SWIGTYPE_p_p_GvNodeGUI.getCPtr(gui));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
  }

  public SWIGTYPE_p_GvShape AllocShape() {
    global::System.IntPtr cPtr = C4dApiPINVOKE.GvWorld_AllocShape(swigCPtr);
    SWIGTYPE_p_GvShape ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_GvShape(cPtr, false);
    return ret;
  }

  public SWIGTYPE_p_GvShape AllocGroupShape() {
    global::System.IntPtr cPtr = C4dApiPINVOKE.GvWorld_AllocGroupShape(swigCPtr);
    SWIGTYPE_p_GvShape ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_GvShape(cPtr, false);
    return ret;
  }

  public void FreeShape(SWIGTYPE_p_p_GvShape shape) {
    C4dApiPINVOKE.GvWorld_FreeShape(swigCPtr, SWIGTYPE_p_p_GvShape.getCPtr(shape));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool RegisterHook(GvHook hook, SWIGTYPE_p_void user) {
    bool ret = C4dApiPINVOKE.GvWorld_RegisterHook(swigCPtr, GvHook.getCPtr(hook), SWIGTYPE_p_void.getCPtr(user));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool AttachHook(int hook_id, SWIGTYPE_p_f_p_BaseDocument_Int32__p_BaseList2D callback) {
    bool ret = C4dApiPINVOKE.GvWorld_AttachHook(swigCPtr, hook_id, SWIGTYPE_p_f_p_BaseDocument_Int32__p_BaseList2D.getCPtr(callback));
    return ret;
  }

  public void DetachHook(int hook_id) {
    C4dApiPINVOKE.GvWorld_DetachHook(swigCPtr, hook_id);
  }

  public BaseList2D GetHookInstance(BaseDocument doc, int hook_id) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.GvWorld_GetHookInstance(swigCPtr, BaseDocument.getCPtr(doc), hook_id);
    BaseList2D ret = (cPtr == global::System.IntPtr.Zero) ? null : new BaseList2D(cPtr, false);
    return ret;
  }

  public bool SendHookMessage(BaseDocument doc, GvNodeMaster master, GvMessHook data, int owner_id) {
    bool ret = C4dApiPINVOKE.GvWorld_SendHookMessage(swigCPtr, BaseDocument.getCPtr(doc), GvNodeMaster.getCPtr(master), GvMessHook.getCPtr(data), owner_id);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool SendOperatorMessage(BaseDocument doc, int message_id, SWIGTYPE_p_void data) {
    bool ret = C4dApiPINVOKE.GvWorld_SendOperatorMessage(swigCPtr, BaseDocument.getCPtr(doc), message_id, SWIGTYPE_p_void.getCPtr(data));
    return ret;
  }

  public bool OpenDialog(int id, GvNodeMaster master) {
    bool ret = C4dApiPINVOKE.GvWorld_OpenDialog(swigCPtr, id, GvNodeMaster.getCPtr(master));
    return ret;
  }

  public void CloseDialog(int id) {
    C4dApiPINVOKE.GvWorld_CloseDialog(swigCPtr, id);
  }

  public void RedrawAll() {
    C4dApiPINVOKE.GvWorld_RedrawAll(swigCPtr);
  }

  public void RedrawMaster(GvNodeMaster master) {
    C4dApiPINVOKE.GvWorld_RedrawMaster(swigCPtr, GvNodeMaster.getCPtr(master));
  }

  public bool AttachNode(GvNodeMaster master, GvNode node, int x, int y) {
    bool ret = C4dApiPINVOKE.GvWorld_AttachNode(swigCPtr, GvNodeMaster.getCPtr(master), GvNode.getCPtr(node), x, y);
    return ret;
  }

  public string /* String_cstype */ GetString(string /* constString&_cstype */ title, string /* constString&_cstype */ default_value)  {  /* <String_csout> */
      string ret = C4dApiPINVOKE.GvWorld_GetString(swigCPtr, title, default_value);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
      return ret;
   } /* </String_csout> */ 

  public double GetFloat(string /* constString&_cstype */ title, double default_value) {
    double ret = C4dApiPINVOKE.GvWorld_GetFloat(swigCPtr, title, default_value);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int GetInteger(string /* constString&_cstype */ title, int default_value) {
    int ret = C4dApiPINVOKE.GvWorld_GetInteger(swigCPtr, title, default_value);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int GetDataTypesMenu(BaseContainer bc, BaseContainer index, int first_menu_id, int first_sub_id, bool show_undefined_type, GvValueFlags flags) {
    int ret = C4dApiPINVOKE.GvWorld_GetDataTypesMenu__SWIG_0(swigCPtr, BaseContainer.getCPtr(bc), BaseContainer.getCPtr(index), first_menu_id, first_sub_id, show_undefined_type, (int)flags);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int GetDataTypesMenu(BaseContainer bc, BaseContainer index, int first_menu_id, int first_sub_id, bool show_undefined_type) {
    int ret = C4dApiPINVOKE.GvWorld_GetDataTypesMenu__SWIG_1(swigCPtr, BaseContainer.getCPtr(bc), BaseContainer.getCPtr(index), first_menu_id, first_sub_id, show_undefined_type);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int GetDataTypesMenu(BaseContainer bc, BaseContainer index, int first_menu_id, int first_sub_id) {
    int ret = C4dApiPINVOKE.GvWorld_GetDataTypesMenu__SWIG_2(swigCPtr, BaseContainer.getCPtr(bc), BaseContainer.getCPtr(index), first_menu_id, first_sub_id);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int GetDataTypes(BaseContainer bc, GvDataOptions options, GvValueFlags flags) {
    int ret = C4dApiPINVOKE.GvWorld_GetDataTypes__SWIG_0(swigCPtr, BaseContainer.getCPtr(bc), (int)options, (int)flags);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int GetDataTypes(BaseContainer bc, GvDataOptions options) {
    int ret = C4dApiPINVOKE.GvWorld_GetDataTypes__SWIG_1(swigCPtr, BaseContainer.getCPtr(bc), (int)options);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int GetDataTypes(BaseContainer bc) {
    int ret = C4dApiPINVOKE.GvWorld_GetDataTypes__SWIG_2(swigCPtr, BaseContainer.getCPtr(bc));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool GetDataTypesTable(SWIGTYPE_p_p_GvDataInfo info, SWIGTYPE_p_Int32 count) {
    bool ret = C4dApiPINVOKE.GvWorld_GetDataTypesTable(swigCPtr, SWIGTYPE_p_p_GvDataInfo.getCPtr(info), SWIGTYPE_p_Int32.getCPtr(count));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int GetDataTypeIndex(int id) {
    int ret = C4dApiPINVOKE.GvWorld_GetDataTypeIndex(swigCPtr, id);
    return ret;
  }

  public GvDataInfo GetDataTypeInfo(int id) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.GvWorld_GetDataTypeInfo(swigCPtr, id);
    GvDataInfo ret = (cPtr == global::System.IntPtr.Zero) ? null : new GvDataInfo(cPtr, false);
    return ret;
  }

  public GvNodeGUI GetMasterGUI(GvNodeMaster master, uint nr) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.GvWorld_GetMasterGUI__SWIG_0(swigCPtr, GvNodeMaster.getCPtr(master), nr);
    GvNodeGUI ret = (cPtr == global::System.IntPtr.Zero) ? null : new GvNodeGUI(cPtr, false);
    return ret;
  }

  public GvNodeGUI GetMasterGUI(GvNodeMaster master) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.GvWorld_GetMasterGUI__SWIG_1(swigCPtr, GvNodeMaster.getCPtr(master));
    GvNodeGUI ret = (cPtr == global::System.IntPtr.Zero) ? null : new GvNodeGUI(cPtr, false);
    return ret;
  }

  public uint GetUniqueID() {
    uint ret = C4dApiPINVOKE.GvWorld_GetUniqueID(swigCPtr);
    return ret;
  }

  public BaseBitmap GetDefaultOperatorIcon(GvOperatorType type) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.GvWorld_GetDefaultOperatorIcon(swigCPtr, (int)type);
    BaseBitmap ret = (cPtr == global::System.IntPtr.Zero) ? null : new BaseBitmap(cPtr, false);
    return ret;
  }

  public void SetPrefs(BaseContainer bc) {
    C4dApiPINVOKE.GvWorld_SetPrefs(swigCPtr, BaseContainer.getCPtr(bc));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
  }

  public void GetPrefs(BaseContainer bc) {
    C4dApiPINVOKE.GvWorld_GetPrefs(swigCPtr, BaseContainer.getCPtr(bc));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
  }

  public int GetDataTypeNames(BaseContainer bc, SWIGTYPE_p_Int32 ids) {
    int ret = C4dApiPINVOKE.GvWorld_GetDataTypeNames(swigCPtr, BaseContainer.getCPtr(bc), SWIGTYPE_p_Int32.getCPtr(ids));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public GvNodeMaster GetMaster(int id) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.GvWorld_GetMaster(swigCPtr, id);
    GvNodeMaster ret = (cPtr == global::System.IntPtr.Zero) ? null : new GvNodeMaster(cPtr, false);
    return ret;
  }

}

}
