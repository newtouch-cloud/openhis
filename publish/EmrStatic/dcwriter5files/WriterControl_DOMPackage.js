"use strict";

/** .NET对象在JS中的封装 2024-12-13*/
export let WriterControl_DOMPackage = {

    /**
     *  创建.NET对象在JS中的强类型封装
     * @param {HTMLElement} rootElement 编辑器控件对象
     * @param {any} instance .NET对象
     * @param {string} typeName 类型名称
     * @returns 创建的JS封装包，如果未能封装则返回对象本身
     */
    CreatePackage: function (rootElement, instance, typeName) {
        var result = instance;
        if (instance != null && instance.invokeMethod && typeName != null) {
            var handler = WriterControl_DOMPackage.InstanceConstructors[typeName];
            if (typeof (handler) == "function") {
                result = handler(instance);
            }
        }
        if (result != null && typeof (result) == "object") {
            result.WriterControl = rootElement;
        }
        return result;
    },
    /**
     * 判断是否支持指定的类型
     * @param {string} typeName 类型名称
     * @returns {boolean} 是否支持
     */
    IsSupport: function (typeName) {
        return typeof (WriterControl_DOMPackage.InstanceConstructors[typeName]) == "function";
    },
    InstanceConstructors: {
        "PromptProtectedContentEventArgs": function (instance) { return new PromptProtectedContentEventArgs(instance); },
        "CanInsertObjectEventArgs": function (instance) { return new CanInsertObjectEventArgs(instance); },
        "InsertObjectEventArgs": function (instance) { return new InsertObjectEventArgs(instance); },
        "QueryListItemsEventArgs": function (instance) { return new QueryListItemsEventArgs(instance); },
        "WriterMouseEventArgs": function (instance) { return new WriterMouseEventArgs(instance); },
        "WriterSectionElementCancelEventArgs": function (instance) { return new WriterSectionElementCancelEventArgs(instance); },
        "XTextElement": function (instance) { return new XTextElement(instance); },
        "ContentChangedEventArgs": function (instance) { return new ContentChangedEventArgs(instance); },
        "ContentChangingEventArgs": function (instance) { return new ContentChangingEventArgs(instance); },
        "ElementEventArgs": function (instance) { return new ElementEventArgs(instance); },
        "WriterEventArgs": function (instance) { return new WriterEventArgs(instance); },
        "WriterSectionElementEventArgs": function (instance) { return new WriterSectionElementEventArgs(instance); },
        "XTextContainerElement": function (instance) { return new XTextContainerElement(instance); },
        "ElementCancelEventArgs": function (instance) { return new ElementCancelEventArgs(instance); },
        "ElementExpressionEventArgs": function (instance) { return new ElementExpressionEventArgs(instance); },
        "ElementKeyEventArgs": function (instance) { return new ElementKeyEventArgs(instance); },
        "ElementMouseEventArgs": function (instance) { return new ElementMouseEventArgs(instance); },
        "ElementValidatingEventArgs": function (instance) { return new ElementValidatingEventArgs(instance); },
        "WriterButtonClickEventArgs": function (instance) { return new WriterButtonClickEventArgs(instance); },
        "WriterExpressionFunctionEventArgs": function (instance) { return new WriterExpressionFunctionEventArgs(instance); },
        "WriterStartEditEventArgs": function (instance) { return new WriterStartEditEventArgs(instance); },
        "WriterTableRowHeightChangedEventArgs": function (instance) { return new WriterTableRowHeightChangedEventArgs(instance); },
        "WriterTableRowHeightChangingEventArgs": function (instance) { return new WriterTableRowHeightChangingEventArgs(instance); }
    }
};
//DCSoft.Writer.Commands.JSPackCodeGen 自动创建于2023-06-20 14:34:19
// CanInsertObjectEventArgs|InsertObjectEventArgs|QueryListItemsEventArgs|WriterMouseEventArgs|WriterSectionElementCancelEventArgs|XTextElement|ContentChangedEventArgs|ContentChangingEventArgs|ElementEventArgs|WriterEventArgs|WriterSectionElementEventArgs|XTextContainerElement|ElementCancelEventArgs|ElementExpressionEventArgs|ElementKeyEventArgs|ElementMouseEventArgs|ElementValidatingEventArgs|WriterButtonClickEventArgs|WriterExpressionFunctionEventArgs|WriterStartEditEventArgs|WriterTableRowHeightChangedEventArgs|WriterTableRowHeightChangingEventArgs

/** 类型 DCSoft.Writer.PromptProtectedContentEventArgs 的JS封装 */
export class PromptProtectedContentEventArgs {
    constructor(obj) {
        if (obj == null) throw "PromptProtectedContentEventArgs:参数为空";
        this.__TypeName == "PromptProtectedContentEventArgs";
        this.__Target = obj;
        this.Message = this.__Target.invokeMethod("get_Message");
        this.ElementName = this.__Target.invokeMethod("get_ElementName");
        this.ElementID = this.__Target.invokeMethod("get_ElementID");
        this.ElementTypeName = this.__Target.invokeMethod("get_ElementTypeName");
        this.PromptMode = this.__Target.invokeMethod("get_PromptMode"); 
    }
    get Handled() { return this.__Target.invokeMethod("get_Handled"); }
    set Handled(v) { this.__Target.invokeMethod("set_Handled", v); }
    //get PromptMode() { return this.__Target.invokeMethod("get_PromptMode"); }
    //get ElementTypeName() { return this.__Target.invokeMethod("get_ElementTypeName"); }
    //get ElementID() { return this.__Target.invokeMethod("get_ElementID"); }
    //get ElementName() { return this.__Target.invokeMethod("get_ElementName"); }
};

