﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompletarCitaDPW.WsCompletarCita {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WsCompletarCita.CompletarCitasExtraportuarioSoap")]
    public interface CompletarCitasExtraportuarioSoap {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento usuario del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CompletarCita", ReplyAction="*")]
        CompletarCitaDPW.WsCompletarCita.CompletarCitaResponse CompletarCita(CompletarCitaDPW.WsCompletarCita.CompletarCitaRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CompletarCita", ReplyAction="*")]
        System.Threading.Tasks.Task<CompletarCitaDPW.WsCompletarCita.CompletarCitaResponse> CompletarCitaAsync(CompletarCitaDPW.WsCompletarCita.CompletarCitaRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CompletarCitaRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CompletarCita", Namespace="http://tempuri.org/", Order=0)]
        public CompletarCitaDPW.WsCompletarCita.CompletarCitaRequestBody Body;
        
        public CompletarCitaRequest() {
        }
        
        public CompletarCitaRequest(CompletarCitaDPW.WsCompletarCita.CompletarCitaRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class CompletarCitaRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string usuario;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string clave;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string numerocita;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string contenedor;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string isoType;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string placa;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string dni;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string ructercerizada;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=8)]
        public string precinto1;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=9)]
        public string precinto2;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=10)]
        public string precinto3;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=11)]
        public string precinto4;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=12)]
        public string peso;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=13)]
        public string tara;
        
        public CompletarCitaRequestBody() {
        }
        
        public CompletarCitaRequestBody(string usuario, string clave, string numerocita, string contenedor, string isoType, string placa, string dni, string ructercerizada, string precinto1, string precinto2, string precinto3, string precinto4, string peso, string tara) {
            this.usuario = usuario;
            this.clave = clave;
            this.numerocita = numerocita;
            this.contenedor = contenedor;
            this.isoType = isoType;
            this.placa = placa;
            this.dni = dni;
            this.ructercerizada = ructercerizada;
            this.precinto1 = precinto1;
            this.precinto2 = precinto2;
            this.precinto3 = precinto3;
            this.precinto4 = precinto4;
            this.peso = peso;
            this.tara = tara;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CompletarCitaResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CompletarCitaResponse", Namespace="http://tempuri.org/", Order=0)]
        public CompletarCitaDPW.WsCompletarCita.CompletarCitaResponseBody Body;
        
        public CompletarCitaResponse() {
        }
        
        public CompletarCitaResponse(CompletarCitaDPW.WsCompletarCita.CompletarCitaResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class CompletarCitaResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string CompletarCitaResult;
        
        public CompletarCitaResponseBody() {
        }
        
        public CompletarCitaResponseBody(string CompletarCitaResult) {
            this.CompletarCitaResult = CompletarCitaResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CompletarCitasExtraportuarioSoapChannel : CompletarCitaDPW.WsCompletarCita.CompletarCitasExtraportuarioSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CompletarCitasExtraportuarioSoapClient : System.ServiceModel.ClientBase<CompletarCitaDPW.WsCompletarCita.CompletarCitasExtraportuarioSoap>, CompletarCitaDPW.WsCompletarCita.CompletarCitasExtraportuarioSoap {
        
        public CompletarCitasExtraportuarioSoapClient() {
        }
        
        public CompletarCitasExtraportuarioSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CompletarCitasExtraportuarioSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CompletarCitasExtraportuarioSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CompletarCitasExtraportuarioSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        CompletarCitaDPW.WsCompletarCita.CompletarCitaResponse CompletarCitaDPW.WsCompletarCita.CompletarCitasExtraportuarioSoap.CompletarCita(CompletarCitaDPW.WsCompletarCita.CompletarCitaRequest request) {
            return base.Channel.CompletarCita(request);
        }
        
        public string CompletarCita(string usuario, string clave, string numerocita, string contenedor, string isoType, string placa, string dni, string ructercerizada, string precinto1, string precinto2, string precinto3, string precinto4, string peso, string tara) {
            CompletarCitaDPW.WsCompletarCita.CompletarCitaRequest inValue = new CompletarCitaDPW.WsCompletarCita.CompletarCitaRequest();
            inValue.Body = new CompletarCitaDPW.WsCompletarCita.CompletarCitaRequestBody();
            inValue.Body.usuario = usuario;
            inValue.Body.clave = clave;
            inValue.Body.numerocita = numerocita;
            inValue.Body.contenedor = contenedor;
            inValue.Body.isoType = isoType;
            inValue.Body.placa = placa;
            inValue.Body.dni = dni;
            inValue.Body.ructercerizada = ructercerizada;
            inValue.Body.precinto1 = precinto1;
            inValue.Body.precinto2 = precinto2;
            inValue.Body.precinto3 = precinto3;
            inValue.Body.precinto4 = precinto4;
            inValue.Body.peso = peso;
            inValue.Body.tara = tara;
            CompletarCitaDPW.WsCompletarCita.CompletarCitaResponse retVal = ((CompletarCitaDPW.WsCompletarCita.CompletarCitasExtraportuarioSoap)(this)).CompletarCita(inValue);
            return retVal.Body.CompletarCitaResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<CompletarCitaDPW.WsCompletarCita.CompletarCitaResponse> CompletarCitaDPW.WsCompletarCita.CompletarCitasExtraportuarioSoap.CompletarCitaAsync(CompletarCitaDPW.WsCompletarCita.CompletarCitaRequest request) {
            return base.Channel.CompletarCitaAsync(request);
        }
        
        public System.Threading.Tasks.Task<CompletarCitaDPW.WsCompletarCita.CompletarCitaResponse> CompletarCitaAsync(string usuario, string clave, string numerocita, string contenedor, string isoType, string placa, string dni, string ructercerizada, string precinto1, string precinto2, string precinto3, string precinto4, string peso, string tara) {
            CompletarCitaDPW.WsCompletarCita.CompletarCitaRequest inValue = new CompletarCitaDPW.WsCompletarCita.CompletarCitaRequest();
            inValue.Body = new CompletarCitaDPW.WsCompletarCita.CompletarCitaRequestBody();
            inValue.Body.usuario = usuario;
            inValue.Body.clave = clave;
            inValue.Body.numerocita = numerocita;
            inValue.Body.contenedor = contenedor;
            inValue.Body.isoType = isoType;
            inValue.Body.placa = placa;
            inValue.Body.dni = dni;
            inValue.Body.ructercerizada = ructercerizada;
            inValue.Body.precinto1 = precinto1;
            inValue.Body.precinto2 = precinto2;
            inValue.Body.precinto3 = precinto3;
            inValue.Body.precinto4 = precinto4;
            inValue.Body.peso = peso;
            inValue.Body.tara = tara;
            return ((CompletarCitaDPW.WsCompletarCita.CompletarCitasExtraportuarioSoap)(this)).CompletarCitaAsync(inValue);
        }
    }
}
