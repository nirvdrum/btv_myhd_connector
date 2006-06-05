﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.42
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.42.
// 
#pragma warning disable 1591

namespace BTV_MyHD_Connector.BTVServer {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="BTVDispatcherSoap", Namespace="http://www.snapstream.com/WebService")]
    public partial class BTVDispatcher : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetActiveRecordingsOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetActiveRecordingsWithChannelOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetEnginesOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetEnginesWithChannelOperationCompleted;
        
        private System.Threading.SendOrPostCallback SetSourcesOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public BTVDispatcher() {
            this.Url = global::BTV_MyHD_Connector.Properties.Settings.Default.BTV_MyHD_Connector_WebDispatcher_BTVDispatcher;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetActiveRecordingsCompletedEventHandler GetActiveRecordingsCompleted;
        
        /// <remarks/>
        public event GetActiveRecordingsWithChannelCompletedEventHandler GetActiveRecordingsWithChannelCompleted;
        
        /// <remarks/>
        public event GetEnginesCompletedEventHandler GetEnginesCompleted;
        
        /// <remarks/>
        public event GetEnginesWithChannelCompletedEventHandler GetEnginesWithChannelCompleted;
        
        /// <remarks/>
        public event SetSourcesCompletedEventHandler SetSourcesCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.snapstream.com/WebService/GetActiveRecordings", RequestNamespace="http://www.snapstream.com/WebService", ResponseNamespace="http://www.snapstream.com/WebService", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public PVSPropertyBag[] GetActiveRecordings() {
            object[] results = this.Invoke("GetActiveRecordings", new object[0]);
            return ((PVSPropertyBag[])(results[0]));
        }
        
        /// <remarks/>
        public void GetActiveRecordingsAsync() {
            this.GetActiveRecordingsAsync(null);
        }
        
        /// <remarks/>
        public void GetActiveRecordingsAsync(object userState) {
            if ((this.GetActiveRecordingsOperationCompleted == null)) {
                this.GetActiveRecordingsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetActiveRecordingsOperationCompleted);
            }
            this.InvokeAsync("GetActiveRecordings", new object[0], this.GetActiveRecordingsOperationCompleted, userState);
        }
        
        private void OnGetActiveRecordingsOperationCompleted(object arg) {
            if ((this.GetActiveRecordingsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetActiveRecordingsCompleted(this, new GetActiveRecordingsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.snapstream.com/WebService/GetActiveRecordingsWithChannel", RequestNamespace="http://www.snapstream.com/WebService", ResponseNamespace="http://www.snapstream.com/WebService", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public PVSPropertyBag[] GetActiveRecordingsWithChannel(string uniqueChannelID) {
            object[] results = this.Invoke("GetActiveRecordingsWithChannel", new object[] {
                        uniqueChannelID});
            return ((PVSPropertyBag[])(results[0]));
        }
        
        /// <remarks/>
        public void GetActiveRecordingsWithChannelAsync(string uniqueChannelID) {
            this.GetActiveRecordingsWithChannelAsync(uniqueChannelID, null);
        }
        
        /// <remarks/>
        public void GetActiveRecordingsWithChannelAsync(string uniqueChannelID, object userState) {
            if ((this.GetActiveRecordingsWithChannelOperationCompleted == null)) {
                this.GetActiveRecordingsWithChannelOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetActiveRecordingsWithChannelOperationCompleted);
            }
            this.InvokeAsync("GetActiveRecordingsWithChannel", new object[] {
                        uniqueChannelID}, this.GetActiveRecordingsWithChannelOperationCompleted, userState);
        }
        
        private void OnGetActiveRecordingsWithChannelOperationCompleted(object arg) {
            if ((this.GetActiveRecordingsWithChannelCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetActiveRecordingsWithChannelCompleted(this, new GetActiveRecordingsWithChannelCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.snapstream.com/WebService/GetEngines", RequestNamespace="http://www.snapstream.com/WebService", ResponseNamespace="http://www.snapstream.com/WebService", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public PVSPropertyBag[] GetEngines() {
            object[] results = this.Invoke("GetEngines", new object[0]);
            return ((PVSPropertyBag[])(results[0]));
        }
        
        /// <remarks/>
        public void GetEnginesAsync() {
            this.GetEnginesAsync(null);
        }
        
        /// <remarks/>
        public void GetEnginesAsync(object userState) {
            if ((this.GetEnginesOperationCompleted == null)) {
                this.GetEnginesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetEnginesOperationCompleted);
            }
            this.InvokeAsync("GetEngines", new object[0], this.GetEnginesOperationCompleted, userState);
        }
        
        private void OnGetEnginesOperationCompleted(object arg) {
            if ((this.GetEnginesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetEnginesCompleted(this, new GetEnginesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.snapstream.com/WebService/GetEnginesWithChannel", RequestNamespace="http://www.snapstream.com/WebService", ResponseNamespace="http://www.snapstream.com/WebService", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public PVSPropertyBag[] GetEnginesWithChannel(string uniqueChannelID) {
            object[] results = this.Invoke("GetEnginesWithChannel", new object[] {
                        uniqueChannelID});
            return ((PVSPropertyBag[])(results[0]));
        }
        
        /// <remarks/>
        public void GetEnginesWithChannelAsync(string uniqueChannelID) {
            this.GetEnginesWithChannelAsync(uniqueChannelID, null);
        }
        
        /// <remarks/>
        public void GetEnginesWithChannelAsync(string uniqueChannelID, object userState) {
            if ((this.GetEnginesWithChannelOperationCompleted == null)) {
                this.GetEnginesWithChannelOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetEnginesWithChannelOperationCompleted);
            }
            this.InvokeAsync("GetEnginesWithChannel", new object[] {
                        uniqueChannelID}, this.GetEnginesWithChannelOperationCompleted, userState);
        }
        
        private void OnGetEnginesWithChannelOperationCompleted(object arg) {
            if ((this.GetEnginesWithChannelCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetEnginesWithChannelCompleted(this, new GetEnginesWithChannelCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.snapstream.com/WebService/SetSources", RequestNamespace="http://www.snapstream.com/WebService", ResponseNamespace="http://www.snapstream.com/WebService", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void SetSources(PVSPropertyBag[] sources) {
            this.Invoke("SetSources", new object[] {
                        sources});
        }
        
        /// <remarks/>
        public void SetSourcesAsync(PVSPropertyBag[] sources) {
            this.SetSourcesAsync(sources, null);
        }
        
        /// <remarks/>
        public void SetSourcesAsync(PVSPropertyBag[] sources, object userState) {
            if ((this.SetSourcesOperationCompleted == null)) {
                this.SetSourcesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSetSourcesOperationCompleted);
            }
            this.InvokeAsync("SetSources", new object[] {
                        sources}, this.SetSourcesOperationCompleted, userState);
        }
        
        private void OnSetSourcesOperationCompleted(object arg) {
            if ((this.SetSourcesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SetSourcesCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    ///// <remarks/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.42")]
    //[System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.snapstream.com/types")]
    //public partial class PVSPropertyBag {
        
    //    private PVSProperty[] propertiesField;
        
    //    /// <remarks/>
    //    public PVSProperty[] Properties {
    //        get {
    //            return this.propertiesField;
    //        }
    //        set {
    //            this.propertiesField = value;
    //        }
    //    }
    //}
    
    ///// <remarks/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.42")]
    //[System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.snapstream.com/types")]
    //public partial class PVSProperty {
        
    //    private string nameField;
        
    //    private string valueField;
        
    //    /// <remarks/>
    //    public string Name {
    //        get {
    //            return this.nameField;
    //        }
    //        set {
    //            this.nameField = value;
    //        }
    //    }
        
    //    /// <remarks/>
    //    public string Value {
    //        get {
    //            return this.valueField;
    //        }
    //        set {
    //            this.valueField = value;
    //        }
    //    }
    //}
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void GetActiveRecordingsCompletedEventHandler(object sender, GetActiveRecordingsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetActiveRecordingsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetActiveRecordingsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public PVSPropertyBag[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((PVSPropertyBag[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void GetActiveRecordingsWithChannelCompletedEventHandler(object sender, GetActiveRecordingsWithChannelCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetActiveRecordingsWithChannelCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetActiveRecordingsWithChannelCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public PVSPropertyBag[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((PVSPropertyBag[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void GetEnginesCompletedEventHandler(object sender, GetEnginesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetEnginesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetEnginesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public PVSPropertyBag[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((PVSPropertyBag[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void GetEnginesWithChannelCompletedEventHandler(object sender, GetEnginesWithChannelCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetEnginesWithChannelCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetEnginesWithChannelCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public PVSPropertyBag[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((PVSPropertyBag[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void SetSourcesCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591