/** 类型 DCSoft.Writer.CanInsertObjectEventArgs 的JS封装 */
export class CanInsertObjectEventArgs {
    constructor(obj) {
        if (obj == null) throw "CanInsertObjectEventArgs:参数为空";
        this.__TypeName == "CanInsertObjectEventArgs";
        this.__Target = obj;
        this.AllowDataFormats = this.__Target.invokeMethod("get_AllowDataFormats");
        this.Position = this.__Target.invokeMethod("get_Position");
        this.SpecifyPosition = this.__Target.invokeMethod("get_SpecifyPosition");
    }
    get Handled() { return this.__Target.invokeMethod("get_Handled"); }
    set Handled(v) { return this.__Target.invokeMethod("set_Handled", v); }
    get Result() { return this.__Target.invokeMethod("get_Result"); }
    set Result(v) { return this.__Target.invokeMethod("set_Result", v); }
    get SpecifyFormat() { return this.__Target.invokeMethod("get_SpecifyFormat"); }
    set SpecifyFormat(v) { return this.__Target.invokeMethod("set_SpecifyFormat", v); }
    /**
    * @param {string} format
    * @returns {boolean}
    */
    GetDataPresent(format) {
        return this.__Target.invokeMethod("GetDataPresent", format);
    }
    /**
    * @returns {Array} 原始类型 System.String[]
    */
    GetFormats() {
        return this.__Target.invokeMethod("GetFormats",);
    }
};


/** 类型 DCSoft.Writer.InsertObjectEventArgs 的JS封装 */
export class InsertObjectEventArgs {
    constructor(obj) {
        if (obj == null) throw "InsertObjectEventArgs:参数为空";
        this.__TypeName == "InsertObjectEventArgs";
        this.__Target = obj;
        this.AllowDataFormats = this.__Target.invokeMethod("get_AllowDataFormats");
        this.AllowedEffect = this.__Target.invokeMethod("get_AllowedEffect");
        this.AutoSelectContent = this.__Target.invokeMethod("get_AutoSelectContent");
        this.CheckMaxTextLengthForCopyPaste = this.__Target.invokeMethod("get_CheckMaxTextLengthForCopyPaste");
        this.ContainerElementID = this.__Target.invokeMethod("get_ContainerElementID");
        this.ContainerElementName = this.__Target.invokeMethod("get_ContainerElementName");
        this.DetectForDragContent = this.__Target.invokeMethod("get_DetectForDragContent");
        this.DragEffect = this.__Target.invokeMethod("get_DragEffect");
        this.InputSource = this.__Target.invokeMethod("get_InputSource");
        this.Position = this.__Target.invokeMethod("get_Position");
        this.ShowUI = this.__Target.invokeMethod("get_ShowUI");
    }
    get Handled() { return this.__Target.invokeMethod("get_Handled"); }
    set Handled(v) { return this.__Target.invokeMethod("set_Handled", v); }
    get Result() { return this.__Target.invokeMethod("get_Result"); }
    set Result(v) { return this.__Target.invokeMethod("set_Result", v); }
    /**
    * @param {string} format
    * @returns {any} 原始类型 System.Object
    */
    GetData(format) {
        return this.__Target.invokeMethod("GetData", format);
    }
    /**
    * @param {string} format
    * @returns {boolean}
    */
    GetDataPresent(format) {
        return this.__Target.invokeMethod("GetDataPresent", format);
    }
    /**
    * @returns {Array} 原始类型 System.String[]
    */
    GetFormats() {
        return this.__Target.invokeMethod("GetFormats",);
    }
};


/** 类型 DCSoft.Writer.Controls.QueryListItemsEventArgs 的JS封装 */
export class QueryListItemsEventArgs {
    constructor(obj) {
        if (obj == null) throw "QueryListItemsEventArgs:参数为空";
        this.__TypeName == "QueryListItemsEventArgs";
        this.__Target = obj;
        this.BufferItems = this.__Target.invokeMethod("get_BufferItems");
        this.ElementID = this.__Target.invokeMethod("get_ElementID");
        this.ElementName = this.__Target.invokeMethod("get_ElementName");
        this.ListSourceName = this.__Target.invokeMethod("get_ListSourceName");
        this.PreText = this.__Target.invokeMethod("get_PreText");
        this.SpecifyValue = this.__Target.invokeMethod("get_SpecifyValue");
        this.SpellCode = this.__Target.invokeMethod("get_SpellCode");
    }
    get Handled() { return this.__Target.invokeMethod("get_Handled"); }
    set Handled(v) { return this.__Target.invokeMethod("set_Handled", v); }
    /**
    * @param {any} item 原始类型为DCSoft.Writer.Data.ListItem
    */
    AddResultItem(item) {
        return this.__Target.invokeMethod("AddResultItem", item);
    }
    /**
    * @param {string} strText
    * @param {string} strValue
    */
    AddResultItemByTextValue(strText, strValue) {
        return this.__Target.invokeMethod("AddResultItemByTextValue", strText, strValue);
    }
    /**
    * @param {string} strText
    * @param {string} strValue
    * @param {string} strTextInList
    */
    AddResultItemByTextValueTextInList(strText, strValue, strTextInList) {
        return this.__Target.invokeMethod("AddResultItemByTextValueTextInList", strText, strValue, strTextInList);
    }
};


/** 类型 DCSoft.Writer.WriterMouseEventArgs 的JS封装 */
export class WriterMouseEventArgs {
    constructor(obj) {
        if (obj == null) throw "WriterMouseEventArgs:参数为空";
        this.__TypeName == "WriterMouseEventArgs";
        this.__Target = obj;
        this.Button = this.__Target.invokeMethod("get_Button");
        this.Clicks = this.__Target.invokeMethod("get_Clicks");
        this.Delta = this.__Target.invokeMethod("get_Delta");
        this.HasLeftButton = this.__Target.invokeMethod("get_HasLeftButton");
        this.HasRightButton = this.__Target.invokeMethod("get_HasRightButton");
        this.X = this.__Target.invokeMethod("get_X");
        this.Y = this.__Target.invokeMethod("get_Y");
    }
    get Handled() { return this.__Target.invokeMethod("get_Handled"); }
    set Handled(v) { return this.__Target.invokeMethod("set_Handled", v); }
};


/** 类型 DCSoft.Writer.WriterSectionElementCancelEventArgs 的JS封装 */
export class WriterSectionElementCancelEventArgs {
    constructor(obj) {
        if (obj == null) throw "WriterSectionElementCancelEventArgs:参数为空";
        this.__TypeName == "WriterSectionElementCancelEventArgs";
        this.__Target = obj;
        this.ElementID = this.__Target.invokeMethod("get_ElementID");
        this.ElementName = this.__Target.invokeMethod("get_ElementName");
    }
    get Cancel() { return this.__Target.invokeMethod("get_Cancel"); }
    set Cancel(v) { return this.__Target.invokeMethod("set_Cancel", v); }
};


