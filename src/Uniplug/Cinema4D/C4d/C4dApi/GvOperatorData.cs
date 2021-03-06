/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 3.0.2
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace C4d {

public class GvOperatorData : NodeData {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal GvOperatorData(global::System.IntPtr cPtr, bool cMemoryOwn) : base(C4dApiPINVOKE.GvOperatorData_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(GvOperatorData obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~GvOperatorData() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          C4dApiPINVOKE.delete_GvOperatorData(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public override bool Init(GeListNode node) {
    bool ret = C4dApiPINVOKE.GvOperatorData_Init(swigCPtr, GeListNode.getCPtr(node));
    return ret;
  }

  public override bool GetDDescription(GeListNode node, Description description, SWIGTYPE_p_DESCFLAGS_DESC flags) {
    bool ret = C4dApiPINVOKE.GvOperatorData_GetDDescription(swigCPtr, GeListNode.getCPtr(node), Description.getCPtr(description), SWIGTYPE_p_DESCFLAGS_DESC.getCPtr(flags));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override bool GetDParameter(GeListNode node, DescID id, GeData t_data, SWIGTYPE_p_DESCFLAGS_GET flags) {
    bool ret = C4dApiPINVOKE.GvOperatorData_GetDParameter(swigCPtr, GeListNode.getCPtr(node), DescID.getCPtr(id), GeData.getCPtr(t_data), SWIGTYPE_p_DESCFLAGS_GET.getCPtr(flags));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override bool GetDEnabling(GeListNode node, DescID id, GeData t_data, DESCFLAGS_ENABLE flags, BaseContainer itemdesc) {
    bool ret = C4dApiPINVOKE.GvOperatorData_GetDEnabling(swigCPtr, GeListNode.getCPtr(node), DescID.getCPtr(id), GeData.getCPtr(t_data), (int)flags, BaseContainer.getCPtr(itemdesc));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override bool SetDParameter(GeListNode node, DescID id, GeData t_data, SWIGTYPE_p_DESCFLAGS_SET flags) {
    bool ret = C4dApiPINVOKE.GvOperatorData_SetDParameter(swigCPtr, GeListNode.getCPtr(node), DescID.getCPtr(id), GeData.getCPtr(t_data), SWIGTYPE_p_DESCFLAGS_SET.getCPtr(flags));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void iGetPortList(GvNode bn, GvPortIO port, GvPortList portlist) {
    C4dApiPINVOKE.GvOperatorData_iGetPortList(swigCPtr, GvNode.getCPtr(bn), (int)port, GvPortList.getCPtr(portlist));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual bool iGetPortDescription(GvNode bn, GvPortIO port, int id, GvPortDescription pd) {
    bool ret = C4dApiPINVOKE.GvOperatorData_iGetPortDescription(swigCPtr, GvNode.getCPtr(bn), (int)port, id, GvPortDescription.getCPtr(pd));
    return ret;
  }

  public virtual bool iCreateOperator(GvNode bn) {
    bool ret = C4dApiPINVOKE.GvOperatorData_iCreateOperator(swigCPtr, GvNode.getCPtr(bn));
    return ret;
  }

  public virtual bool CreatePortDescriptionCache(GvNode bn, bool lock_only) {
    bool ret = C4dApiPINVOKE.GvOperatorData_CreatePortDescriptionCache__SWIG_0(swigCPtr, GvNode.getCPtr(bn), lock_only);
    return ret;
  }

  public virtual bool CreatePortDescriptionCache(GvNode bn) {
    bool ret = C4dApiPINVOKE.GvOperatorData_CreatePortDescriptionCache__SWIG_1(swigCPtr, GvNode.getCPtr(bn));
    return ret;
  }

  public virtual void FreePortDescriptionCache(GvNode bn) {
    C4dApiPINVOKE.GvOperatorData_FreePortDescriptionCache(swigCPtr, GvNode.getCPtr(bn));
  }

  public virtual string /* String_cstype */ GetDetailedText(GvNode bn)  {  /* <String_csout> */
      string ret = C4dApiPINVOKE.GvOperatorData_GetDetailedText(swigCPtr, GvNode.getCPtr(bn));
      return ret;
   } /* </String_csout> */ 

  public virtual string /* String_cstype */ GetText(GvNode bn)  {  /* <String_csout> */
      string ret = C4dApiPINVOKE.GvOperatorData_GetText(swigCPtr, GvNode.getCPtr(bn));
      return ret;
   } /* </String_csout> */ 

  public virtual string /* String_cstype */ GetTitle(GvNode bn)  {  /* <String_csout> */
      string ret = C4dApiPINVOKE.GvOperatorData_GetTitle(swigCPtr, GvNode.getCPtr(bn));
      return ret;
   } /* </String_csout> */ 

  public virtual BaseBitmap GetPortIcon(GvNode bn, int id, GvLayoutType layout_type) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.GvOperatorData_GetPortIcon(swigCPtr, GvNode.getCPtr(bn), id, (int)layout_type);
    BaseBitmap ret = (cPtr == global::System.IntPtr.Zero) ? null : new BaseBitmap(cPtr, false);
    return ret;
  }

  public virtual void GetBodySize(GvNode bn, SWIGTYPE_p_Int32 width, SWIGTYPE_p_Int32 height) {
    C4dApiPINVOKE.GvOperatorData_GetBodySize(swigCPtr, GvNode.getCPtr(bn), SWIGTYPE_p_Int32.getCPtr(width), SWIGTYPE_p_Int32.getCPtr(height));
  }

  public virtual Fusee.Math.double3 /* Vector_cstype_out */ GetBodyColor(GvNode bn)  {  /* <Vector_csout> */
      Fusee.Math.double3 ret = C4dApiPINVOKE.GvOperatorData_GetBodyColor(swigCPtr, GvNode.getCPtr(bn));
      return ret;
   } /* <Vector_csout> */ 

  public virtual bool GetOperatorDescription(GvNode bn, GvOperatorDescription od) {
    bool ret = C4dApiPINVOKE.GvOperatorData_GetOperatorDescription(swigCPtr, GvNode.getCPtr(bn), GvOperatorDescription.getCPtr(od));
    return ret;
  }

  public virtual void EditorDraw(GvNode bn, GvNodeGUI gui, SWIGTYPE_p_GeUserArea da, int x1, int y1, int x2, int y2) {
    C4dApiPINVOKE.GvOperatorData_EditorDraw(swigCPtr, GvNode.getCPtr(bn), GvNodeGUI.getCPtr(gui), SWIGTYPE_p_GeUserArea.getCPtr(da), x1, y1, x2, y2);
  }

  public virtual bool SceneDraw(GvNode bn, BaseDraw bd, BaseDrawHelp bh, BaseThread bt, int flags, SWIGTYPE_p_void data, uint counter) {
    bool ret = C4dApiPINVOKE.GvOperatorData_SceneDraw(swigCPtr, GvNode.getCPtr(bn), BaseDraw.getCPtr(bd), BaseDrawHelp.getCPtr(bh), BaseThread.getCPtr(bt), flags, SWIGTYPE_p_void.getCPtr(data), counter);
    return ret;
  }

  public virtual bool BodyMessage(GvNode bn, GvNodeGUI gui, int x, int y, int chn, int qua, BaseContainer msg) {
    bool ret = C4dApiPINVOKE.GvOperatorData_BodyMessage(swigCPtr, GvNode.getCPtr(bn), GvNodeGUI.getCPtr(gui), x, y, chn, qua, BaseContainer.getCPtr(msg));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual bool EditSettings(GvNode bn, GvNodeGUI gui) {
    bool ret = C4dApiPINVOKE.GvOperatorData_EditSettings(swigCPtr, GvNode.getCPtr(bn), GvNodeGUI.getCPtr(gui));
    return ret;
  }

  public virtual bool QueryCalculation(GvNode bn, GvQuery query) {
    bool ret = C4dApiPINVOKE.GvOperatorData_QueryCalculation(swigCPtr, GvNode.getCPtr(bn), GvQuery.getCPtr(query));
    return ret;
  }

  public virtual bool InitCalculation(GvNode bn, GvCalc calc, GvRun run) {
    bool ret = C4dApiPINVOKE.GvOperatorData_InitCalculation(swigCPtr, GvNode.getCPtr(bn), GvCalc.getCPtr(calc), GvRun.getCPtr(run));
    return ret;
  }

  public virtual void FreeCalculation(GvNode bn, GvCalc calc) {
    C4dApiPINVOKE.GvOperatorData_FreeCalculation(swigCPtr, GvNode.getCPtr(bn), GvCalc.getCPtr(calc));
  }

  public virtual bool Calculate(GvNode bn, GvPort port, GvRun run, GvCalc calc) {
    bool ret = C4dApiPINVOKE.GvOperatorData_Calculate(swigCPtr, GvNode.getCPtr(bn), GvPort.getCPtr(port), GvRun.getCPtr(run), GvCalc.getCPtr(calc));
    return ret;
  }

  public virtual bool AddToCalculationTable(GvNode bn, GvRun run) {
    bool ret = C4dApiPINVOKE.GvOperatorData_AddToCalculationTable(swigCPtr, GvNode.getCPtr(bn), GvRun.getCPtr(run));
    return ret;
  }

  public virtual bool SetRecalculate(GvNode bn, GvPort port, GvRun run, bool force_set) {
    bool ret = C4dApiPINVOKE.GvOperatorData_SetRecalculate__SWIG_0(swigCPtr, GvNode.getCPtr(bn), GvPort.getCPtr(port), GvRun.getCPtr(run), force_set);
    return ret;
  }

  public virtual bool SetRecalculate(GvNode bn, GvPort port, GvRun run) {
    bool ret = C4dApiPINVOKE.GvOperatorData_SetRecalculate__SWIG_1(swigCPtr, GvNode.getCPtr(bn), GvPort.getCPtr(port), GvRun.getCPtr(run));
    return ret;
  }

  public virtual bool SetData(GvNode bn, int type, SWIGTYPE_p_void data, GvOpSetDataMode mode) {
    bool ret = C4dApiPINVOKE.GvOperatorData_SetData__SWIG_0(swigCPtr, GvNode.getCPtr(bn), type, SWIGTYPE_p_void.getCPtr(data), (int)mode);
    return ret;
  }

  public virtual bool SetData(GvNode bn, int type, SWIGTYPE_p_void data) {
    bool ret = C4dApiPINVOKE.GvOperatorData_SetData__SWIG_1(swigCPtr, GvNode.getCPtr(bn), type, SWIGTYPE_p_void.getCPtr(data));
    return ret;
  }

  public virtual bool IsSetDataAllowed(GvNode bn, int type, SWIGTYPE_p_void data, GvOpSetDataMode mode) {
    bool ret = C4dApiPINVOKE.GvOperatorData_IsSetDataAllowed__SWIG_0(swigCPtr, GvNode.getCPtr(bn), type, SWIGTYPE_p_void.getCPtr(data), (int)mode);
    return ret;
  }

  public virtual bool IsSetDataAllowed(GvNode bn, int type, SWIGTYPE_p_void data) {
    bool ret = C4dApiPINVOKE.GvOperatorData_IsSetDataAllowed__SWIG_1(swigCPtr, GvNode.getCPtr(bn), type, SWIGTYPE_p_void.getCPtr(data));
    return ret;
  }

  public virtual int FillPortMenu(GvNode bn, BaseContainer menu, int port_id, int first_menu_id) {
    int ret = C4dApiPINVOKE.GvOperatorData_FillPortMenu(swigCPtr, GvNode.getCPtr(bn), BaseContainer.getCPtr(menu), port_id, first_menu_id);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual bool PortMenuCommand(GvNode bn, int menu_id, int port_id, int mx, int my) {
    bool ret = C4dApiPINVOKE.GvOperatorData_PortMenuCommand(swigCPtr, GvNode.getCPtr(bn), menu_id, port_id, mx, my);
    return ret;
  }

  public virtual int FillOperatorMenu(GvNode bn, BaseContainer menu, int first_menu_id) {
    int ret = C4dApiPINVOKE.GvOperatorData_FillOperatorMenu(swigCPtr, GvNode.getCPtr(bn), BaseContainer.getCPtr(menu), first_menu_id);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual bool OperatorMenuCommand(GvNode bn, int menu_id, int mx, int my) {
    bool ret = C4dApiPINVOKE.GvOperatorData_OperatorMenuCommand(swigCPtr, GvNode.getCPtr(bn), menu_id, mx, my);
    return ret;
  }

  public virtual int FillPortsMenu(GvNode bn, BaseContainer names, BaseContainer ids, int value_type, GvPortIO port, int first_menu_id) {
    int ret = C4dApiPINVOKE.GvOperatorData_FillPortsMenu(swigCPtr, GvNode.getCPtr(bn), BaseContainer.getCPtr(names), BaseContainer.getCPtr(ids), value_type, (int)port, first_menu_id);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual string /* String_cstype */ GetErrorString(GvNode bn, int error)  {  /* <String_csout> */
      string ret = C4dApiPINVOKE.GvOperatorData_GetErrorString(swigCPtr, GvNode.getCPtr(bn), error);
      return ret;
   } /* </String_csout> */ 

  public virtual int GetMainID(GvNode bn, GvPortIO io, DescID desc_id) {
    int ret = C4dApiPINVOKE.GvOperatorData_GetMainID(swigCPtr, GvNode.getCPtr(bn), (int)io, DescID.getCPtr(desc_id));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual DescID GetDescID(GvNode bn, GvPortIO io, int main_id) {
    DescID ret = new DescID(C4dApiPINVOKE.GvOperatorData_GetDescID(swigCPtr, GvNode.getCPtr(bn), (int)io, main_id), true);
    return ret;
  }

  public GvOperatorData() : this(C4dApiPINVOKE.new_GvOperatorData(), true) {
  }

}

}