/** 类型 DCSoft.Writer.Dom.XTextElement 的JS封装 */
export class XTextElement {
    constructor(obj) {
        if (obj == null) throw "XTextElement:参数为空";
        this.__TypeName == "XTextElement";
        this.__Target = obj;
        this.AbsLeft = this.__Target.invokeMethod("get_AbsLeft");
        this.AbsTop = this.__Target.invokeMethod("get_AbsTop");
        this.Bottom = this.__Target.invokeMethod("get_Bottom");
        this.ClientHeight = this.__Target.invokeMethod("get_ClientHeight");
        this.ClientWidth = this.__Target.invokeMethod("get_ClientWidth");
        this.ColumnIndex = this.__Target.invokeMethod("get_ColumnIndex");
        this.ContentIndex = this.__Target.invokeMethod("get_ContentIndex");
        this.ContentVersion = this.__Target.invokeMethod("get_ContentVersion");
        this.CreatorPermessionLevel = this.__Target.invokeMethod("get_CreatorPermessionLevel");
        this.DeleterPermissionLevel = this.__Target.invokeMethod("get_DeleterPermissionLevel");
        //this.ElementInstanceIndex = this.__Target.invokeMethod("get_ElementInstanceIndex");
        this.Focused = this.__Target.invokeMethod("get_Focused");
        this.HasSelection = this.__Target.invokeMethod("get_HasSelection");
        this.Height = this.__Target.invokeMethod("get_Height");
        this.IsLogicDeleted = this.__Target.invokeMethod("get_IsLogicDeleted");
        this.IsNewInputContent = this.__Target.invokeMethod("get_IsNewInputContent");
        this.Left = this.__Target.invokeMethod("get_Left");
        this.OwnerLastPageIndex = this.__Target.invokeMethod("get_OwnerLastPageIndex");
        this.OwnerPageIndex = this.__Target.invokeMethod("get_OwnerPageIndex");
        this.OwnerPagePartyStyle = this.__Target.invokeMethod("get_OwnerPagePartyStyle");
        this.PixelClientWidth = this.__Target.invokeMethod("get_PixelClientWidth");
        this.Right = this.__Target.invokeMethod("get_Right");
        this.RuntimeVisible = this.__Target.invokeMethod("get_RuntimeVisible");
        this.Top = this.__Target.invokeMethod("get_Top");
        this.Width = this.__Target.invokeMethod("get_Width");
    }
    get ElementIndex() { return this.__Target.invokeMethod("get_ElementIndex"); }
    set ElementIndex(v) { return this.__Target.invokeMethod("set_ElementIndex", v); }
    get FormulaValue() { return this.__Target.invokeMethod("get_FormulaValue"); }
    set FormulaValue(v) { return this.__Target.invokeMethod("set_FormulaValue", v); }
    get ID() { return this.__Target.invokeMethod("get_ID"); }
    set ID(v) { return this.__Target.invokeMethod("set_ID", v); }
    get InnerID() { return this.__Target.invokeMethod("get_InnerID"); }
    set InnerID(v) { return this.__Target.invokeMethod("set_InnerID", v); }
    get InnerText() { return this.__Target.invokeMethod("get_InnerText"); }
    set InnerText(v) { return this.__Target.invokeMethod("set_InnerText", v); }
    get OuterText() { return this.__Target.invokeMethod("get_OuterText"); }
    set OuterText(v) { return this.__Target.invokeMethod("set_OuterText", v); }
    get StyleIndex() { return this.__Target.invokeMethod("get_StyleIndex"); }
    set StyleIndex(v) { return this.__Target.invokeMethod("set_StyleIndex", v); }
    get Text() { return this.__Target.invokeMethod("get_Text"); }
    set Text(v) { return this.__Target.invokeMethod("set_Text", v); }
    get Visible() { return this.__Target.invokeMethod("get_Visible"); }
    set Visible(v) { return this.__Target.invokeMethod("set_Visible", v); }
    /**
    * @returns {string}
    */
    DispalyTypeName() {
        return this.__Target.invokeMethod("DispalyTypeName",);
    }
    /**
    */
    EditorRefreshView() {
        return this.__Target.invokeMethod("EditorRefreshView",);
    }
    /**
    * @param {boolean} fastMode
    */
    EditorRefreshViewExt(fastMode) {
        return this.__Target.invokeMethod("EditorRefreshViewExt", fastMode);
    }
    /**
    * @param {number} width
    * @param {number} height
    * @param {boolean} updateView
    * @param {boolean} logUndo
    * @returns {boolean}
    */
    EditorSetSize(width, height, updateView, logUndo) {
        return this.__Target.invokeMethod("EditorSetSize", width, height, updateView, logUndo);
    }
    /**
    * @param {boolean} visible
    * @returns {boolean}
    */
    EditorSetVisible(visible) {
        return this.__Target.invokeMethod("EditorSetVisible", visible);
    }
    /**
    * @param {boolean} visible
    * @param {boolean} logUndo
    * @param {boolean} fastMode
    * @returns {boolean}
    */
    EditorSetVisibleExt(visible, logUndo, fastMode) {
        return this.__Target.invokeMethod("EditorSetVisibleExt", visible, logUndo, fastMode);
    }
    /**
    * @param {string} name
    * @returns {string}
    */
    ExtGetPropertyValue(name) {
        return this.__Target.invokeMethod("ExtGetPropertyValue", name);
    }
    /**
    * @param {string} name
    * @param {string} strValue
    * @returns {boolean}
    */
    ExtSetPropertyValue(name, strValue) {
        return this.__Target.invokeMethod("ExtSetPropertyValue", name, strValue);
    }
    /**
    */
    Focus() {
        return this.__Target.invokeMethod("Focus",);
    }
    /**
    * @param {string} name
    * @returns {string}
    */
    GetAttribute(name) {
        return this.__Target.invokeMethod("GetAttribute", name);
    }
    /**
    * @param {any} args 原始类型为DCSoft.Writer.Dom.GetTextArgs
    * @returns {string}
    */
    GetText(args) {
        return this.__Target.invokeMethod("GetText", args);
    }
    /**
    * @returns {string}
    */
    GetXMLFragment() {
        return this.__Target.invokeMethod("GetXMLFragment",);
    }
    /**
    * @param {string} name
    * @returns {boolean}
    */
    HasAttribute(name) {
        return this.__Target.invokeMethod("HasAttribute", name);
    }
    /**
    */
    InvalidateView() {
        return this.__Target.invokeMethod("InvalidateView",);
    }
    /**
    * @returns {boolean}
    */
    Select() {
        return this.__Target.invokeMethod("Select",);
    }
    /**
    * @param {string} name
    * @param {string} Value
    * @returns {boolean}
    */
    SetAttribute(name, Value) {
        return this.__Target.invokeMethod("SetAttribute", name, Value);
    }
    /**
    * @returns {string}
    */
    ToDebugString() {
        return this.__Target.invokeMethod("ToDebugString",);
    }
    /**
    * @returns {string}
    */
    ToPlaintString() {
        return this.__Target.invokeMethod("ToPlaintString",);
    }
    /**
    * @returns {string}
    */
    ToString() {
        return this.__Target.invokeMethod("ToString",);
    }
};


/** 类型 DCSoft.Writer.ContentChangedEventArgs 的JS封装 */
export class ContentChangedEventArgs {
    constructor(obj) {
        if (obj == null) throw "ContentChangedEventArgs:参数为空";
        this.__TypeName == "ContentChangedEventArgs";
        this.__Target = obj;
        this.ElementID = this.__Target.invokeMethod("get_ElementID");
        this.ElementIndex = this.__Target.invokeMethod("get_ElementIndex");
        this.ElementName = this.__Target.invokeMethod("get_ElementName");
        this.EventSource = this.__Target.invokeMethod("get_EventSource");
        this.OnlyStyleChanged = this.__Target.invokeMethod("get_OnlyStyleChanged");
        this.UndoRedoCause = this.__Target.invokeMethod("get_UndoRedoCause");
    }
    get CancelBubble() { return this.__Target.invokeMethod("get_CancelBubble"); }
    set CancelBubble(v) { return this.__Target.invokeMethod("set_CancelBubble", v); }
    get Handled() { return this.__Target.invokeMethod("get_Handled"); }
    set Handled(v) { return this.__Target.invokeMethod("set_Handled", v); }
};


/** 类型 DCSoft.Writer.ContentChangingEventArgs 的JS封装 */
export class ContentChangingEventArgs {
    constructor(obj) {
        if (obj == null) throw "ContentChangingEventArgs:参数为空";
        this.__TypeName == "ContentChangingEventArgs";
        this.__Target = obj;
        this.ElementID = this.__Target.invokeMethod("get_ElementID");
        this.ElementIndex = this.__Target.invokeMethod("get_ElementIndex");
        this.ElementName = this.__Target.invokeMethod("get_ElementName");
    }
    get Cancel() { return this.__Target.invokeMethod("get_Cancel"); }
    set Cancel(v) { return this.__Target.invokeMethod("set_Cancel", v); }
    get CancelBubble() { return this.__Target.invokeMethod("get_CancelBubble"); }
    set CancelBubble(v) { return this.__Target.invokeMethod("set_CancelBubble", v); }
    get Handled() { return this.__Target.invokeMethod("get_Handled"); }
    set Handled(v) { return this.__Target.invokeMethod("set_Handled", v); }
    /**
    * @returns {string}
    */
    GetContainerNewText() {
        return this.__Target.invokeMethod("GetContainerNewText",);
    }
};


/** 类型 DCSoft.Writer.ElementEventArgs 的JS封装 */
export class ElementEventArgs {
    constructor(obj) {
        if (obj == null) throw "ElementEventArgs:参数为空";
        this.__TypeName == "ElementEventArgs";
        this.__Target = obj;
        this.ElementID = obj.invokeMethod("get_ElementID");
        //this.ElementInstanceIndex = this.__Target.invokeMethod("get_ElementInstanceIndex");
        this.ElementName = obj.invokeMethod("get_ElementName");
        this.ElementTypeName = obj.invokeMethod("get_ElementTypeName");
        this.ElementHashCode = obj.invokeMethod("get_ElementHashCode");
    }
    get TargetElement() {
        return this.WriterControl.__DCWriterReference.invokeMethod(
            "GetElementByHashCode",
            this.ElementHashCode);
    }
    get CancelBubble() { return this.__Target.invokeMethod("get_CancelBubble"); }
    set CancelBubble(v) { return this.__Target.invokeMethod("set_CancelBubble", v); }
    get Handled() { return this.__Target.invokeMethod("get_Handled"); }
    set Handled(v) { return this.__Target.invokeMethod("set_Handled", v); }
};


/** 类型 DCSoft.Writer.WriterEventArgs 的JS封装 */
export class WriterEventArgs {
    constructor(obj) {
        if (obj == null) throw "WriterEventArgs:参数为空";
        this.__TypeName == "WriterEventArgs";
        this.__Target = obj;
        this.ElementID = obj.invokeMethod("get_ElementID");
        this.ElementName = obj.invokeMethod("get_ElementName");
        this.ElementTypeName = obj.invokeMethod("get_ElementTypeName");
        this.ElementHashCode = obj.invokeMethod("get_ElementHashCode");
    }
    get TargetElement() {
        return this.WriterControl.__DCWriterReference.invokeMethod(
            "GetElementByHashCode",
            this.ElementHashCode);
    }
};


/** 类型 DCSoft.Writer.WriterSectionElementEventArgs 的JS封装 */
export class WriterSectionElementEventArgs {
    constructor(obj) {
        if (obj == null) throw "WriterSectionElementEventArgs:参数为空";
        this.__TypeName == "WriterSectionElementEventArgs";
        this.__Target = obj;
        this.ElementID = this.__Target.invokeMethod("get_ElementID");
        this.ElementName = this.__Target.invokeMethod("get_ElementName");
    }
};


/** 类型 DCSoft.Writer.Dom.XTextContainerElement 的JS封装 */
export class XTextContainerElement extends XTextElement {
    constructor(obj) {
        if (obj == null) throw "XTextContainerElement:参数为空";
        super(obj);
        this.__TypeName == "XTextContainerElement";
        this.__Target = obj;
        this.ElementsCount = this.__Target.invokeMethod("get_ElementsCount");
        this.HasSelection = this.__Target.invokeMethod("get_HasSelection");
    }
    get AcceptChildElementTypes2() { return this.__Target.invokeMethod("get_AcceptChildElementTypes2"); }
    set AcceptChildElementTypes2(v) { return this.__Target.invokeMethod("set_AcceptChildElementTypes2", v); }
    get AcceptTab() { return this.__Target.invokeMethod("get_AcceptTab"); }
    set AcceptTab(v) { return this.__Target.invokeMethod("set_AcceptTab", v); }
    get AutoFixTextMode() { return this.__Target.invokeMethod("get_AutoFixTextMode"); }
    set AutoFixTextMode(v) { return this.__Target.invokeMethod("set_AutoFixTextMode", v); }
    get AutoHideMode() { return this.__Target.invokeMethod("get_AutoHideMode"); }
    set AutoHideMode(v) { return this.__Target.invokeMethod("set_AutoHideMode", v); }
    get BringoutToSave() { return this.__Target.invokeMethod("get_BringoutToSave"); }
    set BringoutToSave(v) { return this.__Target.invokeMethod("set_BringoutToSave", v); }
    get CanBeReferenced() { return this.__Target.invokeMethod("get_CanBeReferenced"); }
    set CanBeReferenced(v) { return this.__Target.invokeMethod("set_CanBeReferenced", v); }
    get ContentLock() { return this.__Target.invokeMethod("get_ContentLock"); }
    set ContentLock(v) { return this.__Target.invokeMethod("set_ContentLock", v); }
    get ContentReadonly() { return this.__Target.invokeMethod("get_ContentReadonly"); }
    set ContentReadonly(v) { return this.__Target.invokeMethod("set_ContentReadonly", v); }
    get ContentReadonlyExpression() { return this.__Target.invokeMethod("get_ContentReadonlyExpression"); }
    set ContentReadonlyExpression(v) { return this.__Target.invokeMethod("set_ContentReadonlyExpression", v); }
    get CopySource() { return this.__Target.invokeMethod("get_CopySource"); }
    set CopySource(v) { return this.__Target.invokeMethod("set_CopySource", v); }
    get DataFeedback() { return this.__Target.invokeMethod("get_DataFeedback"); }
    set DataFeedback(v) { return this.__Target.invokeMethod("set_DataFeedback", v); }
    get DataName() { return this.__Target.invokeMethod("get_DataName"); }
    set DataName(v) { return this.__Target.invokeMethod("set_DataName", v); }
    get DefaultValueForValueBinding() { return this.__Target.invokeMethod("get_DefaultValueForValueBinding"); }
    set DefaultValueForValueBinding(v) { return this.__Target.invokeMethod("set_DefaultValueForValueBinding", v); }
    get Deleteable() { return this.__Target.invokeMethod("get_Deleteable"); }
    set Deleteable(v) { return this.__Target.invokeMethod("set_Deleteable", v); }
    get EditorText() { return this.__Target.invokeMethod("get_EditorText"); }
    set EditorText(v) { return this.__Target.invokeMethod("set_EditorText", v); }
    get EditorTextExt() { return this.__Target.invokeMethod("get_EditorTextExt"); }
    set EditorTextExt(v) { return this.__Target.invokeMethod("set_EditorTextExt", v); }
    get ElementIDForEditableDependent() { return this.__Target.invokeMethod("get_ElementIDForEditableDependent"); }
    set ElementIDForEditableDependent(v) { return this.__Target.invokeMethod("set_ElementIDForEditableDependent", v); }
    get EmitDataFieldName() { return this.__Target.invokeMethod("get_EmitDataFieldName"); }
    set EmitDataFieldName(v) { return this.__Target.invokeMethod("set_EmitDataFieldName", v); }
    get EmitDataSource() { return this.__Target.invokeMethod("get_EmitDataSource"); }
    set EmitDataSource(v) { return this.__Target.invokeMethod("set_EmitDataSource", v); }
    get EnablePermission() { return this.__Target.invokeMethod("get_EnablePermission"); }
    set EnablePermission(v) { return this.__Target.invokeMethod("set_EnablePermission", v); }
    get EnableValueValidate() { return this.__Target.invokeMethod("get_EnableValueValidate"); }
    set EnableValueValidate(v) { return this.__Target.invokeMethod("set_EnableValueValidate", v); }
    get EncryptContent() { return this.__Target.invokeMethod("get_EncryptContent"); }
    set EncryptContent(v) { return this.__Target.invokeMethod("set_EncryptContent", v); }
    get FormulaValue() { return this.__Target.invokeMethod("get_FormulaValue"); }
    set FormulaValue(v) { return this.__Target.invokeMethod("set_FormulaValue", v); }
    get HiddenPrintWhenEmpty() { return this.__Target.invokeMethod("get_HiddenPrintWhenEmpty"); }
    set HiddenPrintWhenEmpty(v) { return this.__Target.invokeMethod("set_HiddenPrintWhenEmpty", v); }
    get InnerText() { return this.__Target.invokeMethod("get_InnerText"); }
    set InnerText(v) { return this.__Target.invokeMethod("set_InnerText", v); }
    get JavaScriptForClick() { return this.__Target.invokeMethod("get_JavaScriptForClick"); }
    set JavaScriptForClick(v) { return this.__Target.invokeMethod("set_JavaScriptForClick", v); }
    get JavaScriptForDoubleClick() { return this.__Target.invokeMethod("get_JavaScriptForDoubleClick"); }
    set JavaScriptForDoubleClick(v) { return this.__Target.invokeMethod("set_JavaScriptForDoubleClick", v); }
    get LimitedInputChars() { return this.__Target.invokeMethod("get_LimitedInputChars"); }
    set LimitedInputChars(v) { return this.__Target.invokeMethod("set_LimitedInputChars", v); }
    get MaxInputLength() { return this.__Target.invokeMethod("get_MaxInputLength"); }
    set MaxInputLength(v) { return this.__Target.invokeMethod("set_MaxInputLength", v); }
    get Modified() { return this.__Target.invokeMethod("get_Modified"); }
    set Modified(v) { return this.__Target.invokeMethod("set_Modified", v); }
    get OuterText() { return this.__Target.invokeMethod("get_OuterText"); }
    set OuterText(v) { return this.__Target.invokeMethod("set_OuterText", v); }
    get PrintVisibility() { return this.__Target.invokeMethod("get_PrintVisibility"); }
    set PrintVisibility(v) { return this.__Target.invokeMethod("set_PrintVisibility", v); }
    get PrintVisibilityExpression() { return this.__Target.invokeMethod("get_PrintVisibilityExpression"); }
    set PrintVisibilityExpression(v) { return this.__Target.invokeMethod("set_PrintVisibilityExpression", v); }
    get PropertyExpressions() { return this.__Target.invokeMethod("get_PropertyExpressions"); }
    set PropertyExpressions(v) { return this.__Target.invokeMethod("set_PropertyExpressions", v); }
    get ReferencedDataName() { return this.__Target.invokeMethod("get_ReferencedDataName"); }
    set ReferencedDataName(v) { return this.__Target.invokeMethod("set_ReferencedDataName", v); }
    get Text() { return this.__Target.invokeMethod("get_Text"); }
    set Text(v) { return this.__Target.invokeMethod("set_Text", v); }
    get ToolTip() { return this.__Target.invokeMethod("get_ToolTip"); }
    set ToolTip(v) { return this.__Target.invokeMethod("set_ToolTip", v); }
    get TransparentEncryptMode() { return this.__Target.invokeMethod("get_TransparentEncryptMode"); }
    set TransparentEncryptMode(v) { return this.__Target.invokeMethod("set_TransparentEncryptMode", v); }
    get ValidateStyle() { return this.__Target.invokeMethod("get_ValidateStyle"); }
    set ValidateStyle(v) { return this.__Target.invokeMethod("set_ValidateStyle", v); }
    get ValueBinding() { return this.__Target.invokeMethod("get_ValueBinding"); }
    set ValueBinding(v) { return this.__Target.invokeMethod("set_ValueBinding", v); }
    get ValueExpression() { return this.__Target.invokeMethod("get_ValueExpression"); }
    set ValueExpression(v) { return this.__Target.invokeMethod("set_ValueExpression", v); }
    get Visible() { return this.__Target.invokeMethod("get_Visible"); }
    set Visible(v) { return this.__Target.invokeMethod("set_Visible", v); }
    get VisibleExpression() { return this.__Target.invokeMethod("get_VisibleExpression"); }
    set VisibleExpression(v) { return this.__Target.invokeMethod("set_VisibleExpression", v); }
    /**
    * @returns {boolean}
    */
    CanResign() {
        return this.__Target.invokeMethod("CanResign",);
    }
    /**
    * @returns {boolean}
    */
    ClearSign() {
        return this.__Target.invokeMethod("ClearSign",);
    }
    /**
    * @returns {boolean}
    */
    CommitUserTrace() {
        return this.__Target.invokeMethod("CommitUserTrace",);
    }
    /**
    * @returns {boolean}
    */
    DeleteAllSign() {
        return this.__Target.invokeMethod("DeleteAllSign",);
    }
    /**
    * @returns {string}
    */
    DispalyTypeName() {
        return this.__Target.invokeMethod("DispalyTypeName",);
    }
    /**
    * @param {boolean} logUndo
    * @returns {boolean}
    */
    EditorDelete(logUndo) {
        return this.__Target.invokeMethod("EditorDelete", logUndo);
    }
    /**
    * @param {boolean} logUndo
    * @returns {boolean}
    */
    EditorDeletePreserveContent(logUndo) {
        return this.__Target.invokeMethod("EditorDeletePreserveContent", logUndo);
    }
    /**
    */
    EditorRefreshView() {
        return this.__Target.invokeMethod("EditorRefreshView",);
    }
    /**
    * @param {boolean} fastMode
    */
    EditorRefreshViewExt(fastMode) {
        return this.__Target.invokeMethod("EditorRefreshViewExt", fastMode);
    }
    /**
    * @param {number} width
    * @param {number} height
    * @param {boolean} updateView
    * @param {boolean} logUndo
    * @returns {boolean}
    */
    EditorSetSize(width, height, updateView, logUndo) {
        return this.__Target.invokeMethod("EditorSetSize", width, height, updateView, logUndo);
    }
    /**
    * @param {boolean} visible
    * @returns {boolean}
    */
    EditorSetVisible(visible) {
        return this.__Target.invokeMethod("EditorSetVisible", visible);
    }
    /**
    * @param {boolean} visible
    * @param {boolean} logUndo
    * @param {boolean} fastMode
    * @returns {boolean}
    */
    EditorSetVisibleExt(visible, logUndo, fastMode) {
        return this.__Target.invokeMethod("EditorSetVisibleExt", visible, logUndo, fastMode);
    }
    /**
    * @param {string} name
    * @returns {string}
    */
    ExtGetPropertyValue(name) {
        return this.__Target.invokeMethod("ExtGetPropertyValue", name);
    }
    /**
    * @param {string} name
    * @param {string} strValue
    * @returns {boolean}
    */
    ExtSetPropertyValue(name, strValue) {
        return this.__Target.invokeMethod("ExtSetPropertyValue", name, strValue);
    }
    /**
    * @param {string} name
    * @returns {string}
    */
    GetAttribute(name) {
        return this.__Target.invokeMethod("GetAttribute", name);
    }
    /**
    * @param {any} args 原始类型为DCSoft.Writer.Dom.GetTextArgs
    * @returns {string}
    */
    GetText(args) {
        return this.__Target.invokeMethod("GetText", args);
    }
    /**
    * @returns {string}
    */
    GetXMLFragment() {
        return this.__Target.invokeMethod("GetXMLFragment",);
    }
    /**
    * @param {string} name
    * @returns {boolean}
    */
    HasAttribute(name) {
        return this.__Target.invokeMethod("HasAttribute", name);
    }
    /**
    */
    InvalidateView() {
        return this.__Target.invokeMethod("InvalidateView",);
    }
    /**
    * @returns {boolean}
    */
    ReSign() {
        return this.__Target.invokeMethod("ReSign",);
    }
    /**
    * @param {any} mode 原始类型为DCSoft.Common.DCCASignMode
    * @returns {boolean}
    */
    ReSignSpecifyMode(mode) {
        return this.__Target.invokeMethod("ReSignSpecifyMode", mode);
    }
    /**
    * @param {string} text
    * @param {boolean} ignoreCase
    * @param {number} maxResultCount
    * @returns {any} 原始类型 DCSoft.Writer.Dom.SearchStringResultList
    */
    SearchString(text, ignoreCase, maxResultCount) {
        return this.__Target.invokeMethod("SearchString", text, ignoreCase, maxResultCount);
    }
    /**
    * @returns {boolean}
    */
    Select() {
        return this.__Target.invokeMethod("Select",);
    }
    /**
    * @param {string} name
    * @param {string} Value
    * @returns {boolean}
    */
    SetAttribute(name, Value) {
        return this.__Target.invokeMethod("SetAttribute", name, Value);
    }
    /**
    * @param {string} userID
    * @param {string} authoriseUserIDList
    * @param {boolean} logUndo
    * @returns {boolean}
    */
    SetContentLock(userID, authoriseUserIDList, logUndo) {
        return this.__Target.invokeMethod("SetContentLock", userID, authoriseUserIDList, logUndo);
    }
    /**
    * @returns {boolean}
    */
    SetContentLockByCurrentUser() {
        return this.__Target.invokeMethod("SetContentLockByCurrentUser",);
    }
    /**
    * @param {string} newText
    * @param {any} flags 原始类型为DCSoft.Writer.Dom.DomAccessFlags
    * @param {boolean} disablePermissioin
    * @param {boolean} updateContent
    * @returns {boolean}
    */
    SetEditorTextExt(newText, flags, disablePermissioin, updateContent) {
        return this.__Target.invokeMethod("SetEditorTextExt", newText, flags, disablePermissioin, updateContent);
    }
    /**
    * @param {string} text
    * @param {boolean} ignoreCase
    * @param {any} foreColor
    * @param {any} backColor
    * @param {boolean} supportPrint
    * @param {boolean} supportPDF
    * @returns {any} 原始类型 DCSoft.Writer.Dom.SearchStringResultList
    */
    SetTextHighlight(text, ignoreCase, foreColor, backColor, supportPrint, supportPDF) {
        return this.__Target.invokeMethod("SetTextHighlight", text, ignoreCase, foreColor, backColor, supportPrint, supportPDF);
    }
    /**
    * @param {string} text
    * @param {number} textStyleIndex
    * @param {number} paragraphStyleIndex
    */
    SetTextRawDOM(text, textStyleIndex, paragraphStyleIndex) {
        return this.__Target.invokeMethod("SetTextRawDOM", text, textStyleIndex, paragraphStyleIndex);
    }
    /**
    * @param {any} input 原始类型为DCSoft.Writer.Security.DCSignInputInfo
    * @returns {boolean}
    */
    Sign(input) {
        return this.__Target.invokeMethod("Sign", input);
    }
    /**
    * @returns {string}
    */
    ToDebugString() {
        return this.__Target.invokeMethod("ToDebugString",);
    }
    /**
    * @returns {string}
    */
    ToPlaintString() {
        return this.__Target.invokeMethod("ToPlaintString",);
    }
    /**
    * @returns {string}
    */
    ToString() {
        return this.__Target.invokeMethod("ToString",);
    }
    /**
    * @param {boolean} loadingDocument
    * @returns {any} 原始类型 DCSoft.Writer.Dom.ValueValidateResult
    */
    Validating(loadingDocument) {
        return this.__Target.invokeMethod("Validating", loadingDocument);
    }
};


/** 类型 DCSoft.Writer.ElementCancelEventArgs 的JS封装 */
export class ElementCancelEventArgs extends ElementEventArgs {
    constructor(obj) {
        if (obj == null) throw "ElementCancelEventArgs:参数为空";
        super(obj);
        this.__TypeName == "ElementCancelEventArgs";
        this.__Target = obj;
    }
    get Cancel() { return this.__Target.invokeMethod("get_Cancel"); }
    set Cancel(v) { return this.__Target.invokeMethod("set_Cancel", v); }
};


/** 类型 DCSoft.Writer.ElementExpressionEventArgs 的JS封装 */
export class ElementExpressionEventArgs extends ElementEventArgs {
    constructor(obj) {
        if (obj == null) throw "ElementExpressionEventArgs:参数为空";
        super(obj);
        this.__TypeName == "ElementExpressionEventArgs";
        this.__Target = obj;
        this.Expression = this.__Target.invokeMethod("get_Expression");
    }
    get Result() { return this.__Target.invokeMethod("get_Result"); }
    set Result(v) { return this.__Target.invokeMethod("set_Result", v); }
};


/** 类型 DCSoft.Writer.ElementKeyEventArgs 的JS封装 */
export class ElementKeyEventArgs extends ElementEventArgs {
    constructor(obj) {
        if (obj == null) throw "ElementKeyEventArgs:参数为空";
        super(obj);
        this.__TypeName == "ElementKeyEventArgs";
        this.__Target = obj;
        this.Alt = this.__Target.invokeMethod("get_Alt");
        this.Control = this.__Target.invokeMethod("get_Control");
        this.KeyChar = this.__Target.invokeMethod("get_KeyChar");
        this.KeyCharValue = this.__Target.invokeMethod("get_KeyCharValue");
        this.KeyCode = this.__Target.invokeMethod("get_KeyCode");
        this.KeyCodeValue = this.__Target.invokeMethod("get_KeyCodeValue");
        this.Shift = this.__Target.invokeMethod("get_Shift");
    }
};


/** 类型 DCSoft.Writer.ElementMouseEventArgs 的JS封装 */
export class ElementMouseEventArgs extends ElementEventArgs {
    constructor(obj) {
        if (obj == null) throw "ElementMouseEventArgs:参数为空";
        super(obj);
        this.__TypeName == "ElementMouseEventArgs";
        this.__Target = obj;
        this.Button = this.__Target.invokeMethod("get_Button");
        this.Clicks = this.__Target.invokeMethod("get_Clicks");
        this.Delta = this.__Target.invokeMethod("get_Delta");
        this.DocumentX = this.__Target.invokeMethod("get_DocumentX");
        this.DocumentY = this.__Target.invokeMethod("get_DocumentY");
        this.ElementClientX = this.__Target.invokeMethod("get_ElementClientX");
        this.ElementClientY = this.__Target.invokeMethod("get_ElementClientY");
        this.HasLeftButton = this.__Target.invokeMethod("get_HasLeftButton");
        this.HasRightButton = this.__Target.invokeMethod("get_HasRightButton");
    }
};


/** 类型 DCSoft.Writer.ElementValidatingEventArgs 的JS封装 */
export class ElementValidatingEventArgs extends ElementEventArgs {
    constructor(obj) {
        if (obj == null) throw "ElementValidatingEventArgs:参数为空";
        super(obj);
        this.__TypeName == "ElementValidatingEventArgs";
        this.__Target = obj;
    }
    get Cancel() { return this.__Target.invokeMethod("get_Cancel"); }
    set Cancel(v) { return this.__Target.invokeMethod("set_Cancel", v); }
    get Message() { return this.__Target.invokeMethod("get_Message"); }
    set Message(v) { return this.__Target.invokeMethod("set_Message", v); }
    get ResultLevel() { return this.__Target.invokeMethod("get_ResultLevel"); }
    set ResultLevel(v) { return this.__Target.invokeMethod("set_ResultLevel", v); }
    get ResultState() { return this.__Target.invokeMethod("get_ResultState"); }
    set ResultState(v) { return this.__Target.invokeMethod("set_ResultState", v); }
};


/** 类型 DCSoft.Writer.WriterButtonClickEventArgs 的JS封装 */
export class WriterButtonClickEventArgs extends WriterEventArgs {
    constructor(obj) {
        if (obj == null) throw "WriterButtonClickEventArgs:参数为空";
        super(obj);
        this.__TypeName == "WriterButtonClickEventArgs";
        this.__Target = obj;
        this.ButtonElementID = this.__Target.invokeMethod("get_ButtonElementID");
        this.CommandName = this.__Target.invokeMethod("get_CommandName");
        this.ScriptTextForClick = this.__Target.invokeMethod("get_ScriptTextForClick");
    }
    get Handled() { return this.__Target.invokeMethod("get_Handled"); }
    set Handled(v) { return this.__Target.invokeMethod("set_Handled", v); }
};


/** 类型 DCSoft.Writer.WriterExpressionFunctionEventArgs 的JS封装 */
export class WriterExpressionFunctionEventArgs extends WriterEventArgs {
    constructor(obj) {
        if (obj == null) throw "WriterExpressionFunctionEventArgs:参数为空";
        super(obj);
        this.__TypeName == "WriterExpressionFunctionEventArgs";
        this.__Target = obj;
        this.FunctionName = this.__Target.invokeMethod("get_FunctionName");
        this.ParametersCount = this.__Target.invokeMethod("get_ParametersCount");
        this.ParameterString1 = this.__Target.invokeMethod("get_ParameterString1");
        this.ParameterString2 = this.__Target.invokeMethod("get_ParameterString2");
        this.ParameterString3 = this.__Target.invokeMethod("get_ParameterString3");
        this.ParameterString4 = this.__Target.invokeMethod("get_ParameterString4");
    }
    get ErrorMessage() { return this.__Target.invokeMethod("get_ErrorMessage"); }
    set ErrorMessage(v) { return this.__Target.invokeMethod("set_ErrorMessage", v); }
    get Handled() { return this.__Target.invokeMethod("get_Handled"); }
    set Handled(v) { return this.__Target.invokeMethod("set_Handled", v); }
    get ResultString() { return this.__Target.invokeMethod("get_ResultString"); }
    set ResultString(v) { return this.__Target.invokeMethod("set_ResultString", v); }
    /**
    * @param {number} index
    * @returns {string}
    */
    GetParameterStringValue(index) {
        return this.__Target.invokeMethod("GetParameterStringValue", index);
    }
};


/** 类型 DCSoft.Writer.WriterStartEditEventArgs 的JS封装 */
export class WriterStartEditEventArgs extends WriterEventArgs {
    constructor(obj) {
        if (obj == null) throw "WriterStartEditEventArgs:参数为空";
        super(obj);
        this.__TypeName == "WriterStartEditEventArgs";
        this.__Target = obj;
        this.ContainerElementID = this.__Target.invokeMethod("get_ContainerElementID");
        this.ContainerElementName = this.__Target.invokeMethod("get_ContainerElementName");
        this.DocumentID = this.__Target.invokeMethod("get_DocumentID");
    }
    get CanDetectAgain() { return this.__Target.invokeMethod("get_CanDetectAgain"); }
    set CanDetectAgain(v) { return this.__Target.invokeMethod("set_CanDetectAgain", v); }
    get Readonly() { return this.__Target.invokeMethod("get_Readonly"); }
    set Readonly(v) { return this.__Target.invokeMethod("set_Readonly", v); }
    get ReloadDocument() { return this.__Target.invokeMethod("get_ReloadDocument"); }
    set ReloadDocument(v) { return this.__Target.invokeMethod("set_ReloadDocument", v); }
};


/** 类型 DCSoft.Writer.WriterTableRowHeightChangedEventArgs 的JS封装 */
export class WriterTableRowHeightChangedEventArgs extends WriterEventArgs {
    constructor(obj) {
        if (obj == null) throw "WriterTableRowHeightChangedEventArgs:参数为空";
        super(obj);
        this.__TypeName == "WriterTableRowHeightChangedEventArgs";
        this.__Target = obj;
        this.NewHeight = this.__Target.invokeMethod("get_NewHeight");
        this.OldHeight = this.__Target.invokeMethod("get_OldHeight");
    }
};


/** 类型 DCSoft.Writer.WriterTableRowHeightChangingEventArgs 的JS封装 */
export class WriterTableRowHeightChangingEventArgs extends WriterEventArgs {
    constructor(obj) {
        if (obj == null) throw "WriterTableRowHeightChangingEventArgs:参数为空";
        super(obj);
        this.__TypeName == "WriterTableRowHeightChangingEventArgs";
        this.__Target = obj;
        this.OldHeight = this.__Target.invokeMethod("get_OldHeight");
    }
    get Cancel() { return this.__Target.invokeMethod("get_Cancel"); }
    set Cancel(v) { return this.__Target.invokeMethod("set_Cancel", v); }
    get NewHeight() { return this.__Target.invokeMethod("get_NewHeight"); }
    set NewHeight(v) { return this.__Target.invokeMethod("set_NewHeight", v); }
};

