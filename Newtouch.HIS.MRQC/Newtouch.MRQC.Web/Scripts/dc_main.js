//var __ServicePageUrl2022 = "http://localhost:7027/EMR/MoreHandleDCWriterServicePage";
(function(a,b){function cy(a){return f.isWindow(a)?a:a.nodeType===9?a.defaultView||a.parentWindow:!1}function cu(a){if(!cj[a]){var b=c.body,d=f("<"+a+">").appendTo(b),e=d.css("display");d.remove();if(e==="none"||e===""){ck||(ck=c.createElement("iframe"),ck.frameBorder=ck.width=ck.height=0),b.appendChild(ck);if(!cl||!ck.createElement)cl=(ck.contentWindow||ck.contentDocument).document,cl.write((f.support.boxModel?"<!doctype html>":"")+"<html><body>"),cl.close();d=cl.createElement(a),cl.body.appendChild(d),e=f.css(d,"display"),b.removeChild(ck)}cj[a]=e}return cj[a]}function ct(a,b){var c={};f.each(cp.concat.apply([],cp.slice(0,b)),function(){c[this]=a});return c}function cs(){cq=b}function cr(){setTimeout(cs,0);return cq=f.now()}function ci(){try{return new a.ActiveXObject("Microsoft.XMLHTTP")}catch(b){}}function ch(){try{return new a.XMLHttpRequest}catch(b){}}function cb(a,c){a.dataFilter&&(c=a.dataFilter(c,a.dataType));var d=a.dataTypes,e={},g,h,i=d.length,j,k=d[0],l,m,n,o,p;for(g=1;g<i;g++){if(g===1)for(h in a.converters)typeof h=="string"&&(e[h.toLowerCase()]=a.converters[h]);l=k,k=d[g];if(k==="*")k=l;else if(l!=="*"&&l!==k){m=l+" "+k,n=e[m]||e["* "+k];if(!n){p=b;for(o in e){j=o.split(" ");if(j[0]===l||j[0]==="*"){p=e[j[1]+" "+k];if(p){o=e[o],o===!0?n=p:p===!0&&(n=o);break}}}}!n&&!p&&f.error("No conversion from "+m.replace(" "," to ")),n!==!0&&(c=n?n(c):p(o(c)))}}return c}function ca(a,c,d){var e=a.contents,f=a.dataTypes,g=a.responseFields,h,i,j,k;for(i in g)i in d&&(c[g[i]]=d[i]);while(f[0]==="*")f.shift(),h===b&&(h=a.mimeType||c.getResponseHeader("content-type"));if(h)for(i in e)if(e[i]&&e[i].test(h)){f.unshift(i);break}if(f[0]in d)j=f[0];else{for(i in d){if(!f[0]||a.converters[i+" "+f[0]]){j=i;break}k||(k=i)}j=j||k}if(j){j!==f[0]&&f.unshift(j);return d[j]}}function b_(a,b,c,d){if(f.isArray(b))f.each(b,function(b,e){c||bD.test(a)?d(a,e):b_(a+"["+(typeof e=="object"?b:"")+"]",e,c,d)});else if(!c&&f.type(b)==="object")for(var e in b)b_(a+"["+e+"]",b[e],c,d);else d(a,b)}function b$(a,c){var d,e,g=f.ajaxSettings.flatOptions||{};for(d in c)c[d]!==b&&((g[d]?a:e||(e={}))[d]=c[d]);e&&f.extend(!0,a,e)}function bZ(a,c,d,e,f,g){f=f||c.dataTypes[0],g=g||{},g[f]=!0;var h=a[f],i=0,j=h?h.length:0,k=a===bS,l;for(;i<j&&(k||!l);i++)l=h[i](c,d,e),typeof l=="string"&&(!k||g[l]?l=b:(c.dataTypes.unshift(l),l=bZ(a,c,d,e,l,g)));(k||!l)&&!g["*"]&&(l=bZ(a,c,d,e,"*",g));return l}function bY(a){return function(b,c){typeof b!="string"&&(c=b,b="*");if(f.isFunction(c)){var d=b.toLowerCase().split(bO),e=0,g=d.length,h,i,j;for(;e<g;e++)h=d[e],j=/^\+/.test(h),j&&(h=h.substr(1)||"*"),i=a[h]=a[h]||[],i[j?"unshift":"push"](c)}}}function bB(a,b,c){var d=b==="width"?a.offsetWidth:a.offsetHeight,e=b==="width"?1:0,g=4;if(d>0){if(c!=="border")for(;e<g;e+=2)c||(d-=parseFloat(f.css(a,"padding"+bx[e]))||0),c==="margin"?d+=parseFloat(f.css(a,c+bx[e]))||0:d-=parseFloat(f.css(a,"border"+bx[e]+"Width"))||0;return d+"px"}d=by(a,b);if(d<0||d==null)d=a.style[b];if(bt.test(d))return d;d=parseFloat(d)||0;if(c)for(;e<g;e+=2)d+=parseFloat(f.css(a,"padding"+bx[e]))||0,c!=="padding"&&(d+=parseFloat(f.css(a,"border"+bx[e]+"Width"))||0),c==="margin"&&(d+=parseFloat(f.css(a,c+bx[e]))||0);return d+"px"}function bo(a){var b=c.createElement("div");bh.appendChild(b),b.innerHTML=a.outerHTML;return b.firstChild}function bn(a){var b=(a.nodeName||"").toLowerCase();b==="input"?bm(a):b!=="script"&&typeof a.getElementsByTagName!="undefined"&&f.grep(a.getElementsByTagName("input"),bm)}function bm(a){if(a.type==="checkbox"||a.type==="radio")a.defaultChecked=a.checked}function bl(a){return typeof a.getElementsByTagName!="undefined"?a.getElementsByTagName("*"):typeof a.querySelectorAll!="undefined"?a.querySelectorAll("*"):[]}function bk(a,b){var c;b.nodeType===1&&(b.clearAttributes&&b.clearAttributes(),b.mergeAttributes&&b.mergeAttributes(a),c=b.nodeName.toLowerCase(),c==="object"?b.outerHTML=a.outerHTML:c!=="input"||a.type!=="checkbox"&&a.type!=="radio"?c==="option"?b.selected=a.defaultSelected:c==="input"||c==="textarea"?b.defaultValue=a.defaultValue:c==="script"&&b.text!==a.text&&(b.text=a.text):(a.checked&&(b.defaultChecked=b.checked=a.checked),b.value!==a.value&&(b.value=a.value)),b.removeAttribute(f.expando),b.removeAttribute("_submit_attached"),b.removeAttribute("_change_attached"))}function bj(a,b){if(b.nodeType===1&&!!f.hasData(a)){var c,d,e,g=f._data(a),h=f._data(b,g),i=g.events;if(i){delete h.handle,h.events={};for(c in i)for(d=0,e=i[c].length;d<e;d++)f.event.add(b,c,i[c][d])}h.data&&(h.data=f.extend({},h.data))}}function bi(a,b){return f.nodeName(a,"table")?a.getElementsByTagName("tbody")[0]||a.appendChild(a.ownerDocument.createElement("tbody")):a}function U(a){var b=V.split("|"),c=a.createDocumentFragment();if(c.createElement)while(b.length)c.createElement(b.pop());return c}function T(a,b,c){b=b||0;if(f.isFunction(b))return f.grep(a,function(a,d){var e=!!b.call(a,d,a);return e===c});if(b.nodeType)return f.grep(a,function(a,d){return a===b===c});if(typeof b=="string"){var d=f.grep(a,function(a){return a.nodeType===1});if(O.test(b))return f.filter(b,d,!c);b=f.filter(b,d)}return f.grep(a,function(a,d){return f.inArray(a,b)>=0===c})}function S(a){return!a||!a.parentNode||a.parentNode.nodeType===11}function K(){return!0}function J(){return!1}function n(a,b,c){var d=b+"defer",e=b+"queue",g=b+"mark",h=f._data(a,d);h&&(c==="queue"||!f._data(a,e))&&(c==="mark"||!f._data(a,g))&&setTimeout(function(){!f._data(a,e)&&!f._data(a,g)&&(f.removeData(a,d,!0),h.fire())},0)}function m(a){for(var b in a){if(b==="data"&&f.isEmptyObject(a[b]))continue;if(b!=="toJSON")return!1}return!0}function l(a,c,d){if(d===b&&a.nodeType===1){var e="data-"+c.replace(k,"-$1").toLowerCase();d=a.getAttribute(e);if(typeof d=="string"){try{d=d==="true"?!0:d==="false"?!1:d==="null"?null:f.isNumeric(d)?+d:j.test(d)?f.parseJSON(d):d}catch(g){}f.data(a,c,d)}else d=b}return d}function h(a){var b=g[a]={},c,d;a=a.split(/\s+/);for(c=0,d=a.length;c<d;c++)b[a[c]]=!0;return b}var c=a.document,d=a.navigator,e=a.location,f=function(){function J(){if(!e.isReady){try{c.documentElement.doScroll("left")}catch(a){setTimeout(J,1);return}e.ready()}}var e=function(a,b){return new e.fn.init(a,b,h)},f=a.jQuery,g=a.$,h,i=/^(?:[^#<]*(<[\w\W]+>)[^>]*$|#([\w\-]*)$)/,j=/\S/,k=/^\s+/,l=/\s+$/,m=/^<(\w+)\s*\/?>(?:<\/\1>)?$/,n=/^[\],:{}\s]*$/,o=/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g,p=/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g,q=/(?:^|:|,)(?:\s*\[)+/g,r=/(webkit)[ \/]([\w.]+)/,s=/(opera)(?:.*version)?[ \/]([\w.]+)/,t=/(msie) ([\w.]+)/,u=/(mozilla)(?:.*? rv:([\w.]+))?/,v=/-([a-z]|[0-9])/ig,w=/^-ms-/,x=function(a,b){return(b+"").toUpperCase()},y=d.userAgent,z,A,B,C=Object.prototype.toString,D=Object.prototype.hasOwnProperty,E=Array.prototype.push,F=Array.prototype.slice,G=String.prototype.trim,H=Array.prototype.indexOf,I={};e.fn=e.prototype={constructor:e,init:function(a,d,f){var g,h,j,k;if(!a)return this;if(a.nodeType){this.context=this[0]=a,this.length=1;return this}if(a==="body"&&!d&&c.body){this.context=c,this[0]=c.body,this.selector=a,this.length=1;return this}if(typeof a=="string"){a.charAt(0)!=="<"||a.charAt(a.length-1)!==">"||a.length<3?g=i.exec(a):g=[null,a,null];if(g&&(g[1]||!d)){if(g[1]){d=d instanceof e?d[0]:d,k=d?d.ownerDocument||d:c,j=m.exec(a),j?e.isPlainObject(d)?(a=[c.createElement(j[1])],e.fn.attr.call(a,d,!0)):a=[k.createElement(j[1])]:(j=e.buildFragment([g[1]],[k]),a=(j.cacheable?e.clone(j.fragment):j.fragment).childNodes);return e.merge(this,a)}h=c.getElementById(g[2]);if(h&&h.parentNode){if(h.id!==g[2])return f.find(a);this.length=1,this[0]=h}this.context=c,this.selector=a;return this}return!d||d.jquery?(d||f).find(a):this.constructor(d).find(a)}if(e.isFunction(a))return f.ready(a);a.selector!==b&&(this.selector=a.selector,this.context=a.context);return e.makeArray(a,this)},selector:"",jquery:"1.7.2",length:0,size:function(){return this.length},toArray:function(){return F.call(this,0)},get:function(a){return a==null?this.toArray():a<0?this[this.length+a]:this[a]},pushStack:function(a,b,c){var d=this.constructor();e.isArray(a)?E.apply(d,a):e.merge(d,a),d.prevObject=this,d.context=this.context,b==="find"?d.selector=this.selector+(this.selector?" ":"")+c:b&&(d.selector=this.selector+"."+b+"("+c+")");return d},each:function(a,b){return e.each(this,a,b)},ready:function(a){e.bindReady(),A.add(a);return this},eq:function(a){a=+a;return a===-1?this.slice(a):this.slice(a,a+1)},first:function(){return this.eq(0)},last:function(){return this.eq(-1)},slice:function(){return this.pushStack(F.apply(this,arguments),"slice",F.call(arguments).join(","))},map:function(a){return this.pushStack(e.map(this,function(b,c){return a.call(b,c,b)}))},end:function(){return this.prevObject||this.constructor(null)},push:E,sort:[].sort,splice:[].splice},e.fn.init.prototype=e.fn,e.extend=e.fn.extend=function(){var a,c,d,f,g,h,i=arguments[0]||{},j=1,k=arguments.length,l=!1;typeof i=="boolean"&&(l=i,i=arguments[1]||{},j=2),typeof i!="object"&&!e.isFunction(i)&&(i={}),k===j&&(i=this,--j);for(;j<k;j++)if((a=arguments[j])!=null)for(c in a){d=i[c],f=a[c];if(i===f)continue;l&&f&&(e.isPlainObject(f)||(g=e.isArray(f)))?(g?(g=!1,h=d&&e.isArray(d)?d:[]):h=d&&e.isPlainObject(d)?d:{},i[c]=e.extend(l,h,f)):f!==b&&(i[c]=f)}return i},e.extend({noConflict:function(b){a.$===e&&(a.$=g),b&&a.jQuery===e&&(a.jQuery=f);return e},isReady:!1,readyWait:1,holdReady:function(a){a?e.readyWait++:e.ready(!0)},ready:function(a){if(a===!0&&!--e.readyWait||a!==!0&&!e.isReady){if(!c.body)return setTimeout(e.ready,1);e.isReady=!0;if(a!==!0&&--e.readyWait>0)return;A.fireWith(c,[e]),e.fn.trigger&&e(c).trigger("ready").off("ready")}},bindReady:function(){if(!A){A=e.Callbacks("once memory");if(c.readyState==="complete")return setTimeout(e.ready,1);if(c.addEventListener)c.addEventListener("DOMContentLoaded",B,!1),a.addEventListener("load",e.ready,!1);else if(c.attachEvent){c.attachEvent("onreadystatechange",B),a.attachEvent("onload",e.ready);var b=!1;try{b=a.frameElement==null}catch(d){}c.documentElement.doScroll&&b&&J()}}},isFunction:function(a){return e.type(a)==="function"},isArray:Array.isArray||function(a){return e.type(a)==="array"},isWindow:function(a){return a!=null&&a==a.window},isNumeric:function(a){return!isNaN(parseFloat(a))&&isFinite(a)},type:function(a){return a==null?String(a):I[C.call(a)]||"object"},isPlainObject:function(a){if(!a||e.type(a)!=="object"||a.nodeType||e.isWindow(a))return!1;try{if(a.constructor&&!D.call(a,"constructor")&&!D.call(a.constructor.prototype,"isPrototypeOf"))return!1}catch(c){return!1}var d;for(d in a);return d===b||D.call(a,d)},isEmptyObject:function(a){for(var b in a)return!1;return!0},error:function(a){throw new Error(a)},parseJSON:function(b){if(typeof b!="string"||!b)return null;b=e.trim(b);if(a.JSON&&a.JSON.parse)return a.JSON.parse(b);if(n.test(b.replace(o,"@").replace(p,"]").replace(q,"")))return(new Function("return "+b))();e.error("Invalid JSON: "+b)},parseXML:function(c){if(typeof c!="string"||!c)return null;var d,f;try{a.DOMParser?(f=new DOMParser,d=f.parseFromString(c,"text/xml")):(d=new ActiveXObject("Microsoft.XMLDOM"),d.async="false",d.loadXML(c))}catch(g){d=b}(!d||!d.documentElement||d.getElementsByTagName("parsererror").length)&&e.error("Invalid XML: "+c);return d},noop:function(){},globalEval:function(b){b&&j.test(b)&&(a.execScript||function(b){a.eval.call(a,b)})(b)},camelCase:function(a){return a.replace(w,"ms-").replace(v,x)},nodeName:function(a,b){return a.nodeName&&a.nodeName.toUpperCase()===b.toUpperCase()},each:function(a,c,d){var f,g=0,h=a.length,i=h===b||e.isFunction(a);if(d){if(i){for(f in a)if(c.apply(a[f],d)===!1)break}else for(;g<h;)if(c.apply(a[g++],d)===!1)break}else if(i){for(f in a)if(c.call(a[f],f,a[f])===!1)break}else for(;g<h;)if(c.call(a[g],g,a[g++])===!1)break;return a},trim:G?function(a){return a==null?"":G.call(a)}:function(a){return a==null?"":(a+"").replace(k,"").replace(l,"")},makeArray:function(a,b){var c=b||[];if(a!=null){var d=e.type(a);a.length==null||d==="string"||d==="function"||d==="regexp"||e.isWindow(a)?E.call(c,a):e.merge(c,a)}return c},inArray:function(a,b,c){var d;if(b){if(H)return H.call(b,a,c);d=b.length,c=c?c<0?Math.max(0,d+c):c:0;for(;c<d;c++)if(c in b&&b[c]===a)return c}return-1},merge:function(a,c){var d=a.length,e=0;if(typeof c.length=="number")for(var f=c.length;e<f;e++)a[d++]=c[e];else while(c[e]!==b)a[d++]=c[e++];a.length=d;return a},grep:function(a,b,c){var d=[],e;c=!!c;for(var f=0,g=a.length;f<g;f++)e=!!b(a[f],f),c!==e&&d.push(a[f]);return d},map:function(a,c,d){var f,g,h=[],i=0,j=a.length,k=a instanceof e||j!==b&&typeof j=="number"&&(j>0&&a[0]&&a[j-1]||j===0||e.isArray(a));if(k)for(;i<j;i++)f=c(a[i],i,d),f!=null&&(h[h.length]=f);else for(g in a)f=c(a[g],g,d),f!=null&&(h[h.length]=f);return h.concat.apply([],h)},guid:1,proxy:function(a,c){if(typeof c=="string"){var d=a[c];c=a,a=d}if(!e.isFunction(a))return b;var f=F.call(arguments,2),g=function(){return a.apply(c,f.concat(F.call(arguments)))};g.guid=a.guid=a.guid||g.guid||e.guid++;return g},access:function(a,c,d,f,g,h,i){var j,k=d==null,l=0,m=a.length;if(d&&typeof d=="object"){for(l in d)e.access(a,c,l,d[l],1,h,f);g=1}else if(f!==b){j=i===b&&e.isFunction(f),k&&(j?(j=c,c=function(a,b,c){return j.call(e(a),c)}):(c.call(a,f),c=null));if(c)for(;l<m;l++)c(a[l],d,j?f.call(a[l],l,c(a[l],d)):f,i);g=1}return g?a:k?c.call(a):m?c(a[0],d):h},now:function(){return(new Date).getTime()},uaMatch:function(a){a=a.toLowerCase();var b=r.exec(a)||s.exec(a)||t.exec(a)||a.indexOf("compatible")<0&&u.exec(a)||[];return{browser:b[1]||"",version:b[2]||"0"}},sub:function(){function a(b,c){return new a.fn.init(b,c)}e.extend(!0,a,this),a.superclass=this,a.fn=a.prototype=this(),a.fn.constructor=a,a.sub=this.sub,a.fn.init=function(d,f){f&&f instanceof e&&!(f instanceof a)&&(f=a(f));return e.fn.init.call(this,d,f,b)},a.fn.init.prototype=a.fn;var b=a(c);return a},browser:{}}),e.each("Boolean Number String Function Array Date RegExp Object".split(" "),function(a,b){I["[object "+b+"]"]=b.toLowerCase()}),z=e.uaMatch(y),z.browser&&(e.browser[z.browser]=!0,e.browser.version=z.version),e.browser.webkit&&(e.browser.safari=!0),j.test(" ")&&(k=/^[\s\xA0]+/,l=/[\s\xA0]+$/),h=e(c),c.addEventListener?B=function(){c.removeEventListener("DOMContentLoaded",B,!1),e.ready()}:c.attachEvent&&(B=function(){c.readyState==="complete"&&(c.detachEvent("onreadystatechange",B),e.ready())});return e}(),g={};f.Callbacks=function(a){a=a?g[a]||h(a):{};var c=[],d=[],e,i,j,k,l,m,n=function(b){var d,e,g,h,i;for(d=0,e=b.length;d<e;d++)g=b[d],h=f.type(g),h==="array"?n(g):h==="function"&&(!a.unique||!p.has(g))&&c.push(g)},o=function(b,f){f=f||[],e=!a.memory||[b,f],i=!0,j=!0,m=k||0,k=0,l=c.length;for(;c&&m<l;m++)if(c[m].apply(b,f)===!1&&a.stopOnFalse){e=!0;break}j=!1,c&&(a.once?e===!0?p.disable():c=[]:d&&d.length&&(e=d.shift(),p.fireWith(e[0],e[1])))},p={add:function(){if(c){var a=c.length;n(arguments),j?l=c.length:e&&e!==!0&&(k=a,o(e[0],e[1]))}return this},remove:function(){if(c){var b=arguments,d=0,e=b.length;for(;d<e;d++)for(var f=0;f<c.length;f++)if(b[d]===c[f]){j&&f<=l&&(l--,f<=m&&m--),c.splice(f--,1);if(a.unique)break}}return this},has:function(a){if(c){var b=0,d=c.length;for(;b<d;b++)if(a===c[b])return!0}return!1},empty:function(){c=[];return this},disable:function(){c=d=e=b;return this},disabled:function(){return!c},lock:function(){d=b,(!e||e===!0)&&p.disable();return this},locked:function(){return!d},fireWith:function(b,c){d&&(j?a.once||d.push([b,c]):(!a.once||!e)&&o(b,c));return this},fire:function(){p.fireWith(this,arguments);return this},fired:function(){return!!i}};return p};var i=[].slice;f.extend({Deferred:function(a){var b=f.Callbacks("once memory"),c=f.Callbacks("once memory"),d=f.Callbacks("memory"),e="pending",g={resolve:b,reject:c,notify:d},h={done:b.add,fail:c.add,progress:d.add,state:function(){return e},isResolved:b.fired,isRejected:c.fired,then:function(a,b,c){i.done(a).fail(b).progress(c);return this},always:function(){i.done.apply(i,arguments).fail.apply(i,arguments);return this},pipe:function(a,b,c){return f.Deferred(function(d){f.each({done:[a,"resolve"],fail:[b,"reject"],progress:[c,"notify"]},function(a,b){var c=b[0],e=b[1],g;f.isFunction(c)?i[a](function(){g=c.apply(this,arguments),g&&f.isFunction(g.promise)?g.promise().then(d.resolve,d.reject,d.notify):d[e+"With"](this===i?d:this,[g])}):i[a](d[e])})}).promise()},promise:function(a){if(a==null)a=h;else for(var b in h)a[b]=h[b];return a}},i=h.promise({}),j;for(j in g)i[j]=g[j].fire,i[j+"With"]=g[j].fireWith;i.done(function(){e="resolved"},c.disable,d.lock).fail(function(){e="rejected"},b.disable,d.lock),a&&a.call(i,i);return i},when:function(a){function m(a){return function(b){e[a]=arguments.length>1?i.call(arguments,0):b,j.notifyWith(k,e)}}function l(a){return function(c){b[a]=arguments.length>1?i.call(arguments,0):c,--g||j.resolveWith(j,b)}}var b=i.call(arguments,0),c=0,d=b.length,e=Array(d),g=d,h=d,j=d<=1&&a&&f.isFunction(a.promise)?a:f.Deferred(),k=j.promise();if(d>1){for(;c<d;c++)b[c]&&b[c].promise&&f.isFunction(b[c].promise)?b[c].promise().then(l(c),j.reject,m(c)):--g;g||j.resolveWith(j,b)}else j!==a&&j.resolveWith(j,d?[a]:[]);return k}}),f.support=function(){var b,d,e,g,h,i,j,k,l,m,n,o,p=c.createElement("div"),q=c.documentElement;p.setAttribute("className","t"),p.innerHTML="   <link/><table></table><a href='/a' style='top:1px;float:left;opacity:.55;'>a</a><input type='checkbox'/>",d=p.getElementsByTagName("*"),e=p.getElementsByTagName("a")[0];if(!d||!d.length||!e)return{};g=c.createElement("select"),h=g.appendChild(c.createElement("option")),i=p.getElementsByTagName("input")[0],b={leadingWhitespace:p.firstChild.nodeType===3,tbody:!p.getElementsByTagName("tbody").length,htmlSerialize:!!p.getElementsByTagName("link").length,style:/top/.test(e.getAttribute("style")),hrefNormalized:e.getAttribute("href")==="/a",opacity:/^0.55/.test(e.style.opacity),cssFloat:!!e.style.cssFloat,checkOn:i.value==="on",optSelected:h.selected,getSetAttribute:p.className!=="t",enctype:!!c.createElement("form").enctype,html5Clone:c.createElement("nav").cloneNode(!0).outerHTML!=="<:nav></:nav>",submitBubbles:!0,changeBubbles:!0,focusinBubbles:!1,deleteExpando:!0,noCloneEvent:!0,inlineBlockNeedsLayout:!1,shrinkWrapBlocks:!1,reliableMarginRight:!0,pixelMargin:!0},f.boxModel=b.boxModel=c.compatMode==="CSS1Compat",i.checked=!0,b.noCloneChecked=i.cloneNode(!0).checked,g.disabled=!0,b.optDisabled=!h.disabled;try{delete p.test}catch(r){b.deleteExpando=!1}!p.addEventListener&&p.attachEvent&&p.fireEvent&&(p.attachEvent("onclick",function(){b.noCloneEvent=!1}),p.cloneNode(!0).fireEvent("onclick")),i=c.createElement("input"),i.value="t",i.setAttribute("type","radio"),b.radioValue=i.value==="t",i.setAttribute("checked","checked"),i.setAttribute("name","t"),p.appendChild(i),j=c.createDocumentFragment(),j.appendChild(p.lastChild),b.checkClone=j.cloneNode(!0).cloneNode(!0).lastChild.checked,b.appendChecked=i.checked,j.removeChild(i),j.appendChild(p);if(p.attachEvent)for(n in{submit:1,change:1,focusin:1})m="on"+n,o=m in p,o||(p.setAttribute(m,"return;"),o=typeof p[m]=="function"),b[n+"Bubbles"]=o;j.removeChild(p),j=g=h=p=i=null,f(function(){var d,e,g,h,i,j,l,m,n,q,r,s,t,u=c.getElementsByTagName("body")[0];!u||(m=1,t="padding:0;margin:0;border:",r="position:absolute;top:0;left:0;width:1px;height:1px;",s=t+"0;visibility:hidden;",n="style='"+r+t+"5px solid #000;",q="<div "+n+"display:block;'><div style='"+t+"0;display:block;overflow:hidden;'></div></div>"+"<table "+n+"' cellpadding='0' cellspacing='0'>"+"<tr><td></td></tr></table>",d=c.createElement("div"),d.style.cssText=s+"width:0;height:0;position:static;top:0;margin-top:"+m+"px",u.insertBefore(d,u.firstChild),p=c.createElement("div"),d.appendChild(p),p.innerHTML="<table><tr><td style='"+t+"0;display:none'></td><td>t</td></tr></table>",k=p.getElementsByTagName("td"),o=k[0].offsetHeight===0,k[0].style.display="",k[1].style.display="none",b.reliableHiddenOffsets=o&&k[0].offsetHeight===0,a.getComputedStyle&&(p.innerHTML="",l=c.createElement("div"),l.style.width="0",l.style.marginRight="0",p.style.width="2px",p.appendChild(l),b.reliableMarginRight=(parseInt((a.getComputedStyle(l,null)||{marginRight:0}).marginRight,10)||0)===0),typeof p.style.zoom!="undefined"&&(p.innerHTML="",p.style.width=p.style.padding="1px",p.style.border=0,p.style.overflow="hidden",p.style.display="inline",p.style.zoom=1,b.inlineBlockNeedsLayout=p.offsetWidth===3,p.style.display="block",p.style.overflow="visible",p.innerHTML="<div style='width:5px;'></div>",b.shrinkWrapBlocks=p.offsetWidth!==3),p.style.cssText=r+s,p.innerHTML=q,e=p.firstChild,g=e.firstChild,i=e.nextSibling.firstChild.firstChild,j={doesNotAddBorder:g.offsetTop!==5,doesAddBorderForTableAndCells:i.offsetTop===5},g.style.position="fixed",g.style.top="20px",j.fixedPosition=g.offsetTop===20||g.offsetTop===15,g.style.position=g.style.top="",e.style.overflow="hidden",e.style.position="relative",j.subtractsBorderForOverflowNotVisible=g.offsetTop===-5,j.doesNotIncludeMarginInBodyOffset=u.offsetTop!==m,a.getComputedStyle&&(p.style.marginTop="1%",b.pixelMargin=(a.getComputedStyle(p,null)||{marginTop:0}).marginTop!=="1%"),typeof d.style.zoom!="undefined"&&(d.style.zoom=1),u.removeChild(d),l=p=d=null,f.extend(b,j))});return b}();var j=/^(?:\{.*\}|\[.*\])$/,k=/([A-Z])/g;f.extend({cache:{},uuid:0,expando:"jQuery"+(f.fn.jquery+Math.random()).replace(/\D/g,""),noData:{embed:!0,object:"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000",applet:!0},hasData:function(a){a=a.nodeType?f.cache[a[f.expando]]:a[f.expando];return!!a&&!m(a)},data:function(a,c,d,e){if(!!f.acceptData(a)){var g,h,i,j=f.expando,k=typeof c=="string",l=a.nodeType,m=l?f.cache:a,n=l?a[j]:a[j]&&j,o=c==="events";if((!n||!m[n]||!o&&!e&&!m[n].data)&&k&&d===b)return;n||(l?a[j]=n=++f.uuid:n=j),m[n]||(m[n]={},l||(m[n].toJSON=f.noop));if(typeof c=="object"||typeof c=="function")e?m[n]=f.extend(m[n],c):m[n].data=f.extend(m[n].data,c);g=h=m[n],e||(h.data||(h.data={}),h=h.data),d!==b&&(h[f.camelCase(c)]=d);if(o&&!h[c])return g.events;k?(i=h[c],i==null&&(i=h[f.camelCase(c)])):i=h;return i}},removeData:function(a,b,c){if(!!f.acceptData(a)){var d,e,g,h=f.expando,i=a.nodeType,j=i?f.cache:a,k=i?a[h]:h;if(!j[k])return;if(b){d=c?j[k]:j[k].data;if(d){f.isArray(b)||(b in d?b=[b]:(b=f.camelCase(b),b in d?b=[b]:b=b.split(" ")));for(e=0,g=b.length;e<g;e++)delete d[b[e]];if(!(c?m:f.isEmptyObject)(d))return}}if(!c){delete j[k].data;if(!m(j[k]))return}f.support.deleteExpando||!j.setInterval?delete j[k]:j[k]=null,i&&(f.support.deleteExpando?delete a[h]:a.removeAttribute?a.removeAttribute(h):a[h]=null)}},_data:function(a,b,c){return f.data(a,b,c,!0)},acceptData:function(a){if(a.nodeName){var b=f.noData[a.nodeName.toLowerCase()];if(b)return b!==!0&&a.getAttribute("classid")===b}return!0}}),f.fn.extend({data:function(a,c){var d,e,g,h,i,j=this[0],k=0,m=null;if(a===b){if(this.length){m=f.data(j);if(j.nodeType===1&&!f._data(j,"parsedAttrs")){g=j.attributes;for(i=g.length;k<i;k++)h=g[k].name,h.indexOf("data-")===0&&(h=f.camelCase(h.substring(5)),l(j,h,m[h]));f._data(j,"parsedAttrs",!0)}}return m}if(typeof a=="object")return this.each(function(){f.data(this,a)});d=a.split(".",2),d[1]=d[1]?"."+d[1]:"",e=d[1]+"!";return f.access(this,function(c){if(c===b){m=this.triggerHandler("getData"+e,[d[0]]),m===b&&j&&(m=f.data(j,a),m=l(j,a,m));return m===b&&d[1]?this.data(d[0]):m}d[1]=c,this.each(function(){var b=f(this);b.triggerHandler("setData"+e,d),f.data(this,a,c),b.triggerHandler("changeData"+e,d)})},null,c,arguments.length>1,null,!1)},removeData:function(a){return this.each(function(){f.removeData(this,a)})}}),f.extend({_mark:function(a,b){a&&(b=(b||"fx")+"mark",f._data(a,b,(f._data(a,b)||0)+1))},_unmark:function(a,b,c){a!==!0&&(c=b,b=a,a=!1);if(b){c=c||"fx";var d=c+"mark",e=a?0:(f._data(b,d)||1)-1;e?f._data(b,d,e):(f.removeData(b,d,!0),n(b,c,"mark"))}},queue:function(a,b,c){var d;if(a){b=(b||"fx")+"queue",d=f._data(a,b),c&&(!d||f.isArray(c)?d=f._data(a,b,f.makeArray(c)):d.push(c));return d||[]}},dequeue:function(a,b){b=b||"fx";var c=f.queue(a,b),d=c.shift(),e={};d==="inprogress"&&(d=c.shift()),d&&(b==="fx"&&c.unshift("inprogress"),f._data(a,b+".run",e),d.call(a,function(){f.dequeue(a,b)},e)),c.length||(f.removeData(a,b+"queue "+b+".run",!0),n(a,b,"queue"))}}),f.fn.extend({queue:function(a,c){var d=2;typeof a!="string"&&(c=a,a="fx",d--);if(arguments.length<d)return f.queue(this[0],a);return c===b?this:this.each(function(){var b=f.queue(this,a,c);a==="fx"&&b[0]!=="inprogress"&&f.dequeue(this,a)})},dequeue:function(a){return this.each(function(){f.dequeue(this,a)})},delay:function(a,b){a=f.fx?f.fx.speeds[a]||a:a,b=b||"fx";return this.queue(b,function(b,c){var d=setTimeout(b,a);c.stop=function(){clearTimeout(d)}})},clearQueue:function(a){return this.queue(a||"fx",[])},promise:function(a,c){function m(){--h||d.resolveWith(e,[e])}typeof a!="string"&&(c=a,a=b),a=a||"fx";var d=f.Deferred(),e=this,g=e.length,h=1,i=a+"defer",j=a+"queue",k=a+"mark",l;while(g--)if(l=f.data(e[g],i,b,!0)||(f.data(e[g],j,b,!0)||f.data(e[g],k,b,!0))&&f.data(e[g],i,f.Callbacks("once memory"),!0))h++,l.add(m);m();return d.promise(c)}});var o=/[\n\t\r]/g,p=/\s+/,q=/\r/g,r=/^(?:button|input)$/i,s=/^(?:button|input|object|select|textarea)$/i,t=/^a(?:rea)?$/i,u=/^(?:autofocus|autoplay|async|checked|controls|defer|disabled|hidden|loop|multiple|open|readonly|required|scoped|selected)$/i,v=f.support.getSetAttribute,w,x,y;f.fn.extend({attr:function(a,b){return f.access(this,f.attr,a,b,arguments.length>1)},removeAttr:function(a){return this.each(function(){f.removeAttr(this,a)})},prop:function(a,b){return f.access(this,f.prop,a,b,arguments.length>1)},removeProp:function(a){a=f.propFix[a]||a;return this.each(function(){try{this[a]=b,delete this[a]}catch(c){}})},addClass:function(a){var b,c,d,e,g,h,i;if(f.isFunction(a))return this.each(function(b){f(this).addClass(a.call(this,b,this.className))});if(a&&typeof a=="string"){b=a.split(p);for(c=0,d=this.length;c<d;c++){e=this[c];if(e.nodeType===1)if(!e.className&&b.length===1)e.className=a;else{g=" "+e.className+" ";for(h=0,i=b.length;h<i;h++)~g.indexOf(" "+b[h]+" ")||(g+=b[h]+" ");e.className=f.trim(g)}}}return this},removeClass:function(a){var c,d,e,g,h,i,j;if(f.isFunction(a))return this.each(function(b){f(this).removeClass(a.call(this,b,this.className))});if(a&&typeof a=="string"||a===b){c=(a||"").split(p);for(d=0,e=this.length;d<e;d++){g=this[d];if(g.nodeType===1&&g.className)if(a){h=(" "+g.className+" ").replace(o," ");for(i=0,j=c.length;i<j;i++)h=h.replace(" "+c[i]+" "," ");g.className=f.trim(h)}else g.className=""}}return this},toggleClass:function(a,b){var c=typeof a,d=typeof b=="boolean";if(f.isFunction(a))return this.each(function(c){f(this).toggleClass(a.call(this,c,this.className,b),b)});return this.each(function(){if(c==="string"){var e,g=0,h=f(this),i=b,j=a.split(p);while(e=j[g++])i=d?i:!h.hasClass(e),h[i?"addClass":"removeClass"](e)}else if(c==="undefined"||c==="boolean")this.className&&f._data(this,"__className__",this.className),this.className=this.className||a===!1?"":f._data(this,"__className__")||""})},hasClass:function(a){var b=" "+a+" ",c=0,d=this.length;for(;c<d;c++)if(this[c].nodeType===1&&(" "+this[c].className+" ").replace(o," ").indexOf(b)>-1)return!0;return!1},val:function(a){var c,d,e,g=this[0];{if(!!arguments.length){e=f.isFunction(a);return this.each(function(d){var g=f(this),h;if(this.nodeType===1){e?h=a.call(this,d,g.val()):h=a,h==null?h="":typeof h=="number"?h+="":f.isArray(h)&&(h=f.map(h,function(a){return a==null?"":a+""})),c=f.valHooks[this.type]||f.valHooks[this.nodeName.toLowerCase()];if(!c||!("set"in c)||c.set(this,h,"value")===b)this.value=h}})}if(g){c=f.valHooks[g.type]||f.valHooks[g.nodeName.toLowerCase()];if(c&&"get"in c&&(d=c.get(g,"value"))!==b)return d;d=g.value;return typeof d=="string"?d.replace(q,""):d==null?"":d}}}}),f.extend({valHooks:{option:{get:function(a){var b=a.attributes.value;return!b||b.specified?a.value:a.text}},select:{get:function(a){var b,c,d,e,g=a.selectedIndex,h=[],i=a.options,j=a.type==="select-one";if(g<0)return null;c=j?g:0,d=j?g+1:i.length;for(;c<d;c++){e=i[c];if(e.selected&&(f.support.optDisabled?!e.disabled:e.getAttribute("disabled")===null)&&(!e.parentNode.disabled||!f.nodeName(e.parentNode,"optgroup"))){b=f(e).val();if(j)return b;h.push(b)}}if(j&&!h.length&&i.length)return f(i[g]).val();return h},set:function(a,b){var c=f.makeArray(b);f(a).find("option").each(function(){this.selected=f.inArray(f(this).val(),c)>=0}),c.length||(a.selectedIndex=-1);return c}}},attrFn:{val:!0,css:!0,html:!0,text:!0,data:!0,width:!0,height:!0,offset:!0},attr:function(a,c,d,e){var g,h,i,j=a.nodeType;if(!!a&&j!==3&&j!==8&&j!==2){if(e&&c in f.attrFn)return f(a)[c](d);if(typeof a.getAttribute=="undefined")return f.prop(a,c,d);i=j!==1||!f.isXMLDoc(a),i&&(c=c.toLowerCase(),h=f.attrHooks[c]||(u.test(c)?x:w));if(d!==b){if(d===null){f.removeAttr(a,c);return}if(h&&"set"in h&&i&&(g=h.set(a,d,c))!==b)return g;a.setAttribute(c,""+d);return d}if(h&&"get"in h&&i&&(g=h.get(a,c))!==null)return g;g=a.getAttribute(c);return g===null?b:g}},removeAttr:function(a,b){var c,d,e,g,h,i=0;if(b&&a.nodeType===1){d=b.toLowerCase().split(p),g=d.length;for(;i<g;i++)e=d[i],e&&(c=f.propFix[e]||e,h=u.test(e),h||f.attr(a,e,""),a.removeAttribute(v?e:c),h&&c in a&&(a[c]=!1))}},attrHooks:{type:{set:function(a,b){if(r.test(a.nodeName)&&a.parentNode)f.error("type property can't be changed");else if(!f.support.radioValue&&b==="radio"&&f.nodeName(a,"input")){var c=a.value;a.setAttribute("type",b),c&&(a.value=c);return b}}},value:{get:function(a,b){if(w&&f.nodeName(a,"button"))return w.get(a,b);return b in a?a.value:null},set:function(a,b,c){if(w&&f.nodeName(a,"button"))return w.set(a,b,c);a.value=b}}},propFix:{tabindex:"tabIndex",readonly:"readOnly","for":"htmlFor","class":"className",maxlength:"maxLength",cellspacing:"cellSpacing",cellpadding:"cellPadding",rowspan:"rowSpan",colspan:"colSpan",usemap:"useMap",frameborder:"frameBorder",contenteditable:"contentEditable"},prop:function(a,c,d){var e,g,h,i=a.nodeType;if(!!a&&i!==3&&i!==8&&i!==2){h=i!==1||!f.isXMLDoc(a),h&&(c=f.propFix[c]||c,g=f.propHooks[c]);return d!==b?g&&"set"in g&&(e=g.set(a,d,c))!==b?e:a[c]=d:g&&"get"in g&&(e=g.get(a,c))!==null?e:a[c]}},propHooks:{tabIndex:{get:function(a){var c=a.getAttributeNode("tabindex");return c&&c.specified?parseInt(c.value,10):s.test(a.nodeName)||t.test(a.nodeName)&&a.href?0:b}}}}),f.attrHooks.tabindex=f.propHooks.tabIndex,x={get:function(a,c){var d,e=f.prop(a,c);return e===!0||typeof e!="boolean"&&(d=a.getAttributeNode(c))&&d.nodeValue!==!1?c.toLowerCase():b},set:function(a,b,c){var d;b===!1?f.removeAttr(a,c):(d=f.propFix[c]||c,d in a&&(a[d]=!0),a.setAttribute(c,c.toLowerCase()));return c}},v||(y={name:!0,id:!0,coords:!0},w=f.valHooks.button={get:function(a,c){var d;d=a.getAttributeNode(c);return d&&(y[c]?d.nodeValue!=="":d.specified)?d.nodeValue:b},set:function(a,b,d){var e=a.getAttributeNode(d);e||(e=c.createAttribute(d),a.setAttributeNode(e));return e.nodeValue=b+""}},f.attrHooks.tabindex.set=w.set,f.each(["width","height"],function(a,b){f.attrHooks[b]=f.extend(f.attrHooks[b],{set:function(a,c){if(c===""){a.setAttribute(b,"auto");return c}}})}),f.attrHooks.contenteditable={get:w.get,set:function(a,b,c){b===""&&(b="false"),w.set(a,b,c)}}),f.support.hrefNormalized||f.each(["href","src","width","height"],function(a,c){f.attrHooks[c]=f.extend(f.attrHooks[c],{get:function(a){var d=a.getAttribute(c,2);return d===null?b:d}})}),f.support.style||(f.attrHooks.style={get:function(a){return a.style.cssText.toLowerCase()||b},set:function(a,b){return a.style.cssText=""+b}}),f.support.optSelected||(f.propHooks.selected=f.extend(f.propHooks.selected,{get:function(a){var b=a.parentNode;b&&(b.selectedIndex,b.parentNode&&b.parentNode.selectedIndex);return null}})),f.support.enctype||(f.propFix.enctype="encoding"),f.support.checkOn||f.each(["radio","checkbox"],function(){f.valHooks[this]={get:function(a){return a.getAttribute("value")===null?"on":a.value}}}),f.each(["radio","checkbox"],function(){f.valHooks[this]=f.extend(f.valHooks[this],{set:function(a,b){if(f.isArray(b))return a.checked=f.inArray(f(a).val(),b)>=0}})});var z=/^(?:textarea|input|select)$/i,A=/^([^\.]*)?(?:\.(.+))?$/,B=/(?:^|\s)hover(\.\S+)?\b/,C=/^key/,D=/^(?:mouse|contextmenu)|click/,E=/^(?:focusinfocus|focusoutblur)$/,F=/^(\w*)(?:#([\w\-]+))?(?:\.([\w\-]+))?$/,G=function(
a){var b=F.exec(a);b&&(b[1]=(b[1]||"").toLowerCase(),b[3]=b[3]&&new RegExp("(?:^|\\s)"+b[3]+"(?:\\s|$)"));return b},H=function(a,b){var c=a.attributes||{};return(!b[1]||a.nodeName.toLowerCase()===b[1])&&(!b[2]||(c.id||{}).value===b[2])&&(!b[3]||b[3].test((c["class"]||{}).value))},I=function(a){return f.event.special.hover?a:a.replace(B,"mouseenter$1 mouseleave$1")};f.event={add:function(a,c,d,e,g){var h,i,j,k,l,m,n,o,p,q,r,s;if(!(a.nodeType===3||a.nodeType===8||!c||!d||!(h=f._data(a)))){d.handler&&(p=d,d=p.handler,g=p.selector),d.guid||(d.guid=f.guid++),j=h.events,j||(h.events=j={}),i=h.handle,i||(h.handle=i=function(a){return typeof f!="undefined"&&(!a||f.event.triggered!==a.type)?f.event.dispatch.apply(i.elem,arguments):b},i.elem=a),c=f.trim(I(c)).split(" ");for(k=0;k<c.length;k++){l=A.exec(c[k])||[],m=l[1],n=(l[2]||"").split(".").sort(),s=f.event.special[m]||{},m=(g?s.delegateType:s.bindType)||m,s=f.event.special[m]||{},o=f.extend({type:m,origType:l[1],data:e,handler:d,guid:d.guid,selector:g,quick:g&&G(g),namespace:n.join(".")},p),r=j[m];if(!r){r=j[m]=[],r.delegateCount=0;if(!s.setup||s.setup.call(a,e,n,i)===!1)a.addEventListener?a.addEventListener(m,i,!1):a.attachEvent&&a.attachEvent("on"+m,i)}s.add&&(s.add.call(a,o),o.handler.guid||(o.handler.guid=d.guid)),g?r.splice(r.delegateCount++,0,o):r.push(o),f.event.global[m]=!0}a=null}},global:{},remove:function(a,b,c,d,e){var g=f.hasData(a)&&f._data(a),h,i,j,k,l,m,n,o,p,q,r,s;if(!!g&&!!(o=g.events)){b=f.trim(I(b||"")).split(" ");for(h=0;h<b.length;h++){i=A.exec(b[h])||[],j=k=i[1],l=i[2];if(!j){for(j in o)f.event.remove(a,j+b[h],c,d,!0);continue}p=f.event.special[j]||{},j=(d?p.delegateType:p.bindType)||j,r=o[j]||[],m=r.length,l=l?new RegExp("(^|\\.)"+l.split(".").sort().join("\\.(?:.*\\.)?")+"(\\.|$)"):null;for(n=0;n<r.length;n++)s=r[n],(e||k===s.origType)&&(!c||c.guid===s.guid)&&(!l||l.test(s.namespace))&&(!d||d===s.selector||d==="**"&&s.selector)&&(r.splice(n--,1),s.selector&&r.delegateCount--,p.remove&&p.remove.call(a,s));r.length===0&&m!==r.length&&((!p.teardown||p.teardown.call(a,l)===!1)&&f.removeEvent(a,j,g.handle),delete o[j])}f.isEmptyObject(o)&&(q=g.handle,q&&(q.elem=null),f.removeData(a,["events","handle"],!0))}},customEvent:{getData:!0,setData:!0,changeData:!0},trigger:function(c,d,e,g){if(!e||e.nodeType!==3&&e.nodeType!==8){var h=c.type||c,i=[],j,k,l,m,n,o,p,q,r,s;if(E.test(h+f.event.triggered))return;h.indexOf("!")>=0&&(h=h.slice(0,-1),k=!0),h.indexOf(".")>=0&&(i=h.split("."),h=i.shift(),i.sort());if((!e||f.event.customEvent[h])&&!f.event.global[h])return;c=typeof c=="object"?c[f.expando]?c:new f.Event(h,c):new f.Event(h),c.type=h,c.isTrigger=!0,c.exclusive=k,c.namespace=i.join("."),c.namespace_re=c.namespace?new RegExp("(^|\\.)"+i.join("\\.(?:.*\\.)?")+"(\\.|$)"):null,o=h.indexOf(":")<0?"on"+h:"";if(!e){j=f.cache;for(l in j)j[l].events&&j[l].events[h]&&f.event.trigger(c,d,j[l].handle.elem,!0);return}c.result=b,c.target||(c.target=e),d=d!=null?f.makeArray(d):[],d.unshift(c),p=f.event.special[h]||{};if(p.trigger&&p.trigger.apply(e,d)===!1)return;r=[[e,p.bindType||h]];if(!g&&!p.noBubble&&!f.isWindow(e)){s=p.delegateType||h,m=E.test(s+h)?e:e.parentNode,n=null;for(;m;m=m.parentNode)r.push([m,s]),n=m;n&&n===e.ownerDocument&&r.push([n.defaultView||n.parentWindow||a,s])}for(l=0;l<r.length&&!c.isPropagationStopped();l++)m=r[l][0],c.type=r[l][1],q=(f._data(m,"events")||{})[c.type]&&f._data(m,"handle"),q&&q.apply(m,d),q=o&&m[o],q&&f.acceptData(m)&&q.apply(m,d)===!1&&c.preventDefault();c.type=h,!g&&!c.isDefaultPrevented()&&(!p._default||p._default.apply(e.ownerDocument,d)===!1)&&(h!=="click"||!f.nodeName(e,"a"))&&f.acceptData(e)&&o&&e[h]&&(h!=="focus"&&h!=="blur"||c.target.offsetWidth!==0)&&!f.isWindow(e)&&(n=e[o],n&&(e[o]=null),f.event.triggered=h,e[h](),f.event.triggered=b,n&&(e[o]=n));return c.result}},dispatch:function(c){c=f.event.fix(c||a.event);var d=(f._data(this,"events")||{})[c.type]||[],e=d.delegateCount,g=[].slice.call(arguments,0),h=!c.exclusive&&!c.namespace,i=f.event.special[c.type]||{},j=[],k,l,m,n,o,p,q,r,s,t,u;g[0]=c,c.delegateTarget=this;if(!i.preDispatch||i.preDispatch.call(this,c)!==!1){if(e&&(!c.button||c.type!=="click")){n=f(this),n.context=this.ownerDocument||this;for(m=c.target;m!=this;m=m.parentNode||this)if(m.disabled!==!0){p={},r=[],n[0]=m;for(k=0;k<e;k++)s=d[k],t=s.selector,p[t]===b&&(p[t]=s.quick?H(m,s.quick):n.is(t)),p[t]&&r.push(s);r.length&&j.push({elem:m,matches:r})}}d.length>e&&j.push({elem:this,matches:d.slice(e)});for(k=0;k<j.length&&!c.isPropagationStopped();k++){q=j[k],c.currentTarget=q.elem;for(l=0;l<q.matches.length&&!c.isImmediatePropagationStopped();l++){s=q.matches[l];if(h||!c.namespace&&!s.namespace||c.namespace_re&&c.namespace_re.test(s.namespace))c.data=s.data,c.handleObj=s,o=((f.event.special[s.origType]||{}).handle||s.handler).apply(q.elem,g),o!==b&&(c.result=o,o===!1&&(c.preventDefault(),c.stopPropagation()))}}i.postDispatch&&i.postDispatch.call(this,c);return c.result}},props:"attrChange attrName relatedNode srcElement altKey bubbles cancelable ctrlKey currentTarget eventPhase metaKey relatedTarget shiftKey target timeStamp view which".split(" "),fixHooks:{},keyHooks:{props:"char charCode key keyCode".split(" "),filter:function(a,b){a.which==null&&(a.which=b.charCode!=null?b.charCode:b.keyCode);return a}},mouseHooks:{props:"button buttons clientX clientY fromElement offsetX offsetY pageX pageY screenX screenY toElement".split(" "),filter:function(a,d){var e,f,g,h=d.button,i=d.fromElement;a.pageX==null&&d.clientX!=null&&(e=a.target.ownerDocument||c,f=e.documentElement,g=e.body,a.pageX=d.clientX+(f&&f.scrollLeft||g&&g.scrollLeft||0)-(f&&f.clientLeft||g&&g.clientLeft||0),a.pageY=d.clientY+(f&&f.scrollTop||g&&g.scrollTop||0)-(f&&f.clientTop||g&&g.clientTop||0)),!a.relatedTarget&&i&&(a.relatedTarget=i===a.target?d.toElement:i),!a.which&&h!==b&&(a.which=h&1?1:h&2?3:h&4?2:0);return a}},fix:function(a){if(a[f.expando])return a;var d,e,g=a,h=f.event.fixHooks[a.type]||{},i=h.props?this.props.concat(h.props):this.props;a=f.Event(g);for(d=i.length;d;)e=i[--d],a[e]=g[e];a.target||(a.target=g.srcElement||c),a.target.nodeType===3&&(a.target=a.target.parentNode),a.metaKey===b&&(a.metaKey=a.ctrlKey);return h.filter?h.filter(a,g):a},special:{ready:{setup:f.bindReady},load:{noBubble:!0},focus:{delegateType:"focusin"},blur:{delegateType:"focusout"},beforeunload:{setup:function(a,b,c){f.isWindow(this)&&(this.onbeforeunload=c)},teardown:function(a,b){this.onbeforeunload===b&&(this.onbeforeunload=null)}}},simulate:function(a,b,c,d){var e=f.extend(new f.Event,c,{type:a,isSimulated:!0,originalEvent:{}});d?f.event.trigger(e,null,b):f.event.dispatch.call(b,e),e.isDefaultPrevented()&&c.preventDefault()}},f.event.handle=f.event.dispatch,f.removeEvent=c.removeEventListener?function(a,b,c){a.removeEventListener&&a.removeEventListener(b,c,!1)}:function(a,b,c){a.detachEvent&&a.detachEvent("on"+b,c)},f.Event=function(a,b){if(!(this instanceof f.Event))return new f.Event(a,b);a&&a.type?(this.originalEvent=a,this.type=a.type,this.isDefaultPrevented=a.defaultPrevented||a.returnValue===!1||a.getPreventDefault&&a.getPreventDefault()?K:J):this.type=a,b&&f.extend(this,b),this.timeStamp=a&&a.timeStamp||f.now(),this[f.expando]=!0},f.Event.prototype={preventDefault:function(){this.isDefaultPrevented=K;var a=this.originalEvent;!a||(a.preventDefault?a.preventDefault():a.returnValue=!1)},stopPropagation:function(){this.isPropagationStopped=K;var a=this.originalEvent;!a||(a.stopPropagation&&a.stopPropagation(),a.cancelBubble=!0)},stopImmediatePropagation:function(){this.isImmediatePropagationStopped=K,this.stopPropagation()},isDefaultPrevented:J,isPropagationStopped:J,isImmediatePropagationStopped:J},f.each({mouseenter:"mouseover",mouseleave:"mouseout"},function(a,b){f.event.special[a]={delegateType:b,bindType:b,handle:function(a){var c=this,d=a.relatedTarget,e=a.handleObj,g=e.selector,h;if(!d||d!==c&&!f.contains(c,d))a.type=e.origType,h=e.handler.apply(this,arguments),a.type=b;return h}}}),f.support.submitBubbles||(f.event.special.submit={setup:function(){if(f.nodeName(this,"form"))return!1;f.event.add(this,"click._submit keypress._submit",function(a){var c=a.target,d=f.nodeName(c,"input")||f.nodeName(c,"button")?c.form:b;d&&!d._submit_attached&&(f.event.add(d,"submit._submit",function(a){a._submit_bubble=!0}),d._submit_attached=!0)})},postDispatch:function(a){a._submit_bubble&&(delete a._submit_bubble,this.parentNode&&!a.isTrigger&&f.event.simulate("submit",this.parentNode,a,!0))},teardown:function(){if(f.nodeName(this,"form"))return!1;f.event.remove(this,"._submit")}}),f.support.changeBubbles||(f.event.special.change={setup:function(){if(z.test(this.nodeName)){if(this.type==="checkbox"||this.type==="radio")f.event.add(this,"propertychange._change",function(a){a.originalEvent.propertyName==="checked"&&(this._just_changed=!0)}),f.event.add(this,"click._change",function(a){this._just_changed&&!a.isTrigger&&(this._just_changed=!1,f.event.simulate("change",this,a,!0))});return!1}f.event.add(this,"beforeactivate._change",function(a){var b=a.target;z.test(b.nodeName)&&!b._change_attached&&(f.event.add(b,"change._change",function(a){this.parentNode&&!a.isSimulated&&!a.isTrigger&&f.event.simulate("change",this.parentNode,a,!0)}),b._change_attached=!0)})},handle:function(a){var b=a.target;if(this!==b||a.isSimulated||a.isTrigger||b.type!=="radio"&&b.type!=="checkbox")return a.handleObj.handler.apply(this,arguments)},teardown:function(){f.event.remove(this,"._change");return z.test(this.nodeName)}}),f.support.focusinBubbles||f.each({focus:"focusin",blur:"focusout"},function(a,b){var d=0,e=function(a){f.event.simulate(b,a.target,f.event.fix(a),!0)};f.event.special[b]={setup:function(){d++===0&&c.addEventListener(a,e,!0)},teardown:function(){--d===0&&c.removeEventListener(a,e,!0)}}}),f.fn.extend({on:function(a,c,d,e,g){var h,i;if(typeof a=="object"){typeof c!="string"&&(d=d||c,c=b);for(i in a)this.on(i,c,d,a[i],g);return this}d==null&&e==null?(e=c,d=c=b):e==null&&(typeof c=="string"?(e=d,d=b):(e=d,d=c,c=b));if(e===!1)e=J;else if(!e)return this;g===1&&(h=e,e=function(a){f().off(a);return h.apply(this,arguments)},e.guid=h.guid||(h.guid=f.guid++));return this.each(function(){f.event.add(this,a,e,d,c)})},one:function(a,b,c,d){return this.on(a,b,c,d,1)},off:function(a,c,d){if(a&&a.preventDefault&&a.handleObj){var e=a.handleObj;f(a.delegateTarget).off(e.namespace?e.origType+"."+e.namespace:e.origType,e.selector,e.handler);return this}if(typeof a=="object"){for(var g in a)this.off(g,c,a[g]);return this}if(c===!1||typeof c=="function")d=c,c=b;d===!1&&(d=J);return this.each(function(){f.event.remove(this,a,d,c)})},bind:function(a,b,c){return this.on(a,null,b,c)},unbind:function(a,b){return this.off(a,null,b)},live:function(a,b,c){f(this.context).on(a,this.selector,b,c);return this},die:function(a,b){f(this.context).off(a,this.selector||"**",b);return this},delegate:function(a,b,c,d){return this.on(b,a,c,d)},undelegate:function(a,b,c){return arguments.length==1?this.off(a,"**"):this.off(b,a,c)},trigger:function(a,b){return this.each(function(){f.event.trigger(a,b,this)})},triggerHandler:function(a,b){if(this[0])return f.event.trigger(a,b,this[0],!0)},toggle:function(a){var b=arguments,c=a.guid||f.guid++,d=0,e=function(c){var e=(f._data(this,"lastToggle"+a.guid)||0)%d;f._data(this,"lastToggle"+a.guid,e+1),c.preventDefault();return b[e].apply(this,arguments)||!1};e.guid=c;while(d<b.length)b[d++].guid=c;return this.click(e)},hover:function(a,b){return this.mouseenter(a).mouseleave(b||a)}}),f.each("blur focus focusin focusout load resize scroll unload click dblclick mousedown mouseup mousemove mouseover mouseout mouseenter mouseleave change select submit keydown keypress keyup error contextmenu".split(" "),function(a,b){f.fn[b]=function(a,c){c==null&&(c=a,a=null);return arguments.length>0?this.on(b,null,a,c):this.trigger(b)},f.attrFn&&(f.attrFn[b]=!0),C.test(b)&&(f.event.fixHooks[b]=f.event.keyHooks),D.test(b)&&(f.event.fixHooks[b]=f.event.mouseHooks)}),function(){function x(a,b,c,e,f,g){for(var h=0,i=e.length;h<i;h++){var j=e[h];if(j){var k=!1;j=j[a];while(j){if(j[d]===c){k=e[j.sizset];break}if(j.nodeType===1){g||(j[d]=c,j.sizset=h);if(typeof b!="string"){if(j===b){k=!0;break}}else if(m.filter(b,[j]).length>0){k=j;break}}j=j[a]}e[h]=k}}}function w(a,b,c,e,f,g){for(var h=0,i=e.length;h<i;h++){var j=e[h];if(j){var k=!1;j=j[a];while(j){if(j[d]===c){k=e[j.sizset];break}j.nodeType===1&&!g&&(j[d]=c,j.sizset=h);if(j.nodeName.toLowerCase()===b){k=j;break}j=j[a]}e[h]=k}}}var a=/((?:\((?:\([^()]+\)|[^()]+)+\)|\[(?:\[[^\[\]]*\]|['"][^'"]*['"]|[^\[\]'"]+)+\]|\\.|[^ >+~,(\[\\]+)+|[>+~])(\s*,\s*)?((?:.|\r|\n)*)/g,d="sizcache"+(Math.random()+"").replace(".",""),e=0,g=Object.prototype.toString,h=!1,i=!0,j=/\\/g,k=/\r\n/g,l=/\W/;[0,0].sort(function(){i=!1;return 0});var m=function(b,d,e,f){e=e||[],d=d||c;var h=d;if(d.nodeType!==1&&d.nodeType!==9)return[];if(!b||typeof b!="string")return e;var i,j,k,l,n,q,r,t,u=!0,v=m.isXML(d),w=[],x=b;do{a.exec(""),i=a.exec(x);if(i){x=i[3],w.push(i[1]);if(i[2]){l=i[3];break}}}while(i);if(w.length>1&&p.exec(b))if(w.length===2&&o.relative[w[0]])j=y(w[0]+w[1],d,f);else{j=o.relative[w[0]]?[d]:m(w.shift(),d);while(w.length)b=w.shift(),o.relative[b]&&(b+=w.shift()),j=y(b,j,f)}else{!f&&w.length>1&&d.nodeType===9&&!v&&o.match.ID.test(w[0])&&!o.match.ID.test(w[w.length-1])&&(n=m.find(w.shift(),d,v),d=n.expr?m.filter(n.expr,n.set)[0]:n.set[0]);if(d){n=f?{expr:w.pop(),set:s(f)}:m.find(w.pop(),w.length===1&&(w[0]==="~"||w[0]==="+")&&d.parentNode?d.parentNode:d,v),j=n.expr?m.filter(n.expr,n.set):n.set,w.length>0?k=s(j):u=!1;while(w.length)q=w.pop(),r=q,o.relative[q]?r=w.pop():q="",r==null&&(r=d),o.relative[q](k,r,v)}else k=w=[]}k||(k=j),k||m.error(q||b);if(g.call(k)==="[object Array]")if(!u)e.push.apply(e,k);else if(d&&d.nodeType===1)for(t=0;k[t]!=null;t++)k[t]&&(k[t]===!0||k[t].nodeType===1&&m.contains(d,k[t]))&&e.push(j[t]);else for(t=0;k[t]!=null;t++)k[t]&&k[t].nodeType===1&&e.push(j[t]);else s(k,e);l&&(m(l,h,e,f),m.uniqueSort(e));return e};m.uniqueSort=function(a){if(u){h=i,a.sort(u);if(h)for(var b=1;b<a.length;b++)a[b]===a[b-1]&&a.splice(b--,1)}return a},m.matches=function(a,b){return m(a,null,null,b)},m.matchesSelector=function(a,b){return m(b,null,null,[a]).length>0},m.find=function(a,b,c){var d,e,f,g,h,i;if(!a)return[];for(e=0,f=o.order.length;e<f;e++){h=o.order[e];if(g=o.leftMatch[h].exec(a)){i=g[1],g.splice(1,1);if(i.substr(i.length-1)!=="\\"){g[1]=(g[1]||"").replace(j,""),d=o.find[h](g,b,c);if(d!=null){a=a.replace(o.match[h],"");break}}}}d||(d=typeof b.getElementsByTagName!="undefined"?b.getElementsByTagName("*"):[]);return{set:d,expr:a}},m.filter=function(a,c,d,e){var f,g,h,i,j,k,l,n,p,q=a,r=[],s=c,t=c&&c[0]&&m.isXML(c[0]);while(a&&c.length){for(h in o.filter)if((f=o.leftMatch[h].exec(a))!=null&&f[2]){k=o.filter[h],l=f[1],g=!1,f.splice(1,1);if(l.substr(l.length-1)==="\\")continue;s===r&&(r=[]);if(o.preFilter[h]){f=o.preFilter[h](f,s,d,r,e,t);if(!f)g=i=!0;else if(f===!0)continue}if(f)for(n=0;(j=s[n])!=null;n++)j&&(i=k(j,f,n,s),p=e^i,d&&i!=null?p?g=!0:s[n]=!1:p&&(r.push(j),g=!0));if(i!==b){d||(s=r),a=a.replace(o.match[h],"");if(!g)return[];break}}if(a===q)if(g==null)m.error(a);else break;q=a}return s},m.error=function(a){throw new Error("Syntax error, unrecognized expression: "+a)};var n=m.getText=function(a){var b,c,d=a.nodeType,e="";if(d){if(d===1||d===9||d===11){if(typeof a.textContent=="string")return a.textContent;if(typeof a.innerText=="string")return a.innerText.replace(k,"");for(a=a.firstChild;a;a=a.nextSibling)e+=n(a)}else if(d===3||d===4)return a.nodeValue}else for(b=0;c=a[b];b++)c.nodeType!==8&&(e+=n(c));return e},o=m.selectors={order:["ID","NAME","TAG"],match:{ID:/#((?:[\w\u00c0-\uFFFF\-]|\\.)+)/,CLASS:/\.((?:[\w\u00c0-\uFFFF\-]|\\.)+)/,NAME:/\[name=['"]*((?:[\w\u00c0-\uFFFF\-]|\\.)+)['"]*\]/,ATTR:/\[\s*((?:[\w\u00c0-\uFFFF\-]|\\.)+)\s*(?:(\S?=)\s*(?:(['"])(.*?)\3|(#?(?:[\w\u00c0-\uFFFF\-]|\\.)*)|)|)\s*\]/,TAG:/^((?:[\w\u00c0-\uFFFF\*\-]|\\.)+)/,CHILD:/:(only|nth|last|first)-child(?:\(\s*(even|odd|(?:[+\-]?\d+|(?:[+\-]?\d*)?n\s*(?:[+\-]\s*\d+)?))\s*\))?/,POS:/:(nth|eq|gt|lt|first|last|even|odd)(?:\((\d*)\))?(?=[^\-]|$)/,PSEUDO:/:((?:[\w\u00c0-\uFFFF\-]|\\.)+)(?:\((['"]?)((?:\([^\)]+\)|[^\(\)]*)+)\2\))?/},leftMatch:{},attrMap:{"class":"className","for":"htmlFor"},attrHandle:{href:function(a){return a.getAttribute("href")},type:function(a){return a.getAttribute("type")}},relative:{"+":function(a,b){var c=typeof b=="string",d=c&&!l.test(b),e=c&&!d;d&&(b=b.toLowerCase());for(var f=0,g=a.length,h;f<g;f++)if(h=a[f]){while((h=h.previousSibling)&&h.nodeType!==1);a[f]=e||h&&h.nodeName.toLowerCase()===b?h||!1:h===b}e&&m.filter(b,a,!0)},">":function(a,b){var c,d=typeof b=="string",e=0,f=a.length;if(d&&!l.test(b)){b=b.toLowerCase();for(;e<f;e++){c=a[e];if(c){var g=c.parentNode;a[e]=g.nodeName.toLowerCase()===b?g:!1}}}else{for(;e<f;e++)c=a[e],c&&(a[e]=d?c.parentNode:c.parentNode===b);d&&m.filter(b,a,!0)}},"":function(a,b,c){var d,f=e++,g=x;typeof b=="string"&&!l.test(b)&&(b=b.toLowerCase(),d=b,g=w),g("parentNode",b,f,a,d,c)},"~":function(a,b,c){var d,f=e++,g=x;typeof b=="string"&&!l.test(b)&&(b=b.toLowerCase(),d=b,g=w),g("previousSibling",b,f,a,d,c)}},find:{ID:function(a,b,c){if(typeof b.getElementById!="undefined"&&!c){var d=b.getElementById(a[1]);return d&&d.parentNode?[d]:[]}},NAME:function(a,b){if(typeof b.getElementsByName!="undefined"){var c=[],d=b.getElementsByName(a[1]);for(var e=0,f=d.length;e<f;e++)d[e].getAttribute("name")===a[1]&&c.push(d[e]);return c.length===0?null:c}},TAG:function(a,b){if(typeof b.getElementsByTagName!="undefined")return b.getElementsByTagName(a[1])}},preFilter:{CLASS:function(a,b,c,d,e,f){a=" "+a[1].replace(j,"")+" ";if(f)return a;for(var g=0,h;(h=b[g])!=null;g++)h&&(e^(h.className&&(" "+h.className+" ").replace(/[\t\n\r]/g," ").indexOf(a)>=0)?c||d.push(h):c&&(b[g]=!1));return!1},ID:function(a){return a[1].replace(j,"")},TAG:function(a,b){return a[1].replace(j,"").toLowerCase()},CHILD:function(a){if(a[1]==="nth"){a[2]||m.error(a[0]),a[2]=a[2].replace(/^\+|\s*/g,"");var b=/(-?)(\d*)(?:n([+\-]?\d*))?/.exec(a[2]==="even"&&"2n"||a[2]==="odd"&&"2n+1"||!/\D/.test(a[2])&&"0n+"+a[2]||a[2]);a[2]=b[1]+(b[2]||1)-0,a[3]=b[3]-0}else a[2]&&m.error(a[0]);a[0]=e++;return a},ATTR:function(a,b,c,d,e,f){var g=a[1]=a[1].replace(j,"");!f&&o.attrMap[g]&&(a[1]=o.attrMap[g]),a[4]=(a[4]||a[5]||"").replace(j,""),a[2]==="~="&&(a[4]=" "+a[4]+" ");return a},PSEUDO:function(b,c,d,e,f){if(b[1]==="not")if((a.exec(b[3])||"").length>1||/^\w/.test(b[3]))b[3]=m(b[3],null,null,c);else{var g=m.filter(b[3],c,d,!0^f);d||e.push.apply(e,g);return!1}else if(o.match.POS.test(b[0])||o.match.CHILD.test(b[0]))return!0;return b},POS:function(a){a.unshift(!0);return a}},filters:{enabled:function(a){return a.disabled===!1&&a.type!=="hidden"},disabled:function(a){return a.disabled===!0},checked:function(a){return a.checked===!0},selected:function(a){a.parentNode&&a.parentNode.selectedIndex;return a.selected===!0},parent:function(a){return!!a.firstChild},empty:function(a){return!a.firstChild},has:function(a,b,c){return!!m(c[3],a).length},header:function(a){return/h\d/i.test(a.nodeName)},text:function(a){var b=a.getAttribute("type"),c=a.type;return a.nodeName.toLowerCase()==="input"&&"text"===c&&(b===c||b===null)},radio:function(a){return a.nodeName.toLowerCase()==="input"&&"radio"===a.type},checkbox:function(a){return a.nodeName.toLowerCase()==="input"&&"checkbox"===a.type},file:function(a){return a.nodeName.toLowerCase()==="input"&&"file"===a.type},password:function(a){return a.nodeName.toLowerCase()==="input"&&"password"===a.type},submit:function(a){var b=a.nodeName.toLowerCase();return(b==="input"||b==="button")&&"submit"===a.type},image:function(a){return a.nodeName.toLowerCase()==="input"&&"image"===a.type},reset:function(a){var b=a.nodeName.toLowerCase();return(b==="input"||b==="button")&&"reset"===a.type},button:function(a){var b=a.nodeName.toLowerCase();return b==="input"&&"button"===a.type||b==="button"},input:function(a){return/input|select|textarea|button/i.test(a.nodeName)},focus:function(a){return a===a.ownerDocument.activeElement}},setFilters:{first:function(a,b){return b===0},last:function(a,b,c,d){return b===d.length-1},even:function(a,b){return b%2===0},odd:function(a,b){return b%2===1},lt:function(a,b,c){return b<c[3]-0},gt:function(a,b,c){return b>c[3]-0},nth:function(a,b,c){return c[3]-0===b},eq:function(a,b,c){return c[3]-0===b}},filter:{PSEUDO:function(a,b,c,d){var e=b[1],f=o.filters[e];if(f)return f(a,c,b,d);if(e==="contains")return(a.textContent||a.innerText||n([a])||"").indexOf(b[3])>=0;if(e==="not"){var g=b[3];for(var h=0,i=g.length;h<i;h++)if(g[h]===a)return!1;return!0}m.error(e)},CHILD:function(a,b){var c,e,f,g,h,i,j,k=b[1],l=a;switch(k){case"only":case"first":while(l=l.previousSibling)if(l.nodeType===1)return!1;if(k==="first")return!0;l=a;case"last":while(l=l.nextSibling)if(l.nodeType===1)return!1;return!0;case"nth":c=b[2],e=b[3];if(c===1&&e===0)return!0;f=b[0],g=a.parentNode;if(g&&(g[d]!==f||!a.nodeIndex)){i=0;for(l=g.firstChild;l;l=l.nextSibling)l.nodeType===1&&(l.nodeIndex=++i);g[d]=f}j=a.nodeIndex-e;return c===0?j===0:j%c===0&&j/c>=0}},ID:function(a,b){return a.nodeType===1&&a.getAttribute("id")===b},TAG:function(a,b){return b==="*"&&a.nodeType===1||!!a.nodeName&&a.nodeName.toLowerCase()===b},CLASS:function(a,b){return(" "+(a.className||a.getAttribute("class"))+" ").indexOf(b)>-1},ATTR:function(a,b){var c=b[1],d=m.attr?m.attr(a,c):o.attrHandle[c]?o.attrHandle[c](a):a[c]!=null?a[c]:a.getAttribute(c),e=d+"",f=b[2],g=b[4];return d==null?f==="!=":!f&&m.attr?d!=null:f==="="?e===g:f==="*="?e.indexOf(g)>=0:f==="~="?(" "+e+" ").indexOf(g)>=0:g?f==="!="?e!==g:f==="^="?e.indexOf(g)===0:f==="$="?e.substr(e.length-g.length)===g:f==="|="?e===g||e.substr(0,g.length+1)===g+"-":!1:e&&d!==!1},POS:function(a,b,c,d){var e=b[2],f=o.setFilters[e];if(f)return f(a,c,b,d)}}},p=o.match.POS,q=function(a,b){return"\\"+(b-0+1)};for(var r in o.match)o.match[r]=new RegExp(o.match[r].source+/(?![^\[]*\])(?![^\(]*\))/.source),o.leftMatch[r]=new RegExp(/(^(?:.|\r|\n)*?)/.source+o.match[r].source.replace(/\\(\d+)/g,q));o.match.globalPOS=p;var s=function(a,b){a=Array.prototype.slice.call(a,0);if(b){b.push.apply(b,a);return b}return a};try{Array.prototype.slice.call(c.documentElement.childNodes,0)[0].nodeType}catch(t){s=function(a,b){var c=0,d=b||[];if(g.call(a)==="[object Array]")Array.prototype.push.apply(d,a);else if(typeof a.length=="number")for(var e=a.length;c<e;c++)d.push(a[c]);else for(;a[c];c++)d.push(a[c]);return d}}var u,v;c.documentElement.compareDocumentPosition?u=function(a,b){if(a===b){h=!0;return 0}if(!a.compareDocumentPosition||!b.compareDocumentPosition)return a.compareDocumentPosition?-1:1;return a.compareDocumentPosition(b)&4?-1:1}:(u=function(a,b){if(a===b){h=!0;return 0}if(a.sourceIndex&&b.sourceIndex)return a.sourceIndex-b.sourceIndex;var c,d,e=[],f=[],g=a.parentNode,i=b.parentNode,j=g;if(g===i)return v(a,b);if(!g)return-1;if(!i)return 1;while(j)e.unshift(j),j=j.parentNode;j=i;while(j)f.unshift(j),j=j.parentNode;c=e.length,d=f.length;for(var k=0;k<c&&k<d;k++)if(e[k]!==f[k])return v(e[k],f[k]);return k===c?v(a,f[k],-1):v(e[k],b,1)},v=function(a,b,c){if(a===b)return c;var d=a.nextSibling;while(d){if(d===b)return-1;d=d.nextSibling}return 1}),function(){var a=c.createElement("div"),d="script"+(new Date).getTime(),e=c.documentElement;a.innerHTML="<a name='"+d+"'/>",e.insertBefore(a,e.firstChild),c.getElementById(d)&&(o.find.ID=function(a,c,d){if(typeof c.getElementById!="undefined"&&!d){var e=c.getElementById(a[1]);return e?e.id===a[1]||typeof e.getAttributeNode!="undefined"&&e.getAttributeNode("id").nodeValue===a[1]?[e]:b:[]}},o.filter.ID=function(a,b){var c=typeof a.getAttributeNode!="undefined"&&a.getAttributeNode("id");return a.nodeType===1&&c&&c.nodeValue===b}),e.removeChild(a),e=a=null}(),function(){var a=c.createElement("div");a.appendChild(c.createComment("")),a.getElementsByTagName("*").length>0&&(o.find.TAG=function(a,b){var c=b.getElementsByTagName(a[1]);if(a[1]==="*"){var d=[];for(var e=0;c[e];e++)c[e].nodeType===1&&d.push(c[e]);c=d}return c}),a.innerHTML="<a href='#'></a>",a.firstChild&&typeof a.firstChild.getAttribute!="undefined"&&a.firstChild.getAttribute("href")!=="#"&&(o.attrHandle.href=function(a){return a.getAttribute("href",2)}),a=null}(),c.querySelectorAll&&function(){var a=m,b=c.createElement("div"),d="__sizzle__";b.innerHTML="<p class='TEST'></p>";if(!b.querySelectorAll||b.querySelectorAll(".TEST").length!==0){m=function(b,e,f,g){e=e||c;if(!g&&!m.isXML(e)){var h=/^(\w+$)|^\.([\w\-]+$)|^#([\w\-]+$)/.exec(b);if(h&&(e.nodeType===1||e.nodeType===9)){if(h[1])return s(e.getElementsByTagName(b),f);if(h[2]&&o.find.CLASS&&e.getElementsByClassName)return s(e.getElementsByClassName(h[2]),f)}if(e.nodeType===9){if(b==="body"&&e.body)return s([e.body],f);if(h&&h[3]){var i=e.getElementById(h[3]);if(!i||!i.parentNode)return s([],f);if(i.id===h[3])return s([i],f)}try{return s(e.querySelectorAll(b),f)}catch(j){}}else if(e.nodeType===1&&e.nodeName.toLowerCase()!=="object"){var k=e,l=e.getAttribute("id"),n=l||d,p=e.parentNode,q=/^\s*[+~]/.test(b);l?n=n.replace(/'/g,"\\$&"):e.setAttribute("id",n),q&&p&&(e=e.parentNode);try{if(!q||p)return s(e.querySelectorAll("[id='"+n+"'] "+b),f)}catch(r){}finally{l||k.removeAttribute("id")}}}return a(b,e,f,g)};for(var e in a)m[e]=a[e];b=null}}(),function(){var a=c.documentElement,b=a.matchesSelector||a.mozMatchesSelector||a.webkitMatchesSelector||a.msMatchesSelector;if(b){var d=!b.call(c.createElement("div"),"div"),e=!1;try{b.call(c.documentElement,"[test!='']:sizzle")}catch(f){e=!0}m.matchesSelector=function(a,c){c=c.replace(/\=\s*([^'"\]]*)\s*\]/g,"='$1']");if(!m.isXML(a))try{if(e||!o.match.PSEUDO.test(c)&&!/!=/.test(c)){var f=b.call(a,c);if(f||!d||a.document&&a.document.nodeType!==11)return f}}catch(g){}return m(c,null,null,[a]).length>0}}}(),function(){var a=c.createElement("div");a.innerHTML="<div class='test e'></div><div class='test'></div>";if(!!a.getElementsByClassName&&a.getElementsByClassName("e").length!==0){a.lastChild.className="e";if(a.getElementsByClassName("e").length===1)return;o.order.splice(1,0,"CLASS"),o.find.CLASS=function(a,b,c){if(typeof b.getElementsByClassName!="undefined"&&!c)return b.getElementsByClassName(a[1])},a=null}}(),c.documentElement.contains?m.contains=function(a,b){return a!==b&&(a.contains?a.contains(b):!0)}:c.documentElement.compareDocumentPosition?m.contains=function(a,b){return!!(a.compareDocumentPosition(b)&16)}:m.contains=function(){return!1},m.isXML=function(a){var b=(a?a.ownerDocument||a:0).documentElement;return b?b.nodeName!=="HTML":!1};var y=function(a,b,c){var d,e=[],f="",g=b.nodeType?[b]:b;while(d=o.match.PSEUDO.exec(a))f+=d[0],a=a.replace(o.match.PSEUDO,"");a=o.relative[a]?a+"*":a;for(var h=0,i=g.length;h<i;h++)m(a,g[h],e,c);return m.filter(f,e)};m.attr=f.attr,m.selectors.attrMap={},f.find=m,f.expr=m.selectors,f.expr[":"]=f.expr.filters,f.unique=m.uniqueSort,f.text=m.getText,f.isXMLDoc=m.isXML,f.contains=m.contains}();var L=/Until$/,M=/^(?:parents|prevUntil|prevAll)/,N=/,/,O=/^.[^:#\[\.,]*$/,P=Array.prototype.slice,Q=f.expr.match.globalPOS,R={children:!0,contents:!0,next:!0,prev:!0};f.fn.extend({find:function(a){var b=this,c,d;if(typeof a!="string")return f(a).filter(function(){for(c=0,d=b.length;c<d;c++)if(f.contains(b[c],this))return!0});var e=this.pushStack("","find",a),g,h,i;for(c=0,d=this.length;c<d;c++){g=e.length,f.find(a,this[c],e);if(c>0)for(h=g;h<e.length;h++)for(i=0;i<g;i++)if(e[i]===e[h]){e.splice(h--,1);break}}return e},has:function(a){var b=f(a);return this.filter(function(){for(var a=0,c=b.length;a<c;a++)if(f.contains(this,b[a]))return!0})},not:function(a){return this.pushStack(T(this,a,!1),"not",a)},filter:function(a){return this.pushStack(T(this,a,!0),"filter",a)},is:function(a){return!!a&&(typeof a=="string"?Q.test(a)?f(a,this.context).index(this[0])>=0:f.filter(a,this).length>0:this.filter(a).length>0)},closest:function(a,b){var c=[],d,e,g=this[0];if(f.isArray(a)){var h=1;while(g&&g.ownerDocument&&g!==b){for(d=0;d<a.length;d++)f(g).is(a[d])&&c.push({selector:a[d],elem:g,level:h});g=g.parentNode,h++}return c}var i=Q.test(a)||typeof a!="string"?f(a,b||this.context):0;for(d=0,e=this.length;d<e;d++){g=this[d];while(g){if(i?i.index(g)>-1:f.find.matchesSelector(g,a)){c.push(g);break}g=g.parentNode;if(!g||!g.ownerDocument||g===b||g.nodeType===11)break}}c=c.length>1?f.unique(c):c;return this.pushStack(c,"closest",a)},index:function(a){if(!a)return this[0]&&this[0].parentNode?this.prevAll().length:-1;if(typeof a=="string")return f.inArray(this[0],f(a));return f.inArray(a.jquery?a[0]:a,this)},add:function(a,b){var c=typeof a=="string"?f(a,b):f.makeArray(a&&a.nodeType?[a]:a),d=f.merge(this.get(),c);return this.pushStack(S(c[0])||S(d[0])?d:f.unique(d))},andSelf:function(){return this.add(this.prevObject)}}),f.each({parent:function(a){var b=a.parentNode;return b&&b.nodeType!==11?b:null},parents:function(a){return f.dir(a,"parentNode")},parentsUntil:function(a,b,c){return f.dir(a,"parentNode",c)},next:function(a){return f.nth(a,2,"nextSibling")},prev:function(a){return f.nth(a,2,"previousSibling")},nextAll:function(a){return f.dir(a,"nextSibling")},prevAll:function(a){return f.dir(a,"previousSibling")},nextUntil:function(a,b,c){return f.dir(a,"nextSibling",c)},prevUntil:function(a,b,c){return f.dir(a,"previousSibling",c)},siblings:function(a){return f.sibling((a.parentNode||{}).firstChild,a)},children:function(a){return f.sibling(a.firstChild)},contents:function(a){return f.nodeName(a,"iframe")?a.contentDocument||a.contentWindow.document:f.makeArray(a.childNodes)}},function(a,b){f.fn[a]=function(c,d){var e=f.map(this,b,c);L.test(a)||(d=c),d&&typeof d=="string"&&(e=f.filter(d,e)),e=this.length>1&&!R[a]?f.unique(e):e,(this.length>1||N.test(d))&&M.test(a)&&(e=e.reverse());return this.pushStack(e,a,P.call(arguments).join(","))}}),f.extend({filter:function(a,b,c){c&&(a=":not("+a+")");return b.length===1?f.find.matchesSelector(b[0],a)?[b[0]]:[]:f.find.matches(a,b)},dir:function(a,c,d){var e=[],g=a[c];while(g&&g.nodeType!==9&&(d===b||g.nodeType!==1||!f(g).is(d)))g.nodeType===1&&e.push(g),g=g[c];return e},nth:function(a,b,c,d){b=b||1;var e=0;for(;a;a=a[c])if(a.nodeType===1&&++e===b)break;return a},sibling:function(a,b){var c=[];for(;a;a=a.nextSibling)a.nodeType===1&&a!==b&&c.push(a);return c}});var V="abbr|article|aside|audio|bdi|canvas|data|datalist|details|figcaption|figure|footer|header|hgroup|mark|meter|nav|output|progress|section|summary|time|video",W=/ jQuery\d+="(?:\d+|null)"/g,X=/^\s+/,Y=/<(?!area|br|col|embed|hr|img|input|link|meta|param)(([\w:]+)[^>]*)\/>/ig,Z=/<([\w:]+)/,$=/<tbody/i,_=/<|&#?\w+;/,ba=/<(?:script|style)/i,bb=/<(?:script|object|embed|option|style)/i,bc=new RegExp("<(?:"+V+")[\\s/>]","i"),bd=/checked\s*(?:[^=]|=\s*.checked.)/i,be=/\/(java|ecma)script/i,bf=/^\s*<!(?:\[CDATA\[|\-\-)/,bg={option:[1,"<select multiple='multiple'>","</select>"],legend:[1,"<fieldset>","</fieldset>"],thead:[1,"<table>","</table>"],tr:[2,"<table><tbody>","</tbody></table>"],td:[3,"<table><tbody><tr>","</tr></tbody></table>"],col:[2,"<table><tbody></tbody><colgroup>","</colgroup></table>"],area:[1,"<map>","</map>"],_default:[0,"",""]},bh=U(c);bg.optgroup=bg.option,bg.tbody=bg.tfoot=bg.colgroup=bg.caption=bg.thead,bg.th=bg.td,f.support.htmlSerialize||(bg._default=[1,"div<div>","</div>"]),f.fn.extend({text:function(a){return f.access(this,function(a){return a===b?f.text(this):this.empty().append((this[0]&&this[0].ownerDocument||c).createTextNode(a))},null,a,arguments.length)},wrapAll:function(a){if(f.isFunction(a))return this.each(function(b){f(this).wrapAll(a.call(this,b))});if(this[0]){var b=f(a,this[0].ownerDocument).eq(0).clone(!0);this[0].parentNode&&b.insertBefore(this[0]),b.map(function(){var a=this;while(a.firstChild&&a.firstChild.nodeType===1)a=a.firstChild;return a}).append(this)}return this},wrapInner:function(a){if(f.isFunction(a))return this.each(function(b){f(this).wrapInner(a.call(this,b))});return this.each(function(){var b=f(this),c=b.contents();c.length?c.wrapAll(a):b.append(a)})},wrap:function(a){var b=f.isFunction(a);return this.each(function(c){f(this).wrapAll(b?a.call(this,c):a)})},unwrap:function(){return this.parent().each(function(){f.nodeName(this,"body")||f(this).replaceWith(this.childNodes)}).end()},append:function(){return this.domManip(arguments,!0,function(a){this.nodeType===1&&this.appendChild(a)})},prepend:function(){return this.domManip(arguments,!0,function(a){this.nodeType===1&&this.insertBefore(a,this.firstChild)})},before:function(){if(this[0]&&this[0].parentNode)return this.domManip(arguments,!1,function(a){this.parentNode.insertBefore(a,this)});if(arguments.length){var a=f
.clean(arguments);a.push.apply(a,this.toArray());return this.pushStack(a,"before",arguments)}},after:function(){if(this[0]&&this[0].parentNode)return this.domManip(arguments,!1,function(a){this.parentNode.insertBefore(a,this.nextSibling)});if(arguments.length){var a=this.pushStack(this,"after",arguments);a.push.apply(a,f.clean(arguments));return a}},remove:function(a,b){for(var c=0,d;(d=this[c])!=null;c++)if(!a||f.filter(a,[d]).length)!b&&d.nodeType===1&&(f.cleanData(d.getElementsByTagName("*")),f.cleanData([d])),d.parentNode&&d.parentNode.removeChild(d);return this},empty:function(){for(var a=0,b;(b=this[a])!=null;a++){b.nodeType===1&&f.cleanData(b.getElementsByTagName("*"));while(b.firstChild)b.removeChild(b.firstChild)}return this},clone:function(a,b){a=a==null?!1:a,b=b==null?a:b;return this.map(function(){return f.clone(this,a,b)})},html:function(a){return f.access(this,function(a){var c=this[0]||{},d=0,e=this.length;if(a===b)return c.nodeType===1?c.innerHTML.replace(W,""):null;if(typeof a=="string"&&!ba.test(a)&&(f.support.leadingWhitespace||!X.test(a))&&!bg[(Z.exec(a)||["",""])[1].toLowerCase()]){a=a.replace(Y,"<$1></$2>");try{for(;d<e;d++)c=this[d]||{},c.nodeType===1&&(f.cleanData(c.getElementsByTagName("*")),c.innerHTML=a);c=0}catch(g){}}c&&this.empty().append(a)},null,a,arguments.length)},replaceWith:function(a){if(this[0]&&this[0].parentNode){if(f.isFunction(a))return this.each(function(b){var c=f(this),d=c.html();c.replaceWith(a.call(this,b,d))});typeof a!="string"&&(a=f(a).detach());return this.each(function(){var b=this.nextSibling,c=this.parentNode;f(this).remove(),b?f(b).before(a):f(c).append(a)})}return this.length?this.pushStack(f(f.isFunction(a)?a():a),"replaceWith",a):this},detach:function(a){return this.remove(a,!0)},domManip:function(a,c,d){var e,g,h,i,j=a[0],k=[];if(!f.support.checkClone&&arguments.length===3&&typeof j=="string"&&bd.test(j))return this.each(function(){f(this).domManip(a,c,d,!0)});if(f.isFunction(j))return this.each(function(e){var g=f(this);a[0]=j.call(this,e,c?g.html():b),g.domManip(a,c,d)});if(this[0]){i=j&&j.parentNode,f.support.parentNode&&i&&i.nodeType===11&&i.childNodes.length===this.length?e={fragment:i}:e=f.buildFragment(a,this,k),h=e.fragment,h.childNodes.length===1?g=h=h.firstChild:g=h.firstChild;if(g){c=c&&f.nodeName(g,"tr");for(var l=0,m=this.length,n=m-1;l<m;l++)d.call(c?bi(this[l],g):this[l],e.cacheable||m>1&&l<n?f.clone(h,!0,!0):h)}k.length&&f.each(k,function(a,b){b.src?f.ajax({type:"GET",global:!1,url:b.src,async:!1,dataType:"script"}):f.globalEval((b.text||b.textContent||b.innerHTML||"").replace(bf,"/*$0*/")),b.parentNode&&b.parentNode.removeChild(b)})}return this}}),f.buildFragment=function(a,b,d){var e,g,h,i,j=a[0];b&&b[0]&&(i=b[0].ownerDocument||b[0]),i.createDocumentFragment||(i=c),a.length===1&&typeof j=="string"&&j.length<512&&i===c&&j.charAt(0)==="<"&&!bb.test(j)&&(f.support.checkClone||!bd.test(j))&&(f.support.html5Clone||!bc.test(j))&&(g=!0,h=f.fragments[j],h&&h!==1&&(e=h)),e||(e=i.createDocumentFragment(),f.clean(a,i,e,d)),g&&(f.fragments[j]=h?e:1);return{fragment:e,cacheable:g}},f.fragments={},f.each({appendTo:"append",prependTo:"prepend",insertBefore:"before",insertAfter:"after",replaceAll:"replaceWith"},function(a,b){f.fn[a]=function(c){var d=[],e=f(c),g=this.length===1&&this[0].parentNode;if(g&&g.nodeType===11&&g.childNodes.length===1&&e.length===1){e[b](this[0]);return this}for(var h=0,i=e.length;h<i;h++){var j=(h>0?this.clone(!0):this).get();f(e[h])[b](j),d=d.concat(j)}return this.pushStack(d,a,e.selector)}}),f.extend({clone:function(a,b,c){var d,e,g,h=f.support.html5Clone||f.isXMLDoc(a)||!bc.test("<"+a.nodeName+">")?a.cloneNode(!0):bo(a);if((!f.support.noCloneEvent||!f.support.noCloneChecked)&&(a.nodeType===1||a.nodeType===11)&&!f.isXMLDoc(a)){bk(a,h),d=bl(a),e=bl(h);for(g=0;d[g];++g)e[g]&&bk(d[g],e[g])}if(b){bj(a,h);if(c){d=bl(a),e=bl(h);for(g=0;d[g];++g)bj(d[g],e[g])}}d=e=null;return h},clean:function(a,b,d,e){var g,h,i,j=[];b=b||c,typeof b.createElement=="undefined"&&(b=b.ownerDocument||b[0]&&b[0].ownerDocument||c);for(var k=0,l;(l=a[k])!=null;k++){typeof l=="number"&&(l+="");if(!l)continue;if(typeof l=="string")if(!_.test(l))l=b.createTextNode(l);else{l=l.replace(Y,"<$1></$2>");var m=(Z.exec(l)||["",""])[1].toLowerCase(),n=bg[m]||bg._default,o=n[0],p=b.createElement("div"),q=bh.childNodes,r;b===c?bh.appendChild(p):U(b).appendChild(p),p.innerHTML=n[1]+l+n[2];while(o--)p=p.lastChild;if(!f.support.tbody){var s=$.test(l),t=m==="table"&&!s?p.firstChild&&p.firstChild.childNodes:n[1]==="<table>"&&!s?p.childNodes:[];for(i=t.length-1;i>=0;--i)f.nodeName(t[i],"tbody")&&!t[i].childNodes.length&&t[i].parentNode.removeChild(t[i])}!f.support.leadingWhitespace&&X.test(l)&&p.insertBefore(b.createTextNode(X.exec(l)[0]),p.firstChild),l=p.childNodes,p&&(p.parentNode.removeChild(p),q.length>0&&(r=q[q.length-1],r&&r.parentNode&&r.parentNode.removeChild(r)))}var u;if(!f.support.appendChecked)if(l[0]&&typeof (u=l.length)=="number")for(i=0;i<u;i++)bn(l[i]);else bn(l);l.nodeType?j.push(l):j=f.merge(j,l)}if(d){g=function(a){return!a.type||be.test(a.type)};for(k=0;j[k];k++){h=j[k];if(e&&f.nodeName(h,"script")&&(!h.type||be.test(h.type)))e.push(h.parentNode?h.parentNode.removeChild(h):h);else{if(h.nodeType===1){var v=f.grep(h.getElementsByTagName("script"),g);j.splice.apply(j,[k+1,0].concat(v))}d.appendChild(h)}}}return j},cleanData:function(a){var b,c,d=f.cache,e=f.event.special,g=f.support.deleteExpando;for(var h=0,i;(i=a[h])!=null;h++){if(i.nodeName&&f.noData[i.nodeName.toLowerCase()])continue;c=i[f.expando];if(c){b=d[c];if(b&&b.events){for(var j in b.events)e[j]?f.event.remove(i,j):f.removeEvent(i,j,b.handle);b.handle&&(b.handle.elem=null)}g?delete i[f.expando]:i.removeAttribute&&i.removeAttribute(f.expando),delete d[c]}}}});var bp=/alpha\([^)]*\)/i,bq=/opacity=([^)]*)/,br=/([A-Z]|^ms)/g,bs=/^[\-+]?(?:\d*\.)?\d+$/i,bt=/^-?(?:\d*\.)?\d+(?!px)[^\d\s]+$/i,bu=/^([\-+])=([\-+.\de]+)/,bv=/^margin/,bw={position:"absolute",visibility:"hidden",display:"block"},bx=["Top","Right","Bottom","Left"],by,bz,bA;f.fn.css=function(a,c){return f.access(this,function(a,c,d){return d!==b?f.style(a,c,d):f.css(a,c)},a,c,arguments.length>1)},f.extend({cssHooks:{opacity:{get:function(a,b){if(b){var c=by(a,"opacity");return c===""?"1":c}return a.style.opacity}}},cssNumber:{fillOpacity:!0,fontWeight:!0,lineHeight:!0,opacity:!0,orphans:!0,widows:!0,zIndex:!0,zoom:!0},cssProps:{"float":f.support.cssFloat?"cssFloat":"styleFloat"},style:function(a,c,d,e){if(!!a&&a.nodeType!==3&&a.nodeType!==8&&!!a.style){var g,h,i=f.camelCase(c),j=a.style,k=f.cssHooks[i];c=f.cssProps[i]||i;if(d===b){if(k&&"get"in k&&(g=k.get(a,!1,e))!==b)return g;return j[c]}h=typeof d,h==="string"&&(g=bu.exec(d))&&(d=+(g[1]+1)*+g[2]+parseFloat(f.css(a,c)),h="number");if(d==null||h==="number"&&isNaN(d))return;h==="number"&&!f.cssNumber[i]&&(d+="px");if(!k||!("set"in k)||(d=k.set(a,d))!==b)try{j[c]=d}catch(l){}}},css:function(a,c,d){var e,g;c=f.camelCase(c),g=f.cssHooks[c],c=f.cssProps[c]||c,c==="cssFloat"&&(c="float");if(g&&"get"in g&&(e=g.get(a,!0,d))!==b)return e;if(by)return by(a,c)},swap:function(a,b,c){var d={},e,f;for(f in b)d[f]=a.style[f],a.style[f]=b[f];e=c.call(a);for(f in b)a.style[f]=d[f];return e}}),f.curCSS=f.css,c.defaultView&&c.defaultView.getComputedStyle&&(bz=function(a,b){var c,d,e,g,h=a.style;b=b.replace(br,"-$1").toLowerCase(),(d=a.ownerDocument.defaultView)&&(e=d.getComputedStyle(a,null))&&(c=e.getPropertyValue(b),c===""&&!f.contains(a.ownerDocument.documentElement,a)&&(c=f.style(a,b))),!f.support.pixelMargin&&e&&bv.test(b)&&bt.test(c)&&(g=h.width,h.width=c,c=e.width,h.width=g);return c}),c.documentElement.currentStyle&&(bA=function(a,b){var c,d,e,f=a.currentStyle&&a.currentStyle[b],g=a.style;f==null&&g&&(e=g[b])&&(f=e),bt.test(f)&&(c=g.left,d=a.runtimeStyle&&a.runtimeStyle.left,d&&(a.runtimeStyle.left=a.currentStyle.left),g.left=b==="fontSize"?"1em":f,f=g.pixelLeft+"px",g.left=c,d&&(a.runtimeStyle.left=d));return f===""?"auto":f}),by=bz||bA,f.each(["height","width"],function(a,b){f.cssHooks[b]={get:function(a,c,d){if(c)return a.offsetWidth!==0?bB(a,b,d):f.swap(a,bw,function(){return bB(a,b,d)})},set:function(a,b){return bs.test(b)?b+"px":b}}}),f.support.opacity||(f.cssHooks.opacity={get:function(a,b){return bq.test((b&&a.currentStyle?a.currentStyle.filter:a.style.filter)||"")?parseFloat(RegExp.$1)/100+"":b?"1":""},set:function(a,b){var c=a.style,d=a.currentStyle,e=f.isNumeric(b)?"alpha(opacity="+b*100+")":"",g=d&&d.filter||c.filter||"";c.zoom=1;if(b>=1&&f.trim(g.replace(bp,""))===""){c.removeAttribute("filter");if(d&&!d.filter)return}c.filter=bp.test(g)?g.replace(bp,e):g+" "+e}}),f(function(){f.support.reliableMarginRight||(f.cssHooks.marginRight={get:function(a,b){return f.swap(a,{display:"inline-block"},function(){return b?by(a,"margin-right"):a.style.marginRight})}})}),f.expr&&f.expr.filters&&(f.expr.filters.hidden=function(a){var b=a.offsetWidth,c=a.offsetHeight;return b===0&&c===0||!f.support.reliableHiddenOffsets&&(a.style&&a.style.display||f.css(a,"display"))==="none"},f.expr.filters.visible=function(a){return!f.expr.filters.hidden(a)}),f.each({margin:"",padding:"",border:"Width"},function(a,b){f.cssHooks[a+b]={expand:function(c){var d,e=typeof c=="string"?c.split(" "):[c],f={};for(d=0;d<4;d++)f[a+bx[d]+b]=e[d]||e[d-2]||e[0];return f}}});var bC=/%20/g,bD=/\[\]$/,bE=/\r?\n/g,bF=/#.*$/,bG=/^(.*?):[ \t]*([^\r\n]*)\r?$/mg,bH=/^(?:color|date|datetime|datetime-local|email|hidden|month|number|password|range|search|tel|text|time|url|week)$/i,bI=/^(?:about|app|app\-storage|.+\-extension|file|res|widget):$/,bJ=/^(?:GET|HEAD)$/,bK=/^\/\//,bL=/\?/,bM=/<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>/gi,bN=/^(?:select|textarea)/i,bO=/\s+/,bP=/([?&])_=[^&]*/,bQ=/^([\w\+\.\-]+:)(?:\/\/([^\/?#:]*)(?::(\d+))?)?/,bR=f.fn.load,bS={},bT={},bU,bV,bW=["*/"]+["*"];try{bU=e.href}catch(bX){bU=c.createElement("a"),bU.href="",bU=bU.href}bV=bQ.exec(bU.toLowerCase())||[],f.fn.extend({load:function(a,c,d){if(typeof a!="string"&&bR)return bR.apply(this,arguments);if(!this.length)return this;var e=a.indexOf(" ");if(e>=0){var g=a.slice(e,a.length);a=a.slice(0,e)}var h="GET";c&&(f.isFunction(c)?(d=c,c=b):typeof c=="object"&&(c=f.param(c,f.ajaxSettings.traditional),h="POST"));var i=this;f.ajax({url:a,type:h,dataType:"html",data:c,complete:function(a,b,c){c=a.responseText,a.isResolved()&&(a.done(function(a){c=a}),i.html(g?f("<div>").append(c.replace(bM,"")).find(g):c)),d&&i.each(d,[c,b,a])}});return this},serialize:function(){return f.param(this.serializeArray())},serializeArray:function(){return this.map(function(){return this.elements?f.makeArray(this.elements):this}).filter(function(){return this.name&&!this.disabled&&(this.checked||bN.test(this.nodeName)||bH.test(this.type))}).map(function(a,b){var c=f(this).val();return c==null?null:f.isArray(c)?f.map(c,function(a,c){return{name:b.name,value:a.replace(bE,"\r\n")}}):{name:b.name,value:c.replace(bE,"\r\n")}}).get()}}),f.each("ajaxStart ajaxStop ajaxComplete ajaxError ajaxSuccess ajaxSend".split(" "),function(a,b){f.fn[b]=function(a){return this.on(b,a)}}),f.each(["get","post"],function(a,c){f[c]=function(a,d,e,g){f.isFunction(d)&&(g=g||e,e=d,d=b);return f.ajax({type:c,url:a,data:d,success:e,dataType:g})}}),f.extend({getScript:function(a,c){return f.get(a,b,c,"script")},getJSON:function(a,b,c){return f.get(a,b,c,"json")},ajaxSetup:function(a,b){b?b$(a,f.ajaxSettings):(b=a,a=f.ajaxSettings),b$(a,b);return a},ajaxSettings:{url:bU,isLocal:bI.test(bV[1]),global:!0,type:"GET",contentType:"application/x-www-form-urlencoded; charset=UTF-8",processData:!0,async:!0,accepts:{xml:"application/xml, text/xml",html:"text/html",text:"text/plain",json:"application/json, text/javascript","*":bW},contents:{xml:/xml/,html:/html/,json:/json/},responseFields:{xml:"responseXML",text:"responseText"},converters:{"* text":a.String,"text html":!0,"text json":f.parseJSON,"text xml":f.parseXML},flatOptions:{context:!0,url:!0}},ajaxPrefilter:bY(bS),ajaxTransport:bY(bT),ajax:function(a,c){function w(a,c,l,m){if(s!==2){s=2,q&&clearTimeout(q),p=b,n=m||"",v.readyState=a>0?4:0;var o,r,u,w=c,x=l?ca(d,v,l):b,y,z;if(a>=200&&a<300||a===304){if(d.ifModified){if(y=v.getResponseHeader("Last-Modified"))f.lastModified[k]=y;if(z=v.getResponseHeader("Etag"))f.etag[k]=z}if(a===304)w="notmodified",o=!0;else try{r=cb(d,x),w="success",o=!0}catch(A){w="parsererror",u=A}}else{u=w;if(!w||a)w="error",a<0&&(a=0)}v.status=a,v.statusText=""+(c||w),o?h.resolveWith(e,[r,w,v]):h.rejectWith(e,[v,w,u]),v.statusCode(j),j=b,t&&g.trigger("ajax"+(o?"Success":"Error"),[v,d,o?r:u]),i.fireWith(e,[v,w]),t&&(g.trigger("ajaxComplete",[v,d]),--f.active||f.event.trigger("ajaxStop"))}}typeof a=="object"&&(c=a,a=b),c=c||{};var d=f.ajaxSetup({},c),e=d.context||d,g=e!==d&&(e.nodeType||e instanceof f)?f(e):f.event,h=f.Deferred(),i=f.Callbacks("once memory"),j=d.statusCode||{},k,l={},m={},n,o,p,q,r,s=0,t,u,v={readyState:0,setRequestHeader:function(a,b){if(!s){var c=a.toLowerCase();a=m[c]=m[c]||a,l[a]=b}return this},getAllResponseHeaders:function(){return s===2?n:null},getResponseHeader:function(a){var c;if(s===2){if(!o){o={};while(c=bG.exec(n))o[c[1].toLowerCase()]=c[2]}c=o[a.toLowerCase()]}return c===b?null:c},overrideMimeType:function(a){s||(d.mimeType=a);return this},abort:function(a){a=a||"abort",p&&p.abort(a),w(0,a);return this}};h.promise(v),v.success=v.done,v.error=v.fail,v.complete=i.add,v.statusCode=function(a){if(a){var b;if(s<2)for(b in a)j[b]=[j[b],a[b]];else b=a[v.status],v.then(b,b)}return this},d.url=((a||d.url)+"").replace(bF,"").replace(bK,bV[1]+"//"),d.dataTypes=f.trim(d.dataType||"*").toLowerCase().split(bO),d.crossDomain==null&&(r=bQ.exec(d.url.toLowerCase()),d.crossDomain=!(!r||r[1]==bV[1]&&r[2]==bV[2]&&(r[3]||(r[1]==="http:"?80:443))==(bV[3]||(bV[1]==="http:"?80:443)))),d.data&&d.processData&&typeof d.data!="string"&&(d.data=f.param(d.data,d.traditional)),bZ(bS,d,c,v);if(s===2)return!1;t=d.global,d.type=d.type.toUpperCase(),d.hasContent=!bJ.test(d.type),t&&f.active++===0&&f.event.trigger("ajaxStart");if(!d.hasContent){d.data&&(d.url+=(bL.test(d.url)?"&":"?")+d.data,delete d.data),k=d.url;if(d.cache===!1){var x=f.now(),y=d.url.replace(bP,"$1_="+x);d.url=y+(y===d.url?(bL.test(d.url)?"&":"?")+"_="+x:"")}}(d.data&&d.hasContent&&d.contentType!==!1||c.contentType)&&v.setRequestHeader("Content-Type",d.contentType),d.ifModified&&(k=k||d.url,f.lastModified[k]&&v.setRequestHeader("If-Modified-Since",f.lastModified[k]),f.etag[k]&&v.setRequestHeader("If-None-Match",f.etag[k])),v.setRequestHeader("Accept",d.dataTypes[0]&&d.accepts[d.dataTypes[0]]?d.accepts[d.dataTypes[0]]+(d.dataTypes[0]!=="*"?", "+bW+"; q=0.01":""):d.accepts["*"]);for(u in d.headers)v.setRequestHeader(u,d.headers[u]);if(d.beforeSend&&(d.beforeSend.call(e,v,d)===!1||s===2)){v.abort();return!1}for(u in{success:1,error:1,complete:1})v[u](d[u]);p=bZ(bT,d,c,v);if(!p)w(-1,"No Transport");else{v.readyState=1,t&&g.trigger("ajaxSend",[v,d]),d.async&&d.timeout>0&&(q=setTimeout(function(){v.abort("timeout")},d.timeout));try{s=1,p.send(l,w)}catch(z){if(s<2)w(-1,z);else throw z}}return v},param:function(a,c){var d=[],e=function(a,b){b=f.isFunction(b)?b():b,d[d.length]=encodeURIComponent(a)+"="+encodeURIComponent(b)};c===b&&(c=f.ajaxSettings.traditional);if(f.isArray(a)||a.jquery&&!f.isPlainObject(a))f.each(a,function(){e(this.name,this.value)});else for(var g in a)b_(g,a[g],c,e);return d.join("&").replace(bC,"+")}}),f.extend({active:0,lastModified:{},etag:{}});var cc=f.now(),cd=/(\=)\?(&|$)|\?\?/i;f.ajaxSetup({jsonp:"callback",jsonpCallback:function(){return f.expando+"_"+cc++}}),f.ajaxPrefilter("json jsonp",function(b,c,d){var e=typeof b.data=="string"&&/^application\/x\-www\-form\-urlencoded/.test(b.contentType);if(b.dataTypes[0]==="jsonp"||b.jsonp!==!1&&(cd.test(b.url)||e&&cd.test(b.data))){var g,h=b.jsonpCallback=f.isFunction(b.jsonpCallback)?b.jsonpCallback():b.jsonpCallback,i=a[h],j=b.url,k=b.data,l="$1"+h+"$2";b.jsonp!==!1&&(j=j.replace(cd,l),b.url===j&&(e&&(k=k.replace(cd,l)),b.data===k&&(j+=(/\?/.test(j)?"&":"?")+b.jsonp+"="+h))),b.url=j,b.data=k,a[h]=function(a){g=[a]},d.always(function(){a[h]=i,g&&f.isFunction(i)&&a[h](g[0])}),b.converters["script json"]=function(){g||f.error(h+" was not called");return g[0]},b.dataTypes[0]="json";return"script"}}),f.ajaxSetup({accepts:{script:"text/javascript, application/javascript, application/ecmascript, application/x-ecmascript"},contents:{script:/javascript|ecmascript/},converters:{"text script":function(a){f.globalEval(a);return a}}}),f.ajaxPrefilter("script",function(a){a.cache===b&&(a.cache=!1),a.crossDomain&&(a.type="GET",a.global=!1)}),f.ajaxTransport("script",function(a){if(a.crossDomain){var d,e=c.head||c.getElementsByTagName("head")[0]||c.documentElement;return{send:function(f,g){d=c.createElement("script"),d.async="async",a.scriptCharset&&(d.charset=a.scriptCharset),d.src=a.url,d.onload=d.onreadystatechange=function(a,c){if(c||!d.readyState||/loaded|complete/.test(d.readyState))d.onload=d.onreadystatechange=null,e&&d.parentNode&&e.removeChild(d),d=b,c||g(200,"success")},e.insertBefore(d,e.firstChild)},abort:function(){d&&d.onload(0,1)}}}});var ce=a.ActiveXObject?function(){for(var a in cg)cg[a](0,1)}:!1,cf=0,cg;f.ajaxSettings.xhr=a.ActiveXObject?function(){return!this.isLocal&&ch()||ci()}:ch,function(a){f.extend(f.support,{ajax:!!a,cors:!!a&&"withCredentials"in a})}(f.ajaxSettings.xhr()),f.support.ajax&&f.ajaxTransport(function(c){if(!c.crossDomain||f.support.cors){var d;return{send:function(e,g){var h=c.xhr(),i,j;c.username?h.open(c.type,c.url,c.async,c.username,c.password):h.open(c.type,c.url,c.async);if(c.xhrFields)for(j in c.xhrFields)h[j]=c.xhrFields[j];c.mimeType&&h.overrideMimeType&&h.overrideMimeType(c.mimeType),!c.crossDomain&&!e["X-Requested-With"]&&(e["X-Requested-With"]="XMLHttpRequest");try{for(j in e)h.setRequestHeader(j,e[j])}catch(k){}h.send(c.hasContent&&c.data||null),d=function(a,e){var j,k,l,m,n;try{if(d&&(e||h.readyState===4)){d=b,i&&(h.onreadystatechange=f.noop,ce&&delete cg[i]);if(e)h.readyState!==4&&h.abort();else{j=h.status,l=h.getAllResponseHeaders(),m={},n=h.responseXML,n&&n.documentElement&&(m.xml=n);try{m.text=h.responseText}catch(a){}try{k=h.statusText}catch(o){k=""}!j&&c.isLocal&&!c.crossDomain?j=m.text?200:404:j===1223&&(j=204)}}}catch(p){e||g(-1,p)}m&&g(j,k,m,l)},!c.async||h.readyState===4?d():(i=++cf,ce&&(cg||(cg={},f(a).unload(ce)),cg[i]=d),h.onreadystatechange=d)},abort:function(){d&&d(0,1)}}}});var cj={},ck,cl,cm=/^(?:toggle|show|hide)$/,cn=/^([+\-]=)?([\d+.\-]+)([a-z%]*)$/i,co,cp=[["height","marginTop","marginBottom","paddingTop","paddingBottom"],["width","marginLeft","marginRight","paddingLeft","paddingRight"],["opacity"]],cq;f.fn.extend({show:function(a,b,c){var d,e;if(a||a===0)return this.animate(ct("show",3),a,b,c);for(var g=0,h=this.length;g<h;g++)d=this[g],d.style&&(e=d.style.display,!f._data(d,"olddisplay")&&e==="none"&&(e=d.style.display=""),(e===""&&f.css(d,"display")==="none"||!f.contains(d.ownerDocument.documentElement,d))&&f._data(d,"olddisplay",cu(d.nodeName)));for(g=0;g<h;g++){d=this[g];if(d.style){e=d.style.display;if(e===""||e==="none")d.style.display=f._data(d,"olddisplay")||""}}return this},hide:function(a,b,c){if(a||a===0)return this.animate(ct("hide",3),a,b,c);var d,e,g=0,h=this.length;for(;g<h;g++)d=this[g],d.style&&(e=f.css(d,"display"),e!=="none"&&!f._data(d,"olddisplay")&&f._data(d,"olddisplay",e));for(g=0;g<h;g++)this[g].style&&(this[g].style.display="none");return this},_toggle:f.fn.toggle,toggle:function(a,b,c){var d=typeof a=="boolean";f.isFunction(a)&&f.isFunction(b)?this._toggle.apply(this,arguments):a==null||d?this.each(function(){var b=d?a:f(this).is(":hidden");f(this)[b?"show":"hide"]()}):this.animate(ct("toggle",3),a,b,c);return this},fadeTo:function(a,b,c,d){return this.filter(":hidden").css("opacity",0).show().end().animate({opacity:b},a,c,d)},animate:function(a,b,c,d){function g(){e.queue===!1&&f._mark(this);var b=f.extend({},e),c=this.nodeType===1,d=c&&f(this).is(":hidden"),g,h,i,j,k,l,m,n,o,p,q;b.animatedProperties={};for(i in a){g=f.camelCase(i),i!==g&&(a[g]=a[i],delete a[i]);if((k=f.cssHooks[g])&&"expand"in k){l=k.expand(a[g]),delete a[g];for(i in l)i in a||(a[i]=l[i])}}for(g in a){h=a[g],f.isArray(h)?(b.animatedProperties[g]=h[1],h=a[g]=h[0]):b.animatedProperties[g]=b.specialEasing&&b.specialEasing[g]||b.easing||"swing";if(h==="hide"&&d||h==="show"&&!d)return b.complete.call(this);c&&(g==="height"||g==="width")&&(b.overflow=[this.style.overflow,this.style.overflowX,this.style.overflowY],f.css(this,"display")==="inline"&&f.css(this,"float")==="none"&&(!f.support.inlineBlockNeedsLayout||cu(this.nodeName)==="inline"?this.style.display="inline-block":this.style.zoom=1))}b.overflow!=null&&(this.style.overflow="hidden");for(i in a)j=new f.fx(this,b,i),h=a[i],cm.test(h)?(q=f._data(this,"toggle"+i)||(h==="toggle"?d?"show":"hide":0),q?(f._data(this,"toggle"+i,q==="show"?"hide":"show"),j[q]()):j[h]()):(m=cn.exec(h),n=j.cur(),m?(o=parseFloat(m[2]),p=m[3]||(f.cssNumber[i]?"":"px"),p!=="px"&&(f.style(this,i,(o||1)+p),n=(o||1)/j.cur()*n,f.style(this,i,n+p)),m[1]&&(o=(m[1]==="-="?-1:1)*o+n),j.custom(n,o,p)):j.custom(n,h,""));return!0}var e=f.speed(b,c,d);if(f.isEmptyObject(a))return this.each(e.complete,[!1]);a=f.extend({},a);return e.queue===!1?this.each(g):this.queue(e.queue,g)},stop:function(a,c,d){typeof a!="string"&&(d=c,c=a,a=b),c&&a!==!1&&this.queue(a||"fx",[]);return this.each(function(){function h(a,b,c){var e=b[c];f.removeData(a,c,!0),e.stop(d)}var b,c=!1,e=f.timers,g=f._data(this);d||f._unmark(!0,this);if(a==null)for(b in g)g[b]&&g[b].stop&&b.indexOf(".run")===b.length-4&&h(this,g,b);else g[b=a+".run"]&&g[b].stop&&h(this,g,b);for(b=e.length;b--;)e[b].elem===this&&(a==null||e[b].queue===a)&&(d?e[b](!0):e[b].saveState(),c=!0,e.splice(b,1));(!d||!c)&&f.dequeue(this,a)})}}),f.each({slideDown:ct("show",1),slideUp:ct("hide",1),slideToggle:ct("toggle",1),fadeIn:{opacity:"show"},fadeOut:{opacity:"hide"},fadeToggle:{opacity:"toggle"}},function(a,b){f.fn[a]=function(a,c,d){return this.animate(b,a,c,d)}}),f.extend({speed:function(a,b,c){var d=a&&typeof a=="object"?f.extend({},a):{complete:c||!c&&b||f.isFunction(a)&&a,duration:a,easing:c&&b||b&&!f.isFunction(b)&&b};d.duration=f.fx.off?0:typeof d.duration=="number"?d.duration:d.duration in f.fx.speeds?f.fx.speeds[d.duration]:f.fx.speeds._default;if(d.queue==null||d.queue===!0)d.queue="fx";d.old=d.complete,d.complete=function(a){f.isFunction(d.old)&&d.old.call(this),d.queue?f.dequeue(this,d.queue):a!==!1&&f._unmark(this)};return d},easing:{linear:function(a){return a},swing:function(a){return-Math.cos(a*Math.PI)/2+.5}},timers:[],fx:function(a,b,c){this.options=b,this.elem=a,this.prop=c,b.orig=b.orig||{}}}),f.fx.prototype={update:function(){this.options.step&&this.options.step.call(this.elem,this.now,this),(f.fx.step[this.prop]||f.fx.step._default)(this)},cur:function(){if(this.elem[this.prop]!=null&&(!this.elem.style||this.elem.style[this.prop]==null))return this.elem[this.prop];var a,b=f.css(this.elem,this.prop);return isNaN(a=parseFloat(b))?!b||b==="auto"?0:b:a},custom:function(a,c,d){function h(a){return e.step(a)}var e=this,g=f.fx;this.startTime=cq||cr(),this.end=c,this.now=this.start=a,this.pos=this.state=0,this.unit=d||this.unit||(f.cssNumber[this.prop]?"":"px"),h.queue=this.options.queue,h.elem=this.elem,h.saveState=function(){f._data(e.elem,"fxshow"+e.prop)===b&&(e.options.hide?f._data(e.elem,"fxshow"+e.prop,e.start):e.options.show&&f._data(e.elem,"fxshow"+e.prop,e.end))},h()&&f.timers.push(h)&&!co&&(co=setInterval(g.tick,g.interval))},show:function(){var a=f._data(this.elem,"fxshow"+this.prop);this.options.orig[this.prop]=a||f.style(this.elem,this.prop),this.options.show=!0,a!==b?this.custom(this.cur(),a):this.custom(this.prop==="width"||this.prop==="height"?1:0,this.cur()),f(this.elem).show()},hide:function(){this.options.orig[this.prop]=f._data(this.elem,"fxshow"+this.prop)||f.style(this.elem,this.prop),this.options.hide=!0,this.custom(this.cur(),0)},step:function(a){var b,c,d,e=cq||cr(),g=!0,h=this.elem,i=this.options;if(a||e>=i.duration+this.startTime){this.now=this.end,this.pos=this.state=1,this.update(),i.animatedProperties[this.prop]=!0;for(b in i.animatedProperties)i.animatedProperties[b]!==!0&&(g=!1);if(g){i.overflow!=null&&!f.support.shrinkWrapBlocks&&f.each(["","X","Y"],function(a,b){h.style["overflow"+b]=i.overflow[a]}),i.hide&&f(h).hide();if(i.hide||i.show)for(b in i.animatedProperties)f.style(h,b,i.orig[b]),f.removeData(h,"fxshow"+b,!0),f.removeData(h,"toggle"+b,!0);d=i.complete,d&&(i.complete=!1,d.call(h))}return!1}i.duration==Infinity?this.now=e:(c=e-this.startTime,this.state=c/i.duration,this.pos=f.easing[i.animatedProperties[this.prop]](this.state,c,0,1,i.duration),this.now=this.start+(this.end-this.start)*this.pos),this.update();return!0}},f.extend(f.fx,{tick:function(){var a,b=f.timers,c=0;for(;c<b.length;c++)a=b[c],!a()&&b[c]===a&&b.splice(c--,1);b.length||f.fx.stop()},interval:13,stop:function(){clearInterval(co),co=null},speeds:{slow:600,fast:200,_default:400},step:{opacity:function(a){f.style(a.elem,"opacity",a.now)},_default:function(a){a.elem.style&&a.elem.style[a.prop]!=null?a.elem.style[a.prop]=a.now+a.unit:a.elem[a.prop]=a.now}}}),f.each(cp.concat.apply([],cp),function(a,b){b.indexOf("margin")&&(f.fx.step[b]=function(a){f.style(a.elem,b,Math.max(0,a.now)+a.unit)})}),f.expr&&f.expr.filters&&(f.expr.filters.animated=function(a){return f.grep(f.timers,function(b){return a===b.elem}).length});var cv,cw=/^t(?:able|d|h)$/i,cx=/^(?:body|html)$/i;"getBoundingClientRect"in c.documentElement?cv=function(a,b,c,d){try{d=a.getBoundingClientRect()}catch(e){}if(!d||!f.contains(c,a))return d?{top:d.top,left:d.left}:{top:0,left:0};var g=b.body,h=cy(b),i=c.clientTop||g.clientTop||0,j=c.clientLeft||g.clientLeft||0,k=h.pageYOffset||f.support.boxModel&&c.scrollTop||g.scrollTop,l=h.pageXOffset||f.support.boxModel&&c.scrollLeft||g.scrollLeft,m=d.top+k-i,n=d.left+l-j;return{top:m,left:n}}:cv=function(a,b,c){var d,e=a.offsetParent,g=a,h=b.body,i=b.defaultView,j=i?i.getComputedStyle(a,null):a.currentStyle,k=a.offsetTop,l=a.offsetLeft;while((a=a.parentNode)&&a!==h&&a!==c){if(f.support.fixedPosition&&j.position==="fixed")break;d=i?i.getComputedStyle(a,null):a.currentStyle,k-=a.scrollTop,l-=a.scrollLeft,a===e&&(k+=a.offsetTop,l+=a.offsetLeft,f.support.doesNotAddBorder&&(!f.support.doesAddBorderForTableAndCells||!cw.test(a.nodeName))&&(k+=parseFloat(d.borderTopWidth)||0,l+=parseFloat(d.borderLeftWidth)||0),g=e,e=a.offsetParent),f.support.subtractsBorderForOverflowNotVisible&&d.overflow!=="visible"&&(k+=parseFloat(d.borderTopWidth)||0,l+=parseFloat(d.borderLeftWidth)||0),j=d}if(j.position==="relative"||j.position==="static")k+=h.offsetTop,l+=h.offsetLeft;f.support.fixedPosition&&j.position==="fixed"&&(k+=Math.max(c.scrollTop,h.scrollTop),l+=Math.max(c.scrollLeft,h.scrollLeft));return{top:k,left:l}},f.fn.offset=function(a){if(arguments.length)return a===b?this:this.each(function(b){f.offset.setOffset(this,a,b)});var c=this[0],d=c&&c.ownerDocument;if(!d)return null;if(c===d.body)return f.offset.bodyOffset(c);return cv(c,d,d.documentElement)},f.offset={bodyOffset:function(a){var b=a.offsetTop,c=a.offsetLeft;f.support.doesNotIncludeMarginInBodyOffset&&(b+=parseFloat(f.css(a,"marginTop"))||0,c+=parseFloat(f.css(a,"marginLeft"))||0);return{top:b,left:c}},setOffset:function(a,b,c){var d=f.css(a,"position");d==="static"&&(a.style.position="relative");var e=f(a),g=e.offset(),h=f.css(a,"top"),i=f.css(a,"left"),j=(d==="absolute"||d==="fixed")&&f.inArray("auto",[h,i])>-1,k={},l={},m,n;j?(l=e.position(),m=l.top,n=l.left):(m=parseFloat(h)||0,n=parseFloat(i)||0),f.isFunction(b)&&(b=b.call(a,c,g)),b.top!=null&&(k.top=b.top-g.top+m),b.left!=null&&(k.left=b.left-g.left+n),"using"in b?b.using.call(a,k):e.css(k)}},f.fn.extend({position:function(){if(!this[0])return null;var a=this[0],b=this.offsetParent(),c=this.offset(),d=cx.test(b[0].nodeName)?{top:0,left:0}:b.offset();c.top-=parseFloat(f.css(a,"marginTop"))||0,c.left-=parseFloat(f.css(a,"marginLeft"))||0,d.top+=parseFloat(f.css(b[0],"borderTopWidth"))||0,d.left+=parseFloat(f.css(b[0],"borderLeftWidth"))||0;return{top:c.top-d.top,left:c.left-d.left}},offsetParent:function(){return this.map(function(){var a=this.offsetParent||c.body;while(a&&!cx.test(a.nodeName)&&f.css(a,"position")==="static")a=a.offsetParent;return a})}}),f.each({scrollLeft:"pageXOffset",scrollTop:"pageYOffset"},function(a,c){var d=/Y/.test(c);f.fn[a]=function(e){return f.access(this,function(a,e,g){var h=cy(a);if(g===b)return h?c in h?h[c]:f.support.boxModel&&h.document.documentElement[e]||h.document.body[e]:a[e];h?h.scrollTo(d?f(h).scrollLeft():g,d?g:f(h).scrollTop()):a[e]=g},a,e,arguments.length,null)}}),f.each({Height:"height",Width:"width"},function(a,c){var d="client"+a,e="scroll"+a,g="offset"+a;f.fn["inner"+a]=function(){var a=this[0];return a?a.style?parseFloat(f.css(a,c,"padding")):this[c]():null},f.fn["outer"+a]=function(a){var b=this[0];return b?b.style?parseFloat(f.css(b,c,a?"margin":"border")):this[c]():null},f.fn[c]=function(a){return f.access(this,function(a,c,h){var i,j,k,l;if(f.isWindow(a)){i=a.document,j=i.documentElement[d];return f.support.boxModel&&j||i.body&&i.body[d]||j}if(a.nodeType===9){i=a.documentElement;if(i[d]>=i[e])return i[d];return Math.max(a.body[e],i[e],a.body[g],i[g])}if(h===b){k=f.css(a,c),l=parseFloat(k);return f.isNumeric(l)?l:k}f(a).css(c,h)},c,a,arguments.length,null)}}),a.jQuery=a.$=f,typeof define=="function"&&define.amd&&define.amd.jQuery&&define("jquery",[],function(){return f})})(window);
//-- 都昌软件的处理HTML DOM结构的工具类
// 袁永福到此一游


// 注意：每次修改文件上传时都得写上时间 
 
/*
* Fingerprintjs2 2.1.0 - Modern & flexible browser fingerprint library v2
* https://github.com/Valve/fingerprintjs2
* Copyright (c) 2015 Valentin Vasilyev (valentin.vasilyev@outlook.com)
* Licensed under the MIT (http://www.opensource.org/licenses/mit-license.php) license.
*
* THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
* AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
* IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
* ARE DISCLAIMED. IN NO EVENT SHALL VALENTIN VASILYEV BE LIABLE FOR ANY
* DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
* (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
* LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
* ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
* (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
* THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
/* global define */
(function (name, context, definition) {
    'use strict'
    if (context != window) {
        context = window;
    }
    context[name] = definition();
    // if (typeof window !== 'undefined' && typeof define === 'function' && define.amd) { define(definition) } else if (typeof module !== 'undefined' && module.exports) { module.exports = definition() } else if (context.exports) { context.exports = definition() } else { context[name] = definition() }
})('Fingerprint2', this, function () {
    'use strict'

    /// MurmurHash3 related functions

    //
    // Given two 64bit ints (as an array of two 32bit ints) returns the two
    // added together as a 64bit int (as an array of two 32bit ints).
    //
    var x64Add = function (m, n) {
        m = [m[0] >>> 16, m[0] & 0xffff, m[1] >>> 16, m[1] & 0xffff]
        n = [n[0] >>> 16, n[0] & 0xffff, n[1] >>> 16, n[1] & 0xffff]
        var o = [0, 0, 0, 0]
        o[3] += m[3] + n[3]
        o[2] += o[3] >>> 16
        o[3] &= 0xffff
        o[2] += m[2] + n[2]
        o[1] += o[2] >>> 16
        o[2] &= 0xffff
        o[1] += m[1] + n[1]
        o[0] += o[1] >>> 16
        o[1] &= 0xffff
        o[0] += m[0] + n[0]
        o[0] &= 0xffff
        return [(o[0] << 16) | o[1], (o[2] << 16) | o[3]]
    }

    //
    // Given two 64bit ints (as an array of two 32bit ints) returns the two
    // multiplied together as a 64bit int (as an array of two 32bit ints).
    //
    var x64Multiply = function (m, n) {
        m = [m[0] >>> 16, m[0] & 0xffff, m[1] >>> 16, m[1] & 0xffff]
        n = [n[0] >>> 16, n[0] & 0xffff, n[1] >>> 16, n[1] & 0xffff]
        var o = [0, 0, 0, 0]
        o[3] += m[3] * n[3]
        o[2] += o[3] >>> 16
        o[3] &= 0xffff
        o[2] += m[2] * n[3]
        o[1] += o[2] >>> 16
        o[2] &= 0xffff
        o[2] += m[3] * n[2]
        o[1] += o[2] >>> 16
        o[2] &= 0xffff
        o[1] += m[1] * n[3]
        o[0] += o[1] >>> 16
        o[1] &= 0xffff
        o[1] += m[2] * n[2]
        o[0] += o[1] >>> 16
        o[1] &= 0xffff
        o[1] += m[3] * n[1]
        o[0] += o[1] >>> 16
        o[1] &= 0xffff
        o[0] += (m[0] * n[3]) + (m[1] * n[2]) + (m[2] * n[1]) + (m[3] * n[0])
        o[0] &= 0xffff
        return [(o[0] << 16) | o[1], (o[2] << 16) | o[3]]
    }
    //
    // Given a 64bit int (as an array of two 32bit ints) and an int
    // representing a number of bit positions, returns the 64bit int (as an
    // array of two 32bit ints) rotated left by that number of positions.
    //
    var x64Rotl = function (m, n) {
        n %= 64
        if (n === 32) {
            return [m[1], m[0]]
        } else if (n < 32) {
            return [(m[0] << n) | (m[1] >>> (32 - n)), (m[1] << n) | (m[0] >>> (32 - n))]
        } else {
            n -= 32
            return [(m[1] << n) | (m[0] >>> (32 - n)), (m[0] << n) | (m[1] >>> (32 - n))]
        }
    }
    //
    // Given a 64bit int (as an array of two 32bit ints) and an int
    // representing a number of bit positions, returns the 64bit int (as an
    // array of two 32bit ints) shifted left by that number of positions.
    //
    var x64LeftShift = function (m, n) {
        n %= 64
        if (n === 0) {
            return m
        } else if (n < 32) {
            return [(m[0] << n) | (m[1] >>> (32 - n)), m[1] << n]
        } else {
            return [m[1] << (n - 32), 0]
        }
    }
    //
    // Given two 64bit ints (as an array of two 32bit ints) returns the two
    // xored together as a 64bit int (as an array of two 32bit ints).
    //
    var x64Xor = function (m, n) {
        return [m[0] ^ n[0], m[1] ^ n[1]]
    }
    //
    // Given a block, returns murmurHash3's final x64 mix of that block.
    // (`[0, h[0] >>> 1]` is a 33 bit unsigned right shift. This is the
    // only place where we need to right shift 64bit ints.)
    //
    var x64Fmix = function (h) {
        h = x64Xor(h, [0, h[0] >>> 1])
        h = x64Multiply(h, [0xff51afd7, 0xed558ccd])
        h = x64Xor(h, [0, h[0] >>> 1])
        h = x64Multiply(h, [0xc4ceb9fe, 0x1a85ec53])
        h = x64Xor(h, [0, h[0] >>> 1])
        return h
    }

    //
    // Given a string and an optional seed as an int, returns a 128 bit
    // hash using the x64 flavor of MurmurHash3, as an unsigned hex.
    //
    var x64hash128 = function (key, seed) {
        key = key || ''
        seed = seed || 0
        var remainder = key.length % 16
        var bytes = key.length - remainder
        var h1 = [0, seed]
        var h2 = [0, seed]
        var k1 = [0, 0]
        var k2 = [0, 0]
        var c1 = [0x87c37b91, 0x114253d5]
        var c2 = [0x4cf5ad43, 0x2745937f]
        for (var i = 0; i < bytes; i = i + 16) {
            k1 = [((key.charCodeAt(i + 4) & 0xff)) | ((key.charCodeAt(i + 5) & 0xff) << 8) | ((key.charCodeAt(i + 6) & 0xff) << 16) | ((key.charCodeAt(i + 7) & 0xff) << 24), ((key.charCodeAt(i) & 0xff)) | ((key.charCodeAt(i + 1) & 0xff) << 8) | ((key.charCodeAt(i + 2) & 0xff) << 16) | ((key.charCodeAt(i + 3) & 0xff) << 24)]
            k2 = [((key.charCodeAt(i + 12) & 0xff)) | ((key.charCodeAt(i + 13) & 0xff) << 8) | ((key.charCodeAt(i + 14) & 0xff) << 16) | ((key.charCodeAt(i + 15) & 0xff) << 24), ((key.charCodeAt(i + 8) & 0xff)) | ((key.charCodeAt(i + 9) & 0xff) << 8) | ((key.charCodeAt(i + 10) & 0xff) << 16) | ((key.charCodeAt(i + 11) & 0xff) << 24)]
            k1 = x64Multiply(k1, c1)
            k1 = x64Rotl(k1, 31)
            k1 = x64Multiply(k1, c2)
            h1 = x64Xor(h1, k1)
            h1 = x64Rotl(h1, 27)
            h1 = x64Add(h1, h2)
            h1 = x64Add(x64Multiply(h1, [0, 5]), [0, 0x52dce729])
            k2 = x64Multiply(k2, c2)
            k2 = x64Rotl(k2, 33)
            k2 = x64Multiply(k2, c1)
            h2 = x64Xor(h2, k2)
            h2 = x64Rotl(h2, 31)
            h2 = x64Add(h2, h1)
            h2 = x64Add(x64Multiply(h2, [0, 5]), [0, 0x38495ab5])
        }
        k1 = [0, 0]
        k2 = [0, 0]
        switch (remainder) {
            case 15:
                k2 = x64Xor(k2, x64LeftShift([0, key.charCodeAt(i + 14)], 48))
            // fallthrough
            case 14:
                k2 = x64Xor(k2, x64LeftShift([0, key.charCodeAt(i + 13)], 40))
            // fallthrough
            case 13:
                k2 = x64Xor(k2, x64LeftShift([0, key.charCodeAt(i + 12)], 32))
            // fallthrough
            case 12:
                k2 = x64Xor(k2, x64LeftShift([0, key.charCodeAt(i + 11)], 24))
            // fallthrough
            case 11:
                k2 = x64Xor(k2, x64LeftShift([0, key.charCodeAt(i + 10)], 16))
            // fallthrough
            case 10:
                k2 = x64Xor(k2, x64LeftShift([0, key.charCodeAt(i + 9)], 8))
            // fallthrough
            case 9:
                k2 = x64Xor(k2, [0, key.charCodeAt(i + 8)])
                k2 = x64Multiply(k2, c2)
                k2 = x64Rotl(k2, 33)
                k2 = x64Multiply(k2, c1)
                h2 = x64Xor(h2, k2)
            // fallthrough
            case 8:
                k1 = x64Xor(k1, x64LeftShift([0, key.charCodeAt(i + 7)], 56))
            // fallthrough
            case 7:
                k1 = x64Xor(k1, x64LeftShift([0, key.charCodeAt(i + 6)], 48))
            // fallthrough
            case 6:
                k1 = x64Xor(k1, x64LeftShift([0, key.charCodeAt(i + 5)], 40))
            // fallthrough
            case 5:
                k1 = x64Xor(k1, x64LeftShift([0, key.charCodeAt(i + 4)], 32))
            // fallthrough
            case 4:
                k1 = x64Xor(k1, x64LeftShift([0, key.charCodeAt(i + 3)], 24))
            // fallthrough
            case 3:
                k1 = x64Xor(k1, x64LeftShift([0, key.charCodeAt(i + 2)], 16))
            // fallthrough
            case 2:
                k1 = x64Xor(k1, x64LeftShift([0, key.charCodeAt(i + 1)], 8))
            // fallthrough
            case 1:
                k1 = x64Xor(k1, [0, key.charCodeAt(i)])
                k1 = x64Multiply(k1, c1)
                k1 = x64Rotl(k1, 31)
                k1 = x64Multiply(k1, c2)
                h1 = x64Xor(h1, k1)
            // fallthrough
        }
        h1 = x64Xor(h1, [0, key.length])
        h2 = x64Xor(h2, [0, key.length])
        h1 = x64Add(h1, h2)
        h2 = x64Add(h2, h1)
        h1 = x64Fmix(h1)
        h2 = x64Fmix(h2)
        h1 = x64Add(h1, h2)
        h2 = x64Add(h2, h1)
        return ('00000000' + (h1[0] >>> 0).toString(16)).slice(-8) + ('00000000' + (h1[1] >>> 0).toString(16)).slice(-8) + ('00000000' + (h2[0] >>> 0).toString(16)).slice(-8) + ('00000000' + (h2[1] >>> 0).toString(16)).slice(-8)
    }

    var defaultOptions = {
        preprocessor: null,
        audio: {
            timeout: 1000,
            // On iOS 11, audio context can only be used in response to user interaction.
            // We require users to explicitly enable audio fingerprinting on iOS 11.
            // See https://stackoverflow.com/questions/46363048/onaudioprocess-not-called-on-ios11#46534088
            excludeIOS11: true
        },
        fonts: {
            swfContainerId: 'fingerprintjs2',
            swfPath: 'flash/compiled/FontList.swf',
            userDefinedFonts: [],
            extendedJsFonts: false
        },
        screen: {
            // To ensure consistent fingerprints when users rotate their mobile devices
            detectScreenOrientation: true
        },
        plugins: {
            sortPluginsFor: [/palemoon/i],
            excludeIE: false
        },
        extraComponents: [],
        excludes: {
            // Unreliable on Windows, see https://github.com/Valve/fingerprintjs2/issues/375
            'enumerateDevices': true,
            // devicePixelRatio depends on browser zoom, and it's impossible to detect browser zoom
            'pixelRatio': true,
            // DNT depends on incognito mode for some browsers (Chrome) and it's impossible to detect incognito mode
            'doNotTrack': true,
            // uses js fonts already
            'fontsFlash': true
        },
        NOT_AVAILABLE: 'not available',
        ERROR: 'error',
        EXCLUDED: 'excluded'
    }

    var each = function (obj, iterator) {
        if (Array.prototype.forEach && obj.forEach === Array.prototype.forEach) {
            obj.forEach(iterator)
        } else if (obj.length === +obj.length) {
            for (var i = 0, l = obj.length; i < l; i++) {
                iterator(obj[i], i, obj)
            }
        } else {
            for (var key in obj) {
                if (obj.hasOwnProperty(key)) {
                    iterator(obj[key], key, obj)
                }
            }
        }
    }

    var map = function (obj, iterator) {
        var results = []
        // Not using strict equality so that this acts as a
        // shortcut to checking for `null` and `undefined`.
        if (obj == null) {
            return results
        }
        if (Array.prototype.map && obj.map === Array.prototype.map) { return obj.map(iterator) }
        each(obj, function (value, index, list) {
            results.push(iterator(value, index, list))
        })
        return results
    }

    var extendSoft = function (target, source) {
        if (source == null) { return target }
        var value
        var key
        for (key in source) {
            value = source[key]
            if (value != null && !(Object.prototype.hasOwnProperty.call(target, key))) {
                target[key] = value
            }
        }
        return target
    }

    // https://developer.mozilla.org/en-US/docs/Web/API/MediaDevices/enumerateDevices
    var enumerateDevicesKey = function (done, options) {
        if (!isEnumerateDevicesSupported()) {
            return done(options.NOT_AVAILABLE)
        }
        navigator.mediaDevices.enumerateDevices().then(function (devices) {
            done(devices.map(function (device) {
                return 'id=' + device.deviceId + ';gid=' + device.groupId + ';' + device.kind + ';' + device.label
            }))
        })
            .catch(function (error) {
                done(error)
            })
    }

    var isEnumerateDevicesSupported = function () {
        return (navigator.mediaDevices && navigator.mediaDevices.enumerateDevices)
    }
    // Inspired by and based on https://github.com/cozylife/audio-fingerprint
    var audioKey = function (done, options) {
        var audioOptions = options.audio
        if (audioOptions.excludeIOS11 && navigator.userAgent.match(/OS 11.+Version\/11.+Safari/)) {
            // See comment for excludeUserAgent and https://stackoverflow.com/questions/46363048/onaudioprocess-not-called-on-ios11#46534088
            return done(options.EXCLUDED)
        }

        var AudioContext = window.OfflineAudioContext || window.webkitOfflineAudioContext

        if (AudioContext == null) {
            return done(options.NOT_AVAILABLE)
        }

        var context = new AudioContext(1, 44100, 44100)

        var oscillator = context.createOscillator()
        oscillator.type = 'triangle'
        oscillator.frequency.setValueAtTime(10000, context.currentTime)

        var compressor = context.createDynamicsCompressor()
        each([
            ['threshold', -50],
            ['knee', 40],
            ['ratio', 12],
            ['reduction', -20],
            ['attack', 0],
            ['release', 0.25]
        ], function (item) {
            if (compressor[item[0]] !== undefined && typeof compressor[item[0]].setValueAtTime === 'function') {
                compressor[item[0]].setValueAtTime(item[1], context.currentTime)
            }
        })

        oscillator.connect(compressor)
        compressor.connect(context.destination)
        oscillator.start(0)
        context.startRendering()

        var audioTimeoutId = setTimeout(function () {
            console.warn('Audio fingerprint timed out. Please report bug at https://github.com/Valve/fingerprintjs2 with your user agent: "' + navigator.userAgent + '".')
            context.oncomplete = function () { }
            context = null
            return done('audioTimeout')
        }, audioOptions.timeout)

        context.oncomplete = function (event) {
            var fingerprint
            try {
                clearTimeout(audioTimeoutId)
                fingerprint = event.renderedBuffer.getChannelData(0)
                    .slice(4500, 5000)
                    .reduce(function (acc, val) { return acc + Math.abs(val) }, 0)
                    .toString()
                oscillator.disconnect()
                compressor.disconnect()
            } catch (error) {
                done(error)
                return
            }
            done(fingerprint)
        }
    }
    var UserAgent = function (done) {
        done(navigator.userAgent)
    }
    var webdriver = function (done, options) {
        done(navigator.webdriver == null ? options.NOT_AVAILABLE : navigator.webdriver)
    }
    var languageKey = function (done, options) {
        done(navigator.language || navigator.userLanguage || navigator.browserLanguage || navigator.systemLanguage || options.NOT_AVAILABLE)
    }
    var colorDepthKey = function (done, options) {
        done(window.screen.colorDepth || options.NOT_AVAILABLE)
    }
    var deviceMemoryKey = function (done, options) {
        done(navigator.deviceMemory || options.NOT_AVAILABLE)
    }
    var pixelRatioKey = function (done, options) {
        done(window.devicePixelRatio || options.NOT_AVAILABLE)
    }
    var screenResolutionKey = function (done, options) {
        done(getScreenResolution(options))
    }
    var getScreenResolution = function (options) {
        var resolution = [window.screen.width, window.screen.height]
        if (options.screen.detectScreenOrientation) {
            resolution.sort().reverse()
        }
        return resolution
    }
    var availableScreenResolutionKey = function (done, options) {
        done(getAvailableScreenResolution(options))
    }
    var getAvailableScreenResolution = function (options) {
        if (window.screen.availWidth && window.screen.availHeight) {
            var available = [window.screen.availHeight, window.screen.availWidth]
            if (options.screen.detectScreenOrientation) {
                available.sort().reverse()
            }
            return available
        }
        // headless browsers
        return options.NOT_AVAILABLE
    }
    var timezoneOffset = function (done) {
        done(new Date().getTimezoneOffset())
    }
    var timezone = function (done, options) {
        if (window.Intl && window.Intl.DateTimeFormat) {
            done(new window.Intl.DateTimeFormat().resolvedOptions().timeZone)
            return
        }
        done(options.NOT_AVAILABLE)
    }
    var sessionStorageKey = function (done, options) {
        done(hasSessionStorage(options))
    }
    var localStorageKey = function (done, options) {
        done(hasLocalStorage(options))
    }
    var indexedDbKey = function (done, options) {
        done(hasIndexedDB(options))
    }
    var addBehaviorKey = function (done) {
        // body might not be defined at this point or removed programmatically
        done(!!(document.body && document.body.addBehavior))
    }
    var openDatabaseKey = function (done) {
        done(!!window.openDatabase)
    }
    var cpuClassKey = function (done, options) {
        done(getNavigatorCpuClass(options))
    }
    var platformKey = function (done, options) {
        done(getNavigatorPlatform(options))
    }
    var doNotTrackKey = function (done, options) {
        done(getDoNotTrack(options))
    }
    var canvasKey = function (done, options) {
        if (isCanvasSupported()) {
            done(getCanvasFp(options))
            return
        }
        done(options.NOT_AVAILABLE)
    }
    var webglKey = function (done, options) {
        if (isWebGlSupported()) {
            done(getWebglFp())
            return
        }
        done(options.NOT_AVAILABLE)
    }
    var webglVendorAndRendererKey = function (done) {
        if (isWebGlSupported()) {
            done(getWebglVendorAndRenderer())
            return
        }
        done()
    }
    var adBlockKey = function (done) {
        done(getAdBlock())
    }
    var hasLiedLanguagesKey = function (done) {
        done(getHasLiedLanguages())
    }
    var hasLiedResolutionKey = function (done) {
        done(getHasLiedResolution())
    }
    var hasLiedOsKey = function (done) {
        done(getHasLiedOs())
    }
    var hasLiedBrowserKey = function (done) {
        done(getHasLiedBrowser())
    }
    // flash fonts (will increase fingerprinting time 20X to ~ 130-150ms)
    var flashFontsKey = function (done, options) {
        // we do flash if swfobject is loaded
        if (!hasSwfObjectLoaded()) {
            return done('swf object not loaded')
        }
        if (!hasMinFlashInstalled()) {
            return done('flash not installed')
        }
        if (!options.fonts.swfPath) {
            return done('missing options.fonts.swfPath')
        }
        loadSwfAndDetectFonts(function (fonts) {
            done(fonts)
        }, options)
    }
    // kudos to http://www.lalit.org/lab/javascript-css-font-detect/
    var jsFontsKey = function (done, options) {
        // a font will be compared against all the three default fonts.
        // and if it doesn't match all 3 then that font is not available.
        var baseFonts = ['monospace', 'sans-serif', 'serif']

        var fontList = [
            'Andale Mono', 'Arial', 'Arial Black', 'Arial Hebrew', 'Arial MT', 'Arial Narrow', 'Arial Rounded MT Bold', 'Arial Unicode MS',
            'Bitstream Vera Sans Mono', 'Book Antiqua', 'Bookman Old Style',
            'Calibri', 'Cambria', 'Cambria Math', 'Century', 'Century Gothic', 'Century Schoolbook', 'Comic Sans', 'Comic Sans MS', 'Consolas', 'Courier', 'Courier New',
            'Geneva', 'Georgia',
            'Helvetica', 'Helvetica Neue',
            'Impact',
            'Lucida Bright', 'Lucida Calligraphy', 'Lucida Console', 'Lucida Fax', 'LUCIDA GRANDE', 'Lucida Handwriting', 'Lucida Sans', 'Lucida Sans Typewriter', 'Lucida Sans Unicode',
            'Microsoft Sans Serif', 'Monaco', 'Monotype Corsiva', 'MS Gothic', 'MS Outlook', 'MS PGothic', 'MS Reference Sans Serif', 'MS Sans Serif', 'MS Serif', 'MYRIAD', 'MYRIAD PRO',
            'Palatino', 'Palatino Linotype',
            'Segoe Print', 'Segoe Script', 'Segoe UI', 'Segoe UI Light', 'Segoe UI Semibold', 'Segoe UI Symbol',
            'Tahoma', 'Times', 'Times New Roman', 'Times New Roman PS', 'Trebuchet MS',
            'Verdana', 'Wingdings', 'Wingdings 2', 'Wingdings 3'
        ]

        if (options.fonts.extendedJsFonts) {
            var extendedFontList = [
                'Abadi MT Condensed Light', 'Academy Engraved LET', 'ADOBE CASLON PRO', 'Adobe Garamond', 'ADOBE GARAMOND PRO', 'Agency FB', 'Aharoni', 'Albertus Extra Bold', 'Albertus Medium', 'Algerian', 'Amazone BT', 'American Typewriter',
                'American Typewriter Condensed', 'AmerType Md BT', 'Andalus', 'Angsana New', 'AngsanaUPC', 'Antique Olive', 'Aparajita', 'Apple Chancery', 'Apple Color Emoji', 'Apple SD Gothic Neo', 'Arabic Typesetting', 'ARCHER',
                'ARNO PRO', 'Arrus BT', 'Aurora Cn BT', 'AvantGarde Bk BT', 'AvantGarde Md BT', 'AVENIR', 'Ayuthaya', 'Bandy', 'Bangla Sangam MN', 'Bank Gothic', 'BankGothic Md BT', 'Baskerville',
                'Baskerville Old Face', 'Batang', 'BatangChe', 'Bauer Bodoni', 'Bauhaus 93', 'Bazooka', 'Bell MT', 'Bembo', 'Benguiat Bk BT', 'Berlin Sans FB', 'Berlin Sans FB Demi', 'Bernard MT Condensed', 'BernhardFashion BT', 'BernhardMod BT', 'Big Caslon', 'BinnerD',
                'Blackadder ITC', 'BlairMdITC TT', 'Bodoni 72', 'Bodoni 72 Oldstyle', 'Bodoni 72 Smallcaps', 'Bodoni MT', 'Bodoni MT Black', 'Bodoni MT Condensed', 'Bodoni MT Poster Compressed',
                'Bookshelf Symbol 7', 'Boulder', 'Bradley Hand', 'Bradley Hand ITC', 'Bremen Bd BT', 'Britannic Bold', 'Broadway', 'Browallia New', 'BrowalliaUPC', 'Brush Script MT', 'Californian FB', 'Calisto MT', 'Calligrapher', 'Candara',
                'CaslonOpnface BT', 'Castellar', 'Centaur', 'Cezanne', 'CG Omega', 'CG Times', 'Chalkboard', 'Chalkboard SE', 'Chalkduster', 'Charlesworth', 'Charter Bd BT', 'Charter BT', 'Chaucer',
                'ChelthmITC Bk BT', 'Chiller', 'Clarendon', 'Clarendon Condensed', 'CloisterBlack BT', 'Cochin', 'Colonna MT', 'Constantia', 'Cooper Black', 'Copperplate', 'Copperplate Gothic', 'Copperplate Gothic Bold',
                'Copperplate Gothic Light', 'CopperplGoth Bd BT', 'Corbel', 'Cordia New', 'CordiaUPC', 'Cornerstone', 'Coronet', 'Cuckoo', 'Curlz MT', 'DaunPenh', 'Dauphin', 'David', 'DB LCD Temp', 'DELICIOUS', 'Denmark',
                'DFKai-SB', 'Didot', 'DilleniaUPC', 'DIN', 'DokChampa', 'Dotum', 'DotumChe', 'Ebrima', 'Edwardian Script ITC', 'Elephant', 'English 111 Vivace BT', 'Engravers MT', 'EngraversGothic BT', 'Eras Bold ITC', 'Eras Demi ITC', 'Eras Light ITC', 'Eras Medium ITC',
                'EucrosiaUPC', 'Euphemia', 'Euphemia UCAS', 'EUROSTILE', 'Exotc350 Bd BT', 'FangSong', 'Felix Titling', 'Fixedsys', 'FONTIN', 'Footlight MT Light', 'Forte',
                'FrankRuehl', 'Fransiscan', 'Freefrm721 Blk BT', 'FreesiaUPC', 'Freestyle Script', 'French Script MT', 'FrnkGothITC Bk BT', 'Fruitger', 'FRUTIGER',
                'Futura', 'Futura Bk BT', 'Futura Lt BT', 'Futura Md BT', 'Futura ZBlk BT', 'FuturaBlack BT', 'Gabriola', 'Galliard BT', 'Gautami', 'Geeza Pro', 'Geometr231 BT', 'Geometr231 Hv BT', 'Geometr231 Lt BT', 'GeoSlab 703 Lt BT',
                'GeoSlab 703 XBd BT', 'Gigi', 'Gill Sans', 'Gill Sans MT', 'Gill Sans MT Condensed', 'Gill Sans MT Ext Condensed Bold', 'Gill Sans Ultra Bold', 'Gill Sans Ultra Bold Condensed', 'Gisha', 'Gloucester MT Extra Condensed', 'GOTHAM', 'GOTHAM BOLD',
                'Goudy Old Style', 'Goudy Stout', 'GoudyHandtooled BT', 'GoudyOLSt BT', 'Gujarati Sangam MN', 'Gulim', 'GulimChe', 'Gungsuh', 'GungsuhChe', 'Gurmukhi MN', 'Haettenschweiler', 'Harlow Solid Italic', 'Harrington', 'Heather', 'Heiti SC', 'Heiti TC', 'HELV',
                'Herald', 'High Tower Text', 'Hiragino Kaku Gothic ProN', 'Hiragino Mincho ProN', 'Hoefler Text', 'Humanst 521 Cn BT', 'Humanst521 BT', 'Humanst521 Lt BT', 'Imprint MT Shadow', 'Incised901 Bd BT', 'Incised901 BT',
                'Incised901 Lt BT', 'INCONSOLATA', 'Informal Roman', 'Informal011 BT', 'INTERSTATE', 'IrisUPC', 'Iskoola Pota', 'JasmineUPC', 'Jazz LET', 'Jenson', 'Jester', 'Jokerman', 'Juice ITC', 'Kabel Bk BT', 'Kabel Ult BT', 'Kailasa', 'KaiTi', 'Kalinga', 'Kannada Sangam MN',
                'Kartika', 'Kaufmann Bd BT', 'Kaufmann BT', 'Khmer UI', 'KodchiangUPC', 'Kokila', 'Korinna BT', 'Kristen ITC', 'Krungthep', 'Kunstler Script', 'Lao UI', 'Latha', 'Leelawadee', 'Letter Gothic', 'Levenim MT', 'LilyUPC', 'Lithograph', 'Lithograph Light', 'Long Island',
                'Lydian BT', 'Magneto', 'Maiandra GD', 'Malayalam Sangam MN', 'Malgun Gothic',
                'Mangal', 'Marigold', 'Marion', 'Marker Felt', 'Market', 'Marlett', 'Matisse ITC', 'Matura MT Script Capitals', 'Meiryo', 'Meiryo UI', 'Microsoft Himalaya', 'Microsoft JhengHei', 'Microsoft New Tai Lue', 'Microsoft PhagsPa', 'Microsoft Tai Le',
                'Microsoft Uighur', 'Microsoft YaHei', 'Microsoft Yi Baiti', 'MingLiU', 'MingLiU_HKSCS', 'MingLiU_HKSCS-ExtB', 'MingLiU-ExtB', 'Minion', 'Minion Pro', 'Miriam', 'Miriam Fixed', 'Mistral', 'Modern', 'Modern No. 20', 'Mona Lisa Solid ITC TT', 'Mongolian Baiti',
                'MONO', 'MoolBoran', 'Mrs Eaves', 'MS LineDraw', 'MS Mincho', 'MS PMincho', 'MS Reference Specialty', 'MS UI Gothic', 'MT Extra', 'MUSEO', 'MV Boli',
                'Nadeem', 'Narkisim', 'NEVIS', 'News Gothic', 'News GothicMT', 'NewsGoth BT', 'Niagara Engraved', 'Niagara Solid', 'Noteworthy', 'NSimSun', 'Nyala', 'OCR A Extended', 'Old Century', 'Old English Text MT', 'Onyx', 'Onyx BT', 'OPTIMA', 'Oriya Sangam MN',
                'OSAKA', 'OzHandicraft BT', 'Palace Script MT', 'Papyrus', 'Parchment', 'Party LET', 'Pegasus', 'Perpetua', 'Perpetua Titling MT', 'PetitaBold', 'Pickwick', 'Plantagenet Cherokee', 'Playbill', 'PMingLiU', 'PMingLiU-ExtB',
                'Poor Richard', 'Poster', 'PosterBodoni BT', 'PRINCETOWN LET', 'Pristina', 'PTBarnum BT', 'Pythagoras', 'Raavi', 'Rage Italic', 'Ravie', 'Ribbon131 Bd BT', 'Rockwell', 'Rockwell Condensed', 'Rockwell Extra Bold', 'Rod', 'Roman', 'Sakkal Majalla',
                'Santa Fe LET', 'Savoye LET', 'Sceptre', 'Script', 'Script MT Bold', 'SCRIPTINA', 'Serifa', 'Serifa BT', 'Serifa Th BT', 'ShelleyVolante BT', 'Sherwood',
                'Shonar Bangla', 'Showcard Gothic', 'Shruti', 'Signboard', 'SILKSCREEN', 'SimHei', 'Simplified Arabic', 'Simplified Arabic Fixed', 'SimSun', 'SimSun-ExtB', 'Sinhala Sangam MN', 'Sketch Rockwell', 'Skia', 'Small Fonts', 'Snap ITC', 'Snell Roundhand', 'Socket',
                'Souvenir Lt BT', 'Staccato222 BT', 'Steamer', 'Stencil', 'Storybook', 'Styllo', 'Subway', 'Swis721 BlkEx BT', 'Swiss911 XCm BT', 'Sylfaen', 'Synchro LET', 'System', 'Tamil Sangam MN', 'Technical', 'Teletype', 'Telugu Sangam MN', 'Tempus Sans ITC',
                'Terminal', 'Thonburi', 'Traditional Arabic', 'Trajan', 'TRAJAN PRO', 'Tristan', 'Tubular', 'Tunga', 'Tw Cen MT', 'Tw Cen MT Condensed', 'Tw Cen MT Condensed Extra Bold',
                'TypoUpright BT', 'Unicorn', 'Univers', 'Univers CE 55 Medium', 'Univers Condensed', 'Utsaah', 'Vagabond', 'Vani', 'Vijaya', 'Viner Hand ITC', 'VisualUI', 'Vivaldi', 'Vladimir Script', 'Vrinda', 'Westminster', 'WHITNEY', 'Wide Latin',
                'ZapfEllipt BT', 'ZapfHumnst BT', 'ZapfHumnst Dm BT', 'Zapfino', 'Zurich BlkEx BT', 'Zurich Ex BT', 'ZWAdobeF']
            fontList = fontList.concat(extendedFontList)
        }

        fontList = fontList.concat(options.fonts.userDefinedFonts)

        // remove duplicate fonts
        fontList = fontList.filter(function (font, position) {
            return fontList.indexOf(font) === position
        })

        // we use m or w because these two characters take up the maximum width.
        // And we use a LLi so that the same matching fonts can get separated
        var testString = 'mmmmmmmmmmlli'

        // we test using 72px font size, we may use any size. I guess larger the better.
        var testSize = '72px'

        var h = document.getElementsByTagName('body')[0]

        // div to load spans for the base fonts
        var baseFontsDiv = document.createElement('div')

        // div to load spans for the fonts to detect
        var fontsDiv = document.createElement('div')

        var defaultWidth = {}
        var defaultHeight = {}

        // creates a span where the fonts will be loaded
        var createSpan = function () {
            var s = document.createElement('span')
            /*
             * We need this css as in some weird browser this
             * span elements shows up for a microSec which creates a
             * bad user experience
             */
            s.style.position = 'absolute'
            s.style.left = '-9999px'
            s.style.fontSize = testSize

            // css font reset to reset external styles
            s.style.fontStyle = 'normal'
            s.style.fontWeight = 'normal'
            s.style.letterSpacing = 'normal'
            s.style.lineBreak = 'auto'
            s.style.lineHeight = 'normal'
            s.style.textTransform = 'none'
            s.style.textAlign = 'left'
            s.style.textDecoration = 'none'
            s.style.textShadow = 'none'
            s.style.whiteSpace = 'normal'
            s.style.wordBreak = 'normal'
            s.style.wordSpacing = 'normal'

            s.innerHTML = testString
            return s
        }

        // creates a span and load the font to detect and a base font for fallback
        var createSpanWithFonts = function (fontToDetect, baseFont) {
            var s = createSpan()
            s.style.fontFamily = "'" + fontToDetect + "'," + baseFont
            return s
        }

        // creates spans for the base fonts and adds them to baseFontsDiv
        var initializeBaseFontsSpans = function () {
            var spans = []
            for (var index = 0, length = baseFonts.length; index < length; index++) {
                var s = createSpan()
                s.style.fontFamily = baseFonts[index]
                baseFontsDiv.appendChild(s)
                spans.push(s)
            }
            return spans
        }

        // creates spans for the fonts to detect and adds them to fontsDiv
        var initializeFontsSpans = function () {
            var spans = {}
            for (var i = 0, l = fontList.length; i < l; i++) {
                var fontSpans = []
                for (var j = 0, numDefaultFonts = baseFonts.length; j < numDefaultFonts; j++) {
                    var s = createSpanWithFonts(fontList[i], baseFonts[j])
                    fontsDiv.appendChild(s)
                    fontSpans.push(s)
                }
                spans[fontList[i]] = fontSpans // Stores {fontName : [spans for that font]}
            }
            return spans
        }

        // checks if a font is available
        var isFontAvailable = function (fontSpans) {
            var detected = false
            for (var i = 0; i < baseFonts.length; i++) {
                detected = (fontSpans[i].offsetWidth !== defaultWidth[baseFonts[i]] || fontSpans[i].offsetHeight !== defaultHeight[baseFonts[i]])
                if (detected) {
                    return detected
                }
            }
            return detected
        }

        // create spans for base fonts
        var baseFontsSpans = initializeBaseFontsSpans()

        // add the spans to the DOM
        h.appendChild(baseFontsDiv)

        // get the default width for the three base fonts
        for (var index = 0, length = baseFonts.length; index < length; index++) {
            defaultWidth[baseFonts[index]] = baseFontsSpans[index].offsetWidth // width for the default font
            defaultHeight[baseFonts[index]] = baseFontsSpans[index].offsetHeight // height for the default font
        }

        // create spans for fonts to detect
        var fontsSpans = initializeFontsSpans()

        // add all the spans to the DOM
        h.appendChild(fontsDiv)

        // check available fonts
        var available = []
        for (var i = 0, l = fontList.length; i < l; i++) {
            if (isFontAvailable(fontsSpans[fontList[i]])) {
                available.push(fontList[i])
            }
        }

        // remove spans from DOM
        h.removeChild(fontsDiv)
        h.removeChild(baseFontsDiv)
        done(available)
    }
    var pluginsComponent = function (done, options) {
        if (isIE()) {
            if (!options.plugins.excludeIE) {
                done(getIEPlugins(options))
            } else {
                done(options.EXCLUDED)
            }
        } else {
            done(getRegularPlugins(options))
        }
    }
    var getRegularPlugins = function (options) {
        if (navigator.plugins == null) {
            return options.NOT_AVAILABLE
        }

        var plugins = []
        // plugins isn't defined in Node envs.
        for (var i = 0, l = navigator.plugins.length; i < l; i++) {
            if (navigator.plugins[i]) { plugins.push(navigator.plugins[i]) }
        }

        // sorting plugins only for those user agents, that we know randomize the plugins
        // every time we try to enumerate them
        if (pluginsShouldBeSorted(options)) {
            plugins = plugins.sort(function (a, b) {
                if (a.name > b.name) { return 1 }
                if (a.name < b.name) { return -1 }
                return 0
            })
        }
        return map(plugins, function (p) {
            var mimeTypes = map(p, function (mt) {
                return [mt.type, mt.suffixes]
            })
            return [p.name, p.description, mimeTypes]
        })
    }
    var getIEPlugins = function (options) {
        var result = []
        if ((Object.getOwnPropertyDescriptor && Object.getOwnPropertyDescriptor(window, 'ActiveXObject')) || ('ActiveXObject' in window)) {
            var names = [
                'AcroPDF.PDF', // Adobe PDF reader 7+
                'Adodb.Stream',
                'AgControl.AgControl', // Silverlight
                'DevalVRXCtrl.DevalVRXCtrl.1',
                'MacromediaFlashPaper.MacromediaFlashPaper',
                'Msxml2.DOMDocument',
                'Msxml2.XMLHTTP',
                'PDF.PdfCtrl', // Adobe PDF reader 6 and earlier, brrr
                'QuickTime.QuickTime', // QuickTime
                'QuickTimeCheckObject.QuickTimeCheck.1',
                'RealPlayer',
                'RealPlayer.RealPlayer(tm) ActiveX Control (32-bit)',
                'RealVideo.RealVideo(tm) ActiveX Control (32-bit)',
                'Scripting.Dictionary',
                'SWCtl.SWCtl', // ShockWave player
                'Shell.UIHelper',
                'ShockwaveFlash.ShockwaveFlash', // flash plugin
                'Skype.Detection',
                'TDCCtl.TDCCtl',
                'WMPlayer.OCX', // Windows media player
                'rmocx.RealPlayer G2 Control',
                'rmocx.RealPlayer G2 Control.1'
            ]
            // starting to detect plugins in IE
            result = map(names, function (name) {
                try {
                    // eslint-disable-next-line no-new
                    new window.ActiveXObject(name)
                    return name
                } catch (e) {
                    return options.ERROR
                }
            })
        } else {
            result.push(options.NOT_AVAILABLE)
        }
        if (navigator.plugins) {
            result = result.concat(getRegularPlugins(options))
        }
        return result
    }
    var pluginsShouldBeSorted = function (options) {
        var should = false
        for (var i = 0, l = options.plugins.sortPluginsFor.length; i < l; i++) {
            var re = options.plugins.sortPluginsFor[i]
            if (navigator.userAgent.match(re)) {
                should = true
                break
            }
        }
        return should
    }
    var touchSupportKey = function (done) {
        done(getTouchSupport())
    }
    var hardwareConcurrencyKey = function (done, options) {
        done(getHardwareConcurrency(options))
    }
    var hasSessionStorage = function (options) {
        try {
            return !!window.sessionStorage
        } catch (e) {
            return options.ERROR // SecurityError when referencing it means it exists
        }
    }

    // https://bugzilla.mozilla.org/show_bug.cgi?id=781447
    var hasLocalStorage = function (options) {
        try {
            return !!window.localStorage
        } catch (e) {
            return options.ERROR // SecurityError when referencing it means it exists
        }
    }
    var hasIndexedDB = function (options) {
        try {
            return !!window.indexedDB
        } catch (e) {
            return options.ERROR // SecurityError when referencing it means it exists
        }
    }
    var getHardwareConcurrency = function (options) {
        if (navigator.hardwareConcurrency) {
            return navigator.hardwareConcurrency
        }
        return options.NOT_AVAILABLE
    }
    var getNavigatorCpuClass = function (options) {
        return navigator.cpuClass || options.NOT_AVAILABLE
    }
    var getNavigatorPlatform = function (options) {
        if (navigator.platform) {
            return navigator.platform
        } else {
            return options.NOT_AVAILABLE
        }
    }
    var getDoNotTrack = function (options) {
        if (navigator.doNotTrack) {
            return navigator.doNotTrack
        } else if (navigator.msDoNotTrack) {
            return navigator.msDoNotTrack
        } else if (window.doNotTrack) {
            return window.doNotTrack
        } else {
            return options.NOT_AVAILABLE
        }
    }
    // This is a crude and primitive touch screen detection.
    // It's not possible to currently reliably detect the  availability of a touch screen
    // with a JS, without actually subscribing to a touch event.
    // http://www.stucox.com/blog/you-cant-detect-a-touchscreen/
    // https://github.com/Modernizr/Modernizr/issues/548
    // method returns an array of 3 values:
    // maxTouchPoints, the success or failure of creating a TouchEvent,
    // and the availability of the 'ontouchstart' property

    var getTouchSupport = function () {
        var maxTouchPoints = 0
        var touchEvent
        if (typeof navigator.maxTouchPoints !== 'undefined') {
            maxTouchPoints = navigator.maxTouchPoints
        } else if (typeof navigator.msMaxTouchPoints !== 'undefined') {
            maxTouchPoints = navigator.msMaxTouchPoints
        }
        try {
            document.createEvent('TouchEvent')
            touchEvent = true
        } catch (_) {
            touchEvent = false
        }
        var touchStart = 'ontouchstart' in window
        return [maxTouchPoints, touchEvent, touchStart]
    }
    // https://www.browserleaks.com/canvas#how-does-it-work

    var getCanvasFp = function (options) {
        var result = []
        // Very simple now, need to make it more complex (geo shapes etc)
        var canvas = document.createElement('canvas')
        canvas.width = 2000
        canvas.height = 200
        canvas.style.display = 'inline'
        var ctx = canvas.getContext('2d')
        // detect browser support of canvas winding
        // http://blogs.adobe.com/webplatform/2013/01/30/winding-rules-in-canvas/
        // https://github.com/Modernizr/Modernizr/blob/master/feature-detects/canvas/winding.js
        ctx.rect(0, 0, 10, 10)
        ctx.rect(2, 2, 6, 6)
        result.push('canvas winding:' + ((ctx.isPointInPath(5, 5, 'evenodd') === false) ? 'yes' : 'no'))

        ctx.textBaseline = 'alphabetic'
        ctx.fillStyle = '#f60'
        ctx.fillRect(125, 1, 62, 20)
        ctx.fillStyle = '#069'
        // https://github.com/Valve/fingerprintjs2/issues/66
        if (options.dontUseFakeFontInCanvas) {
            ctx.font = '11pt Arial'
        } else {
            ctx.font = '11pt no-real-font-123'
        }
        ctx.fillText('Cwm fjordbank glyphs vext quiz, \ud83d\ude03', 2, 15)
        ctx.fillStyle = 'rgba(102, 204, 0, 0.2)'
        ctx.font = '18pt Arial'
        ctx.fillText('Cwm fjordbank glyphs vext quiz, \ud83d\ude03', 4, 45)

        // canvas blending
        // http://blogs.adobe.com/webplatform/2013/01/28/blending-features-in-canvas/
        // http://jsfiddle.net/NDYV8/16/
        ctx.globalCompositeOperation = 'multiply'
        ctx.fillStyle = 'rgb(255,0,255)'
        ctx.beginPath()
        ctx.arc(50, 50, 50, 0, Math.PI * 2, true)
        ctx.closePath()
        ctx.fill()
        ctx.fillStyle = 'rgb(0,255,255)'
        ctx.beginPath()
        ctx.arc(100, 50, 50, 0, Math.PI * 2, true)
        ctx.closePath()
        ctx.fill()
        ctx.fillStyle = 'rgb(255,255,0)'
        ctx.beginPath()
        ctx.arc(75, 100, 50, 0, Math.PI * 2, true)
        ctx.closePath()
        ctx.fill()
        ctx.fillStyle = 'rgb(255,0,255)'
        // canvas winding
        // http://blogs.adobe.com/webplatform/2013/01/30/winding-rules-in-canvas/
        // http://jsfiddle.net/NDYV8/19/
        ctx.arc(75, 75, 75, 0, Math.PI * 2, true)
        ctx.arc(75, 75, 25, 0, Math.PI * 2, true)
        ctx.fill('evenodd')

        if (canvas.toDataURL) { result.push('canvas fp:' + canvas.toDataURL()) }
        return result
    }
    var getWebglFp = function () {
        var gl
        var fa2s = function (fa) {
            gl.clearColor(0.0, 0.0, 0.0, 1.0)
            gl.enable(gl.DEPTH_TEST)
            gl.depthFunc(gl.LEQUAL)
            gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT)
            return '[' + fa[0] + ', ' + fa[1] + ']'
        }
        var maxAnisotropy = function (gl) {
            var ext = gl.getExtension('EXT_texture_filter_anisotropic') || gl.getExtension('WEBKIT_EXT_texture_filter_anisotropic') || gl.getExtension('MOZ_EXT_texture_filter_anisotropic')
            if (ext) {
                var anisotropy = gl.getParameter(ext.MAX_TEXTURE_MAX_ANISOTROPY_EXT)
                if (anisotropy === 0) {
                    anisotropy = 2
                }
                return anisotropy
            } else {
                return null
            }
        }

        gl = getWebglCanvas()
        if (!gl) { return null }
        // WebGL fingerprinting is a combination of techniques, found in MaxMind antifraud script & Augur fingerprinting.
        // First it draws a gradient object with shaders and convers the image to the Base64 string.
        // Then it enumerates all WebGL extensions & capabilities and appends them to the Base64 string, resulting in a huge WebGL string, potentially very unique on each device
        // Since iOS supports webgl starting from version 8.1 and 8.1 runs on several graphics chips, the results may be different across ios devices, but we need to verify it.
        var result = []
        var vShaderTemplate = 'attribute vec2 attrVertex;varying vec2 varyinTexCoordinate;uniform vec2 uniformOffset;void main(){varyinTexCoordinate=attrVertex+uniformOffset;gl_Position=vec4(attrVertex,0,1);}'
        var fShaderTemplate = 'precision mediump float;varying vec2 varyinTexCoordinate;void main() {gl_FragColor=vec4(varyinTexCoordinate,0,1);}'
        var vertexPosBuffer = gl.createBuffer()
        gl.bindBuffer(gl.ARRAY_BUFFER, vertexPosBuffer)
        var vertices = new Float32Array([-0.2, -0.9, 0, 0.4, -0.26, 0, 0, 0.732134444, 0])
        gl.bufferData(gl.ARRAY_BUFFER, vertices, gl.STATIC_DRAW)
        vertexPosBuffer.itemSize = 3
        vertexPosBuffer.numItems = 3
        var program = gl.createProgram()
        var vshader = gl.createShader(gl.VERTEX_SHADER)
        gl.shaderSource(vshader, vShaderTemplate)
        gl.compileShader(vshader)
        var fshader = gl.createShader(gl.FRAGMENT_SHADER)
        gl.shaderSource(fshader, fShaderTemplate)
        gl.compileShader(fshader)
        gl.attachShader(program, vshader)
        gl.attachShader(program, fshader)
        gl.linkProgram(program)
        gl.useProgram(program)
        program.vertexPosAttrib = gl.getAttribLocation(program, 'attrVertex')
        program.offsetUniform = gl.getUniformLocation(program, 'uniformOffset')
        gl.enableVertexAttribArray(program.vertexPosArray)
        gl.vertexAttribPointer(program.vertexPosAttrib, vertexPosBuffer.itemSize, gl.FLOAT, !1, 0, 0)
        gl.uniform2f(program.offsetUniform, 1, 1)
        gl.drawArrays(gl.TRIANGLE_STRIP, 0, vertexPosBuffer.numItems)
        try {
            result.push(gl.canvas.toDataURL())
        } catch (e) {
            /* .toDataURL may be absent or broken (blocked by extension) */
        }
        result.push('extensions:' + (gl.getSupportedExtensions() || []).join(';'))
        result.push('webgl aliased line width range:' + fa2s(gl.getParameter(gl.ALIASED_LINE_WIDTH_RANGE)))
        result.push('webgl aliased point size range:' + fa2s(gl.getParameter(gl.ALIASED_POINT_SIZE_RANGE)))
        result.push('webgl alpha bits:' + gl.getParameter(gl.ALPHA_BITS))
        result.push('webgl antialiasing:' + (gl.getContextAttributes().antialias ? 'yes' : 'no'))
        result.push('webgl blue bits:' + gl.getParameter(gl.BLUE_BITS))
        result.push('webgl depth bits:' + gl.getParameter(gl.DEPTH_BITS))
        result.push('webgl green bits:' + gl.getParameter(gl.GREEN_BITS))
        result.push('webgl max anisotropy:' + maxAnisotropy(gl))
        result.push('webgl max combined texture image units:' + gl.getParameter(gl.MAX_COMBINED_TEXTURE_IMAGE_UNITS))
        result.push('webgl max cube map texture size:' + gl.getParameter(gl.MAX_CUBE_MAP_TEXTURE_SIZE))
        result.push('webgl max fragment uniform vectors:' + gl.getParameter(gl.MAX_FRAGMENT_UNIFORM_VECTORS))
        result.push('webgl max render buffer size:' + gl.getParameter(gl.MAX_RENDERBUFFER_SIZE))
        result.push('webgl max texture image units:' + gl.getParameter(gl.MAX_TEXTURE_IMAGE_UNITS))
        result.push('webgl max texture size:' + gl.getParameter(gl.MAX_TEXTURE_SIZE))
        result.push('webgl max varying vectors:' + gl.getParameter(gl.MAX_VARYING_VECTORS))
        result.push('webgl max vertex attribs:' + gl.getParameter(gl.MAX_VERTEX_ATTRIBS))
        result.push('webgl max vertex texture image units:' + gl.getParameter(gl.MAX_VERTEX_TEXTURE_IMAGE_UNITS))
        result.push('webgl max vertex uniform vectors:' + gl.getParameter(gl.MAX_VERTEX_UNIFORM_VECTORS))
        result.push('webgl max viewport dims:' + fa2s(gl.getParameter(gl.MAX_VIEWPORT_DIMS)))
        result.push('webgl red bits:' + gl.getParameter(gl.RED_BITS))
        result.push('webgl renderer:' + gl.getParameter(gl.RENDERER))
        result.push('webgl shading language version:' + gl.getParameter(gl.SHADING_LANGUAGE_VERSION))
        result.push('webgl stencil bits:' + gl.getParameter(gl.STENCIL_BITS))
        result.push('webgl vendor:' + gl.getParameter(gl.VENDOR))
        result.push('webgl version:' + gl.getParameter(gl.VERSION))

        try {
            // Add the unmasked vendor and unmasked renderer if the debug_renderer_info extension is available
            var extensionDebugRendererInfo = gl.getExtension('WEBGL_debug_renderer_info')
            if (extensionDebugRendererInfo) {
                result.push('webgl unmasked vendor:' + gl.getParameter(extensionDebugRendererInfo.UNMASKED_VENDOR_WEBGL))
                result.push('webgl unmasked renderer:' + gl.getParameter(extensionDebugRendererInfo.UNMASKED_RENDERER_WEBGL))
            }
        } catch (e) { /* squelch */ }

        if (!gl.getShaderPrecisionFormat) {
            return result
        }

        each(['FLOAT', 'INT'], function (numType) {
            each(['VERTEX', 'FRAGMENT'], function (shader) {
                each(['HIGH', 'MEDIUM', 'LOW'], function (numSize) {
                    each(['precision', 'rangeMin', 'rangeMax'], function (key) {
                        var format = gl.getShaderPrecisionFormat(gl[shader + '_SHADER'], gl[numSize + '_' + numType])[key]
                        if (key !== 'precision') {
                            key = 'precision ' + key
                        }
                        var line = ['webgl ', shader.toLowerCase(), ' shader ', numSize.toLowerCase(), ' ', numType.toLowerCase(), ' ', key, ':', format].join('')
                        result.push(line)
                    })
                })
            })
        })
        return result
    }
    var getWebglVendorAndRenderer = function () {
        /* This a subset of the WebGL fingerprint with a lot of entropy, while being reasonably browser-independent */
        try {
            var glContext = getWebglCanvas()
            var extensionDebugRendererInfo = glContext.getExtension('WEBGL_debug_renderer_info')
            return glContext.getParameter(extensionDebugRendererInfo.UNMASKED_VENDOR_WEBGL) + '~' + glContext.getParameter(extensionDebugRendererInfo.UNMASKED_RENDERER_WEBGL)
        } catch (e) {
            return null
        }
    }
    var getAdBlock = function () {
        var ads = document.createElement('div')
        ads.innerHTML = '&nbsp;'
        ads.className = 'adsbox'
        var result = false
        try {
            // body may not exist, that's why we need try/catch
            document.body.appendChild(ads)
            result = document.getElementsByClassName('adsbox')[0].offsetHeight === 0
            document.body.removeChild(ads)
        } catch (e) {
            result = false
        }
        return result
    }
    var getHasLiedLanguages = function () {
        // We check if navigator.language is equal to the first language of navigator.languages
        // navigator.languages is undefined on IE11 (and potentially older IEs)
        if (typeof navigator.languages !== 'undefined') {
            try {
                var firstLanguages = navigator.languages[0].substr(0, 2)
                if (firstLanguages !== navigator.language.substr(0, 2)) {
                    return true
                }
            } catch (err) {
                return true
            }
        }
        return false
    }
    var getHasLiedResolution = function () {
        return window.screen.width < window.screen.availWidth || window.screen.height < window.screen.availHeight
    }
    var getHasLiedOs = function () {
        var userAgent = navigator.userAgent.toLowerCase()
        var oscpu = navigator.oscpu
        var platform = navigator.platform.toLowerCase()
        var os
        // We extract the OS from the user agent (respect the order of the if else if statement)
        if (userAgent.indexOf('windows phone') >= 0) {
            os = 'Windows Phone'
        } else if (userAgent.indexOf('win') >= 0) {
            os = 'Windows'
        } else if (userAgent.indexOf('android') >= 0) {
            os = 'Android'
        } else if (userAgent.indexOf('linux') >= 0 || userAgent.indexOf('cros') >= 0) {
            os = 'Linux'
        } else if (userAgent.indexOf('iphone') >= 0 || userAgent.indexOf('ipad') >= 0) {
            os = 'iOS'
        } else if (userAgent.indexOf('mac') >= 0) {
            os = 'Mac'
        } else {
            os = 'Other'
        }
        // We detect if the person uses a mobile device
        var mobileDevice = (('ontouchstart' in window) ||
            (navigator.maxTouchPoints > 0) ||
            (navigator.msMaxTouchPoints > 0))

        if (mobileDevice && os !== 'Windows Phone' && os !== 'Android' && os !== 'iOS' && os !== 'Other') {
            return true
        }

        // We compare oscpu with the OS extracted from the UA
        if (typeof oscpu !== 'undefined') {
            oscpu = oscpu.toLowerCase()
            if (oscpu.indexOf('win') >= 0 && os !== 'Windows' && os !== 'Windows Phone') {
                return true
            } else if (oscpu.indexOf('linux') >= 0 && os !== 'Linux' && os !== 'Android') {
                return true
            } else if (oscpu.indexOf('mac') >= 0 && os !== 'Mac' && os !== 'iOS') {
                return true
            } else if ((oscpu.indexOf('win') === -1 && oscpu.indexOf('linux') === -1 && oscpu.indexOf('mac') === -1) !== (os === 'Other')) {
                return true
            }
        }

        // We compare platform with the OS extracted from the UA
        if (platform.indexOf('win') >= 0 && os !== 'Windows' && os !== 'Windows Phone') {
            return true
        } else if ((platform.indexOf('linux') >= 0 || platform.indexOf('android') >= 0 || platform.indexOf('pike') >= 0) && os !== 'Linux' && os !== 'Android') {
            return true
        } else if ((platform.indexOf('mac') >= 0 || platform.indexOf('ipad') >= 0 || platform.indexOf('ipod') >= 0 || platform.indexOf('iphone') >= 0) && os !== 'Mac' && os !== 'iOS') {
            return true
        } else {
            var platformIsOther = platform.indexOf('win') < 0 &&
                platform.indexOf('linux') < 0 &&
                platform.indexOf('mac') < 0 &&
                platform.indexOf('iphone') < 0 &&
                platform.indexOf('ipad') < 0
            if (platformIsOther !== (os === 'Other')) {
                return true
            }
        }

        return typeof navigator.plugins === 'undefined' && os !== 'Windows' && os !== 'Windows Phone'
    }
    var getHasLiedBrowser = function () {
        var userAgent = navigator.userAgent.toLowerCase()
        var productSub = navigator.productSub

        // we extract the browser from the user agent (respect the order of the tests)
        var browser
        if (userAgent.indexOf('firefox') >= 0) {
            browser = 'Firefox'
        } else if (userAgent.indexOf('opera') >= 0 || userAgent.indexOf('opr') >= 0) {
            browser = 'Opera'
        } else if (userAgent.indexOf('chrome') >= 0) {
            browser = 'Chrome'
        } else if (userAgent.indexOf('safari') >= 0) {
            browser = 'Safari'
        } else if (userAgent.indexOf('trident') >= 0) {
            browser = 'Internet Explorer'
        } else {
            browser = 'Other'
        }

        if ((browser === 'Chrome' || browser === 'Safari' || browser === 'Opera') && productSub !== '20030107') {
            return true
        }

        // eslint-disable-next-line no-eval
        var tempRes = eval.toString().length
        if (tempRes === 37 && browser !== 'Safari' && browser !== 'Firefox' && browser !== 'Other') {
            return true
        } else if (tempRes === 39 && browser !== 'Internet Explorer' && browser !== 'Other') {
            return true
        } else if (tempRes === 33 && browser !== 'Chrome' && browser !== 'Opera' && browser !== 'Other') {
            return true
        }

        // We create an error to see how it is handled
        var errFirefox
        try {
            // eslint-disable-next-line no-throw-literal
            throw 'a'
        } catch (err) {
            try {
                err.toSource()
                errFirefox = true
            } catch (errOfErr) {
                errFirefox = false
            }
        }
        return errFirefox && browser !== 'Firefox' && browser !== 'Other'
    }
    var isCanvasSupported = function () {
        var elem = document.createElement('canvas')
        return !!(elem.getContext && elem.getContext('2d'))
    }
    var isWebGlSupported = function () {
        // code taken from Modernizr
        if (!isCanvasSupported()) {
            return false
        }

        var glContext = getWebglCanvas()
        return !!window.WebGLRenderingContext && !!glContext
    }
    var isIE = function () {
        if (navigator.appName === 'Microsoft Internet Explorer') {
            return true
        } else if (navigator.appName === 'Netscape' && /Trident/.test(navigator.userAgent)) { // IE 11
            return true
        }
        return false
    }
    var hasSwfObjectLoaded = function () {
        return typeof window.swfobject !== 'undefined'
    }
    var hasMinFlashInstalled = function () {
        return window.swfobject.hasFlashPlayerVersion('9.0.0')
    }
    var addFlashDivNode = function (options) {
        var node = document.createElement('div')
        node.setAttribute('id', options.fonts.swfContainerId)
        document.body.appendChild(node)
    }
    var loadSwfAndDetectFonts = function (done, options) {
        var hiddenCallback = '___fp_swf_loaded'
        window[hiddenCallback] = function (fonts) {
            done(fonts)
        }
        var id = options.fonts.swfContainerId
        addFlashDivNode()
        var flashvars = { onReady: hiddenCallback }
        var flashparams = { allowScriptAccess: 'always', menu: 'false' }
        window.swfobject.embedSWF(options.fonts.swfPath, id, '1', '1', '9.0.0', false, flashvars, flashparams, {})
    }
    var getWebglCanvas = function () {
        var canvas = document.createElement('canvas')
        var gl = null
        try {
            gl = canvas.getContext('webgl') || canvas.getContext('experimental-webgl')
        } catch (e) { /* squelch */ }
        if (!gl) { gl = null }
        return gl
    }

    var components = [
        { key: 'userAgent', getData: UserAgent },
        { key: 'webdriver', getData: webdriver },
        { key: 'language', getData: languageKey },
        { key: 'colorDepth', getData: colorDepthKey },
        { key: 'deviceMemory', getData: deviceMemoryKey },
        { key: 'pixelRatio', getData: pixelRatioKey },
        { key: 'hardwareConcurrency', getData: hardwareConcurrencyKey },
        { key: 'screenResolution', getData: screenResolutionKey },
        { key: 'availableScreenResolution', getData: availableScreenResolutionKey },
        { key: 'timezoneOffset', getData: timezoneOffset },
        { key: 'timezone', getData: timezone },
        { key: 'sessionStorage', getData: sessionStorageKey },
        { key: 'localStorage', getData: localStorageKey },
        { key: 'indexedDb', getData: indexedDbKey },
        { key: 'addBehavior', getData: addBehaviorKey },
        { key: 'openDatabase', getData: openDatabaseKey },
        { key: 'cpuClass', getData: cpuClassKey },
        { key: 'platform', getData: platformKey },
        { key: 'doNotTrack', getData: doNotTrackKey },
        { key: 'plugins', getData: pluginsComponent },
        { key: 'canvas', getData: canvasKey },
        { key: 'webgl', getData: webglKey },
        { key: 'webglVendorAndRenderer', getData: webglVendorAndRendererKey },
        { key: 'adBlock', getData: adBlockKey },
        { key: 'hasLiedLanguages', getData: hasLiedLanguagesKey },
        { key: 'hasLiedResolution', getData: hasLiedResolutionKey },
        { key: 'hasLiedOs', getData: hasLiedOsKey },
        { key: 'hasLiedBrowser', getData: hasLiedBrowserKey },
        { key: 'touchSupport', getData: touchSupportKey },
        { key: 'fonts', getData: jsFontsKey, pauseBefore: true },
        { key: 'fontsFlash', getData: flashFontsKey, pauseBefore: true },
        { key: 'audio', getData: audioKey },
        { key: 'enumerateDevices', getData: enumerateDevicesKey }
    ]

    var Fingerprint2 = function (options) {
        throw new Error("'new Fingerprint()' is deprecated, see https://github.com/Valve/fingerprintjs2#upgrade-guide-from-182-to-200")
    }

    Fingerprint2.get = function (options, callback) {
        if (!callback) {
            callback = options
            options = {}
        } else if (!options) {
            options = {}
        }
        extendSoft(options, defaultOptions)
        options.components = options.extraComponents.concat(components)

        var keys = {
            data: [],
            addPreprocessedComponent: function (key, value) {
                if (typeof options.preprocessor === 'function') {
                    value = options.preprocessor(key, value)
                }
                keys.data.push({ key: key, value: value })
            }
        }

        var i = -1
        var chainComponents = function (alreadyWaited) {
            i += 1
            if (i >= options.components.length) { // on finish
                callback(keys.data)
                return
            }
            var component = options.components[i]

            if (options.excludes[component.key]) {
                chainComponents(false) // skip
                return
            }

            if (!alreadyWaited && component.pauseBefore) {
                i -= 1
                setTimeout(function () {
                    chainComponents(true)
                }, 1)
                return
            }

            try {
                component.getData(function (value) {
                    keys.addPreprocessedComponent(component.key, value)
                    chainComponents(false)
                }, options)
            } catch (error) {
                // main body error
                keys.addPreprocessedComponent(component.key, String(error))
                chainComponents(false)
            }
        }

        chainComponents(false)
    }

    Fingerprint2.getPromise = function (options) {
        return new Promise(function (resolve, reject) {
            Fingerprint2.get(options, resolve)
        })
    }

    Fingerprint2.getV18 = function (options, callback) {
        if (callback == null) {
            callback = options
            options = {}
        }
        return Fingerprint2.get(options, function (components) {
            var newComponents = []
            for (var i = 0; i < components.length; i++) {
                var component = components[i]
                if (component.value === (options.NOT_AVAILABLE || 'not available')) {
                    newComponents.push({ key: component.key, value: 'unknown' })
                } else if (component.key === 'plugins') {
                    newComponents.push({
                        key: 'plugins',
                        value: map(component.value, function (p) {
                            var mimeTypes = map(p[2], function (mt) {
                                if (mt.join) { return mt.join('~') }
                                return mt
                            }).join(',')
                            return [p[0], p[1], mimeTypes].join('::')
                        })
                    })
                } else if (['canvas', 'webgl'].indexOf(component.key) !== -1) {
                    newComponents.push({ key: component.key, value: component.value.join('~') })
                } else if (['sessionStorage', 'localStorage', 'indexedDb', 'addBehavior', 'openDatabase'].indexOf(component.key) !== -1) {
                    if (component.value) {
                        newComponents.push({ key: component.key, value: 1 })
                    } else {
                        // skip
                        continue
                    }
                } else {
                    if (component.value) {
                        newComponents.push(component.value.join ? { key: component.key, value: component.value.join(';') } : component)
                    } else {
                        newComponents.push({ key: component.key, value: component.value })
                    }
                }
            }
            var murmur = x64hash128(map(newComponents, function (component) { return component.value }).join('~~~'), 31)
            callback(murmur, newComponents)
        })
    }
    //自定义参数
    Fingerprint2.getMyComponents = function (options) {
        extendSoft(options, defaultOptions)
        options.components = options.extraComponents.concat(components)
        var keys = {
            data: [],
            addPreprocessedComponent: function (key, value) {
                if (typeof options.preprocessor === 'function') {
                    value = options.preprocessor(key, value)
                }
                keys.data.push({ key: key, value: value })
            }
        }
        for (var i = 0; i < options.components.length; i++) {
            var component = options.components[i]
            if (options.excludes[component.key]) {
                continue
            }
            try {
                component.getData(function (value) {
                    keys.addPreprocessedComponent(component.key, value)
                }, options)
            } catch (error) {
                // main body error
                keys.addPreprocessedComponent(component.key, String(error))
            }
        }
        return keys.data;
    }
    //获取指纹
    Fingerprint2.GetMyFingerprint = function () {
        var options = {
            excludes: {
                enumerateDevices: true,
                audio: true,
                canvas: true,
                webgl: true
            }
        };
        var MyFins;// 指纹
        var components = Fingerprint2.getMyComponents(options);
        var values = components.map(function (component) {
            return component.value
        });
        MyFins = Fingerprint2.x64hash128(values.join(''), 31);// 指纹
        return MyFins;
    }
    Fingerprint2.x64hash128 = x64hash128
    Fingerprint2.VERSION = '2.1.0'
    return Fingerprint2
})

 

var DCDomTools = new Object();

DCDomTools.FlagsEnumValueToString = function (v, itemNames, itemValues) {
    if (typeof (v) != "number") {
        return itemNames[0];
    }
    var strResult = null;
    var len = itemNames.length;
    for (var iCount = 0; iCount < len; iCount++) {
        var itemValue = itemValues[iCount];
        if ((v & itemValue) == itemValue) {
            v -= itemValue;
            if (strResult == null) {
                strResult = itemNames[iCount];
            }
            else {
                strResult = strResult + "," + itemNames[iCount];
            }
        }
    }//for
    if (v != 0) {
        if (strResult == null) {
            strResult = v.toString();
        }
        else {
            strResult = strResult + "," + v.toString();
        }
    }
    return strResult;
};

DCDomTools.StringToFlagsEnumValue = function (strValue, itemNames, itemValues) {
    if (typeof (strValue) != "string") {
        return itemValues[0];
    }
    if (strValue.indexOf(",") >= 0) {
        var strItems = strValue.split(",");
        var intResult = 0;
        for (var iCount = 0; iCount < strItems.length; iCount++) {
            var strItem = strItems[iCount].trim();
            var itemIndex = itemNames.indexOf(strItem);
            if (itemIndex >= 0) {
                intResult += itemValues[itemIndex];
            }
            else {
                itemIndex = parseInt(strItem);
                if (isNaN(itemIndex) == false) {
                    intResult += itemIndex;
                }
            }
        }//for
        return intResult;
    }
    else {
        var intResult = itemNames.indexOf(strValue);
        if (intResult >= 0) {
            return itemValues[intResult];
        }
        else {
            intResult = parseInt(strValue);
            if (isNaN(intResult) == false) {
                return intResult;
            }
            return itemValues[0];
        }
    }
};

DCDomTools.StringToEnumValue = function (strValue, itemNames) {
    if (typeof (strValue) == "string" && itemNames.indexOf) {
        var itemIndex = itemNames.indexOf(str);
        if (itemIndex >= 0) {
            return itemIndex;
        }
    }
    else {
        for (var iCount = itemNames.length - 1; iCount >= 0; iCount--) {
            if (itemNames[iCount] == str) {
                return iCount;
            }
        }
    }
    return 0;
};

DCDomTools.EnumValueToString = function(v, itemNames){
    if (v >= 0 && v < itemNames.length) {
        return itemNames[v];
    }
    else {
        return itemNames[0];
    }
};

DCDomTools.getSessionID = function () {
    if (DCDomTools.__Fingerprint == null) {
        DCDomTools.__Fingerprint = Fingerprint2.GetMyFingerprint();
    }
    return DCDomTools.__Fingerprint;

    if (document.WriterControl != null) {
        var sid2 = document.WriterControl.__globalClientID;
        if (sid2 != null && sid2.length > 0) {
            return sid2;
        }
    }

    var sid = null;
    var cookies = document.cookie;
    if (cookies != null
        && cookies.length > 0
        && (cookies.indexOf("SessionId") >= 0
            || cookies.indexOf("SessionID") >= 0
            || cookies.indexOf("sessionid") >= 0)) {

    }
    else {
        sid = DCDomTools.GetDCSessionID20201022();// window.top.dc_sessionid20201022;
        if (sid == null || sid.length == 0) {
            if (__error_window_localStorage == false) {
                try {
                    if (window.localStorage) {
                        sid = window.localStorage["dc_sessionid20201022"];
                        if (sid == null || sid.length == 0) {
                            sid = "sid" + new Date().valueOf();
                            window.localStorage["dc_sessionid20201022"] = sid;
                        }
                    }
                }
                catch (e) {
                    __error_window_localStorage = true;
                }
            }
            if (sid == null || sid.length == 0) {
                sid = "sid" + new Date().valueOf();
            }
            DCDomTools.SetDCSessionID20201022(sid);//window.top.dc_sessionid20201022 = sid;
        }
    }
    return sid;
};

//// 获得全局客户端编号
//DCDomTools.getGlobalClientID = function () {

    
//    if (DCDomTools.__globalClientID == null) {

        
//        var wtop = window.top;
//        var id = wtop.DCClientID20200617;
//        if (typeof (id) == "undefined") {
//            id = new Date().valueOf().toString();
//            wtop.DCClientID20200617 = id;
//        }
//        return id;
//    }
//    return DCDomTools.__globalClientID;
//};

// 对文档节点进行排序，如果修改了文档结构返回true,否则返回false。
DCDomTools.sortChildNodes = function (rootNode, sortFunction) {
    if (rootNode == null) {
        return;
    }
    var list = new Array();
    var nodes = rootNode.childNodes;
    for (var iCount = 0; iCount < nodes.length; iCount++) {
        list.push(nodes[iCount]);

    }
    list.sort(sortFunction);
    var changed = false;
    for (var iCount = 0; iCount < list.length; iCount++) {
        if (list[iCount] != nodes[iCount]) {
            changed = true;
            break;
        }
    }
    if (changed == true) {
        while (rootNode.firstChild != null) {
            rootNode.removeChild(rootNode.firstChild);
        }
        for (var iCount = 0; iCount < list.length; iCount++) {
            rootNode.appendChild(list[iCount]);
        }
        return true;
    }
    else {
        return false;
    }
};

// 判断一个字符串是否以另外一个字符串打头。
DCDomTools.StartsWith = function(bigStr, smallStr)
{
    if (smallStr == null || smallStr.length == 0) {
        return true;
    }
    if (bigStr != null && smallStr != null && bigStr.length >= smallStr.length) {
        var str2 = bigStr.substr(0, smallStr.length);
        if (str2 == smallStr) {
            return true;
        }
    }
    return false;
};

DCDomTools.getSelectionRange = function () {
    var sel = DCDomTools.getSelection();
    if (sel != null
        && sel.getRangeAt
        && sel.rangeCount >= 1 
        && document.body.getAttribute("ismobiledevice") !== "true") {
        var rng = sel.getRangeAt(0);
        return rng;
    }
    return null;
};

//伍贻超20190717：前端对字符串进行HTML解码，要求传入的字符串必须是HTML编码后
DCDomTools.HTMLDecode = function (str) {
    //var div = document.createElement("div");
    //div.innerHTML = str;
    //return div.innerText;

    var HTML_DECODE = {
        "&lt;": "<",
        "&gt;": ">",
        "&amp;": "&",
        "&nbsp;": " ",
        "&quot;": "\"",
        "&copy;": ""
    };
    var REGX_HTML_ENCODE = /"|&|'|<|>|[\x00-\x20]|[\x7F-\xFF]|[\u0100-\u2700]/g;
    var REGX_HTML_DECODE = /&\w+;|&#(\d+);/g;
    var REGX_TRIM = /(^\s*)|(\s*$)/g;
    str = (str != undefined) ? str : "";
    return (typeof str != "string") ? str :
        str.replace(REGX_HTML_DECODE,
            function ($0, $1) {
                var c = HTML_DECODE[$0];
                if (c == undefined) {
                    if (!isNaN($1)) {
                        c = String.fromCharCode(($1 == 160) ? 32 : $1);
                    } else {
                        c = $0;
                    }
                }
                return c;
            });
};

//伍贻超20190717：前端对字符串进行HTML编码，要求传入的字符串必须是HTML明码
DCDomTools.HTMLEncode = function (str) {
    //var div = document.createElement("div");
    //div.innerText = str;
    //return div.innerHTML;

    return !str ? str : String(str).replace(/&/g, "&amp;").replace(/>/g, "&gt;").replace(/</g, "&lt;").replace(/"/g, "&quot;");
};


// 获得被选中的所有文档节点
//@param splitText 是否根据需要拆分文本节点。
DCDomTools.GetSelectionNodes = function (splitText) {
    var resultNodes = new Array();
    var sel = DCDomTools.getSelection();
    if (sel == null) {
        return txtNodes;
    }
    if (sel.getRangeAt) {
        for (var iCount = 0; iCount < sel.rangeCount; iCount++) {
            var rng = sel.getRangeAt(iCount);

            var endNode = rng.endContainer;
            //优化标签删除 2019-10-17 hulijun
            if (endNode && endNode.hasChildNodes()) {
                endNode = endNode.lastChild;
            }
            if (splitText != false) {
                if (endNode.nodeType == 3 && endNode.length > rng.endOffset && rng.endOffset > 0) {
                    // 拆分结尾文本节点
                    var node2 = endNode.splitText(rng.endOffset);
                }
            }

            var startNode = rng.startContainer;
            if (splitText != false ) {
                if (startNode.nodeType == 3 && startNode.length > rng.startOffset && rng.startOffset > 0) {
                    // 拆分起始文本节点
                    var node2 = startNode.splitText(rng.startOffset);
                    if (startNode == endNode) {
                        endNode = node2;
                    }
                    startNode = node2;
                }
            }

            var totalParent = rng.commonAncestorContainer;
            if (totalParent == null) {
                var p1 = startNode.parentNode;
                while (p1 != null) {
                    var p2 = endNode.parentNode;
                    while (p2 != null) {
                        if (p1 == p2) {
                            totalParent = p1;
                            break;
                        }
                        p2 = p2.parentNode;
                    }
                    p1 = p1.parentNode;
                }
                if (totalParent != null) {
                    break;
                }
            }
            if (startNode == endNode) {
                if (startNode.nodeType == 3) {
                    resultNodes.push(startNode);
                }
                else {
                    function GetNodes(rootNode) {
                        var len = rootNode.childNodes.length;
                        for (var iCount = 0; iCount < len; iCount++) {
                            var subNode = rootNode.childNodes[iCount];
                            resultNodes.push(subNode);
                            if (subNode.nodeType == 3) {
                                GetNodes(subNode);
                            }
                        }
                    }
                    GetNodes(startNode);
                }
            }
            else {
                var node = startNode;
                while (true) {
                    resultNodes.push(node);
                    // 获得DOM树中的下一个节点
                    var nextNode = null;
                    if (node.nodeType == 1) {
                        // 元素节点。
                        if (node.firstChild) {
                            nextNode = node.firstChild;
                        }
                        else {
                            nextNode = node.nextSibling;
                        }
                    }
                    else {
                        // 非元素节点
                        nextNode = node.nextSibling;
                    }
                    if (nextNode == null) {
                        nextNode = node;
                        while (nextNode != null) {
                            if (nextNode.nextSibling == null) {
                                nextNode = nextNode.parentNode;
                            } else {
                                nextNode = nextNode.nextSibling;
                                break;
                            }
                            if (nextNode == totalParent) {
                                break;
                            }
                        }//while
                    }
                    if (nextNode == endNode) {
                        resultNodes.push(nextNode);
                        // 遇到结尾节点则完成工作，退出循环。
                        break;
                    }
                    if (nextNode == null || nextNode == totalParent) {
                        // 下一个节点为空则退出循环。
                        break;
                    }
                    node = nextNode;
                }//while
            }
        }
    }
    return resultNodes;
};

DCDomTools.fixAjaxSettings = function(settings , myWriterControl )
{
    if (DCDomTools.getIEVersion() >= 10 || DCDomTools.getIEVersion() == -1 ) {
        settings.xhrFields = { withCredentials: true };
        settings.crossDomain = true;
    }
    else {
        // jquery版本兼容
        jQuery.support && (jQuery.support.cors = true);
    }
    if (myWriterControl == null) {
        myWriterControl = document.WriterControl;
    }
    //wyc20201203:添加全局的同步异步设置
    if (settings.async !== undefined &&
        myWriterControl &&
        myWriterControl.Options) {
        var set = myWriterControl.Options.AJAXAsync;
        if (set === false || set === "false") {
            settings.async = false;
        } else if (set === true || set === "true") {
            settings.async = true;
        }
    }

    //zhangbin20220407: 添加响应头contentType: text/plain
    if(typeof settings.data == 'string' || typeof settings.data == 'undefined'){
        settings.contentType = 'text/plain;charset=UTF-8'
    }

    //wyc20200820：添加自定义的AJAX请求头
    if (myWriterControl != null
        && myWriterControl.Options != null
        && myWriterControl.Options.AttachedAJAXHeader != null) {
        var _AttachedAJAXHeader = myWriterControl.Options.AttachedAJAXHeader
        var typestr = typeof (_AttachedAJAXHeader);
        var attachedHeaderObj = null;
        if (typestr == "object") {
            attachedHeaderObj = _AttachedAJAXHeader;
        } else if (typestr == "string" && _AttachedAJAXHeader.slice(0, 1) == "{") {
            try {
                attachedHeaderObj = JSON.parse(_AttachedAJAXHeader);
            } catch (e) {
                console.log(e);
            }
        }
        if (attachedHeaderObj != null) {
            if (typeof (settings.headers) === "object") {
                for (var i in attachedHeaderObj) {
                    settings.headers[i] = attachedHeaderObj[i];
    }
            } else {
                settings.headers = attachedHeaderObj;
            }
        }
    }

    //wyc20221009:集中处理transformusebase64的转换
    if (myWriterControl != null &&
        typeof (myWriterControl.IsUseBase64Transfer) === "function" &&
        myWriterControl.IsUseBase64Transfer() === true &&
        settings.data != null &&
        typeof (settings.data) !== "object") {
        var excludecommandname = [];//wyc20221122先留空，以后需要时再引出
        //wyc20221122:处理排除项，某些命令不使用base64传输
        var index = settings.url.indexOf("?");
        var index2 = settings.url.indexOf("=");
        var commandname = settings.url.slice(index + 1, index2)
        if (excludecommandname.indexOf(commandname) === -1) {
            settings.url = settings.url + "&transformusebase64=true";
            settings.data = window.btoa(encodeURIComponent(settings.data));
        }
    }

    // yyf 20200831: 添加dc_sessionid的请求头

    var sid = DCDomTools.getSessionID();

    if (sid != null && sid.length > 0) {
        function analyseUrl(url) {
            if (url == null || url.length == 0) {
                return null;
            }
            var regex = /^(?:([a-z]*):)?(?:\/\/)?(?:([^:@]*)(?::([^@]*))?@)?([0-9a-z-._]+)?(?::([0-9]*))?(\/[^?#]*)?(?:\?([^#]*))?(?:#(.*))?$/i;
            //               1 - scheme              2 - user    3 = pass    4 - host           5 - port  6 - path        7 - query    8 - hash
             
            var md = url.match(regex) || [];

            var r = {
                "url": url,
                "scheme": md[1],
                "user": md[2],
                "pass": md[3],
                "host": md[4],
                "port": md[5] && +md[5],
                "path": md[6],
                "query": md[7],
                "hash": md[8]
            };
              
            return r;
        }
        settings.beforeSend = function (xhr, mySettings) {
            var loc = analyseUrl(document.location.href);
            var useHeader = false;
            if (loc != null) {
                if (loc.scheme == "file" || loc.scheme == null || loc.length == 0) {
                    // 从本地文件触发的
                    useHeader = false;
                }
                else {
                    var targetUrl = analyseUrl(mySettings.url);
                    if (targetUrl != null) {
                        if (targetUrl.scheme == null || targetUrl.scheme.length == 0) {
                            // 目标路径没有模式，应该是相对路径，则不是跨域
                            useHeader = true;
                        }
                        else {
                            if (loc.host == targetUrl.host && loc.port == targetUrl.port) {
                                // 没有跨域
                                useHeader = true;
                            }
                            else {
                                // 跨域了
                                useHeader = false;
                            }
                        }
                    }
                }
            }
            if (useHeader == true )
            {
                xhr.setRequestHeader("dcbid2022", sid);
                // 20210702 xym 注释报错代码(BSDCWRIT-306)
                // XMLHttpRequest不允许设置这些标题，它们是由浏览器自动设置的。原因在于，通过操纵这些标头，可能会欺骗服务器通过相同的连接接受第二个请求，而这种连接不会经过通常的安全检查 - 这将成为浏览器中的安全漏洞。
                // xhr.setRequestHeader("Sec-Fetch-Site", "same-origin");
            }
            //else
            {
                mySettings.url = DCDomTools.appendSessionIDToUrl(mySettings.url);
            }
        }
    }
    return settings;
};

var __error_window_localStorage = false;

DCDomTools.appendSessionIDToUrl = function (url) {
    if (url != null && url.length > 0) {
        var sid = DCDomTools.getSessionID();
        if (sid != null && sid.length > 0) {
            var index = url.indexOf("?");
            if (index > 0) {
                url = url + "&dcbid2022=" + sid;
            }
            else {
                url = url + "?dcbid2022=" + sid;
            }
        }
    }
    return url;
};

//@method 获得IE浏览器的版本号，如果不是IE浏览器则返回-1. 
DCDomTools.getIEVersion = function () {
    if (DCDomTools.__ieversion == null) {
        DCDomTools.__ieversion = -1;
        var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串  
        var isIE = userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1; //判断是否IE<11浏览器  
        var isEdge = userAgent.indexOf("Edge") > -1 && !isIE; //判断是否IE的Edge浏览器  
        var isIE11 = userAgent.indexOf('Trident') > -1 && userAgent.indexOf("rv:11.0") > -1;
        if (isIE) {
            var reIE = new RegExp("MSIE (\\d+\\.\\d+);");
            reIE.test(userAgent);
            var fIEVersion = parseFloat(RegExp["$1"]);
            if (fIEVersion == 7) {
                DCDomTools.__ieversion = 7;
            }
            else if (fIEVersion == 8) {
                DCDomTools.__ieversion = 8;
            }
            else if (fIEVersion == 9) {
                DCDomTools.__ieversion = 9;
            }
            else if (fIEVersion == 10) {
                DCDomTools.__ieversion = 10;
            }
            else {
                DCDomTools.__ieversion = 6;//IE版本<=7
            }
        }
        else if (isEdge) {
            // MS Eege浏览器不认为是IE浏览器。
            DCDomTools.__ieversion = -1; 
            //DCDomTools.__ieversion = 'edge';//edge
        }
        else if (isIE11) {
            DCDomTools.__ieversion = 11; //IE11  
        }
        else {
            DCDomTools.__ieversion = -1;//不是ie浏览器
        }
    }
    return DCDomTools.__ieversion;
};

//@method 判断是否为IE7或者更低版本
DCDomTools.IsIE7 = function () {
    if (DCDomTools.__isie7 == null) {
        DCDomTools.__isie7 = false;
        if (navigator && navigator.appName == "Microsoft Internet Explorer") {
            var ver = navigator.appVersion;
            if (ver != null) {
                if (ver.indexOf("MSIE 8.") >= 0
                    || ver.indexOf("MSIE 7.") >= 0
                    || ver.indexOf("MSIE 6.") >= 0) {
                    DCDomTools.__isie7 = true;
                }
            }
        }
    }
    return DCDomTools.__isie7;
};

function RegisterCORSCallback(win, name, func) {
    if (win == null) {
        win = window;
    }
    if (win.CORSCallbacks == null) {
        win.CORSCallbacks = new Array();
    }
    //防止linux下跨域错误
    var imgValue = undefined
    win.onmessage = function (e) {
        e = e || win.event;
        win.CORSCallSender = e.source;
        var data = e.data;
        if(data != null && data.imgValue != null){
            imgValue = data.imgValue
        }
        if (data != null && data.length > 11) {
            if (data.substr(0, 11) == "DCCallback:") {
                data = data.substr(11);
                var index = data.indexOf("$");
                var funcName = data;
                var parameter = null;
                if (index > 0) {
                    funcName = data.substr(0, index);
                    parameter = data.substr(index + 1);
                }
                for (var iCount = 0; iCount < win.CORSCallbacks.length; iCount++) {
                    var info = win.CORSCallbacks[iCount];
                    if (info.name == funcName) {
                        info.func.call(win,parameter,imgValue);
                        break;
                    }
                }
            }
        }
    };
    for (var iCount = 0; iCount < win.CORSCallbacks.length; iCount++) {
        var info = win.CORSCallbacks[iCount];
        if (info.name == name) {
            info.func = func;
            return;
        }
    }
    var newInfo = new Object();
    newInfo.name = name;
    newInfo.func = func;
    win.CORSCallbacks.push(newInfo);    
};

function ExeCORSCallback(name, parameter) {
    if (win.CORSCallSender != null) {
        win.CORSCallSender.postMessage("DCCallback:" + name + "$" + parameter);
        return true;
    }
    return false;
};

//@method 设置跨域远程调用
DCDomTools.SetRPCCallback = function (win) {
    if (window.dcrpc == null) {
        window.dcrpc = new Array();
        window.addEventListener("message", function (e) {
            e = e || window.event;
            var txt = e.data;
            if (txt != null && txt.length > 0) {
                var callName = txt;
                var parameter = null;
                var index = txt.indexOf("*");
                if (index > 0) {
                    callName = txt.substr(0, index);
                    parameter = txt.substr(index + 1);
                }
                for (var iCount = 0; iCount < window.dcrpc.length; iCount++) {
                    var item = window.dcrpc[iCount];
                    if (item.win == e.source) {
                        var func = item[callName];
                        if (typeof (func) == "function") {
                            func(parameter);
                        }
                        break;
                    }
                }//for
            }
        });
    }
    for (var iCount = 0; iCount < window.dcrpc.length; iCount++) {
        var item = window.dcrpc[iCount];
        if (item.win == win) {
            function temp9998() {
                win.postMessage("dcrpc20190320", "*");
                if(timer1){
                    clearTimeout(timer1);
                    timer1 = null;
                }
            }
            var timer1 = window.setTimeout(temp9998, 2000);
            return item;
        }
    }//for

    var info = new Object();
    info.win = win;
    window.dcrpc.push(info);
    function temp999() {
        info.innerSended = true;
        win.postMessage("dcrpc20190320", "*");
        if(timer2){
            clearTimeout(timer2);
            timer2 = null;
        }
    }
    var timer2 = window.setTimeout(temp999, 2000);

    info.RPCCall = function (name, parameter, delay) {
        if (typeof (delay) == "number" && delay > 0) {
            var funcssss = function () {
                info.win.postMessage(name + "*" + parameter);
                if(timer3){
                    clearTimeout(timer3);
                    timer3 = null;
                }
            }
            var timer3 = window.setTimeout(funcssss, delay);
        }
        else {
            info.win.postMessage(name + "*" + parameter);
        }
    };
    return info;
};

// 判断是否为一个JSON格式的字符串
DCDomTools.isJsonText = function (txt) {
    if (txt == null || txt.length == 0) {
        return false;
    }
    if (txt.substr(0, 1) == "{" && txt.substr(txt.length - 1) == "}") {
        return true;
    }
    if (txt.substr(0, 1) == "[" && txt.substr(txt.length - 1) == "]") {
        return true;
    }
    return false;
};

// 修正事件参数对象
DCDomTools.FixEventObject = function (eventObject) {
    if (eventObject == null && window.event) {
        eventObject = window.event;
    }
    if (eventObject != null && eventObject.originalEvent) {
        eventObject = eventObject.originalEvent;
    }
    return eventObject;
};

// 冒泡调用内容改变方法
DCDomTools.BubbleRaiseChanged = function (element) {
    if (element == null) {
        var sel = this.getSelection();
        if (sel != null) {
            element = sel.focusNode;
        }
    }
    var p = element;
    while (p != null) {
        if (typeof (p.RaiseChanged) == "function") {
            p.RaiseChanged();
        }
        p = p.parentNode;
    }
};

// 获得文档中所有的全局样式清单。
DCDomTools.GetAllStyleSheet = function () {
    var result = new Object();
    if (document.styleSheets) {
        var len = document.styleSheets.length;
        for (var iCount = 0; iCount < len; iCount++) {
            var sheet = document.styleSheets[iCount];
            // 20200305 xuyiming 解决DCWRITER-3055
            try {
                var rules = sheet.rules || sheet.cssRules;
            }
            catch (err) {
                var rules = null;
            }
            if (rules) {
                var len2 = rules.length;
                for (var iCount2 = 0; iCount2 < len2; iCount2++) {
                    var rule = rules[iCount2];
                    if (rule.style) {
                        result[rule.selectorText] = rule.style;
                    }
                }
            }
        }
        return result;
    }

    var nodes = document.getElementsByTagName("STYLE");
    var len = nodes.length;
    for (var iCount = 0; iCount < len; iCount++) {
        var node = nodes[iCount];
        if (node.sheet && node.sheet.rules) {
            var rules = node.sheet.rules;
            var len2 = rules.length;
            for (var iCount2 = 0; iCount2 < len2; iCount2++) {
                var rule = rules[iCount2];
                if (rule.style) {
                    result[rule.selectorText] = rule.style;
                }
            }
        }
    }
    return result;
};

// @method 获得2个节点中所有的子孙节点
// @param startNode 起始节点
// @param endNode 结束节点
// @excludeReadonlyNode 是否过滤掉内容只读(无法手动编辑)的节点。
DCDomTools.GetAllNodes = function (startNode, endNode, excludeReadonlyNode) {
    if (startNode != null && endNode != null) {
        var handled = false;
        var p1 = startNode;
        while (p1 != null) {
            var p2 = endNode;
            while (p2 != null) {
                if (p1 == p2) {
                    break;
                }
                if (p1.parentNode == p2.parentNode) {
                    // 起始节点和结束节点具有相同的父节点。
                    while (p2 != null) {
                        if (p1 == p2) {
                            // 结束节点的后续节点出现了起始节点，则进行互换。
                            var temp = startNode;
                            startNode = endNode;
                            endNode = temp;
                            handled = true;
                            break;
                        }
                        p2 = p2.nextSibling;
                    }//while
                    break;
                }
                p2 = p2.parentNode;
            }
            if (handled == true) {
                break;
            }
            p1 = p1.parentNode;
        }
    }
    var result = new Array();

    var nextNode = startNode;
    while (nextNode != null) {
        if (excludeReadonlyNode == true
            && nextNode.isContentEditable == false) {

        }
        else {
            result.push(nextNode);
        }
        if (nextNode == endNode) {
            // 遇到结束节点，则退出
            break;
        }
        // 获得下一个节点
        if (nextNode.firstChild != null) {
            // 获得第一个子节点
            nextNode = nextNode.firstChild;
        }
        else if (nextNode.nextSibling != null) {
            // 获得下一个平级节点
            nextNode = nextNode.nextSibling;
        }
        else {
            // 获得上级节点的下一个平级节点
            nextNode = nextNode.parentNode;
            while (nextNode != null) {
                if (nextNode.nextSibling != null) {
                    nextNode = nextNode.nextSibling;
                    break;
                }
                nextNode = nextNode.parentNode;
            }
        }
        //nextNode = GetNextNode(nextNode);
    }//while
    return result;
};

DCDomTools.hasAttriubte = function (element, attributeName) {
    if (element == null) {
        return false;
    }
    if (attributeName == null || attributeName.length == 0) {
        return false;
    }
    attributeName = attributeName.toLocaleString();
    if (element.hasAttribute) {
        return element.hasAttribute(attributeName);
    }
    if (element.attributes) {
        for (var iCount = 0; iCount < element.attributes.length; iCount++) {
            var attr = element.attributes[iCount];
            if (attr.nodeName == attributeName) {
                return true;
            }
        }
    }
    return false;
};

DCDomTools.ParseJSON = function (strJson) {
    if (typeof (JSON) == "undefined") {
        var v2 = window.eval("(" + strJson + ")");
        return v2;
    }
    var v = JSON.parse(strJson);
    return v;
};

// 动态判断并加载JQUERY库
DCDomTools.LoadJQuery = function (rootElement) {
    var input = DCDomTools.GetSettingsElement(rootElement);
    if( input != null )
    {
        var url = input.getAttribute("jqueryurl");
        if( url != null && url.length >0 )
        {
            var oScript = document.createElement("script");
            oScript.type = "text\/javascript";
            oScript.setAttribute("language", "javascript");
            oScript.setAttribute("async", "false");
            document.head.appendChild(oScript);
            oScript.src = url;
            return true;
        }
        return false;
    }
};
DCDomTools.GetSettingsElement = function ( rootElement ) {
    if( rootElement != null && rootElement.getAttribute && rootElement.getAttribute("dctype") == "WebWriterControl")
    {
        var input = document.getElementById(rootElement.id + "_Settings");
        return input;
    }
    return null;
};

// 模拟键盘事件
DCDomTools.fireKeyEvent = function (element , evtType, keyCode , ctrlKey ,altKey , shiftKey ) {
    var evtObj;
    if (document.createEvent) {
        if (window.KeyEvent) {//firefox 浏览器下模拟事件
            evtObj = document.createEvent('KeyEvents');
            evtObj.initKeyEvent(evtType, true, true, window, ctrlKey, altKey, shiftKey, false, keyCode, 0);
        } else {//chrome 浏览器下模拟事件
            evtObj = document.createEvent('UIEvents');
            evtObj.initUIEvent(evtType, true, true, window, 1);

            delete evtObj.keyCode;
            if (typeof evtObj.keyCode === "undefined") {//为了模拟keycode
                Object.defineProperty(evtObj, "keyCode", { value: keyCode });
            } else {
                evtObj.key = String.fromCharCode(keyCode);
            }

            if (typeof evtObj.ctrlKey === 'undefined') {//为了模拟ctrl键
                Object.defineProperty(evtObj, "ctrlKey", { value: ctrlKey });
                Object.defineProperty(evtObj, "shiftKey", { value: shiftKey });
                Object.defineProperty(evtObj, "altKey", { value: altKey });
            } else {
                evtObj.ctrlKey = true;
            }
        }
        element.dispatchEvent(evtObj);

    } else if (document.createEventObject) {//IE 浏览器下模拟事件
        evtObj = document.createEventObject();
        evtObj.keyCode = keyCode
        element.fireEvent('on' + evtType, evtObj);
    }
};

// 获得文档中同一组的单选框复选框文档元素
DCDomTools.GetCheckRadioBoxElementsByName = function (srcElement) {
    var result = new Array();
    //wyc20220107：修复不同子文档中同组名单选多选勾选互相影响的问题
    var ownersubdoc = DCDomTools.GetElementOwnerSubDocument(srcElement);
    var parent = ownersubdoc === null ? document : ownersubdoc;
    ////////////////////////////////////////////////////////////////
    var nodes = parent.getElementsByTagName("INPUT");
    for (var iCount = 0; iCount < nodes.length; iCount++) {
        var node = nodes[iCount];
        if (node.type == srcElement.type && node.name == srcElement.name
            && node.name !== undefined
            && node.name !== null
            && node.name !== "") {
            result.push(node);
        }
    }
    if (result.length == 0) {
        result.push(srcElement);//如果没设置name则包含自身
    }
    return result;
};


// 判断指定的元素是否处于鼠标拖拽滚动的操作状态
DCDomTools.IsMouseDragScrollMode = function (rootElement) {
    if (rootElement == null) {
        return false;
    }
    return rootElement.flagMouseDragScroll == true;
};

// 设置鼠标拖拽滚动的操作模式
DCDomTools.setMouseDragScrollMode = function (rootElement, setValue) {
    if (rootElement == null) {
        return false;
    }
    if (setValue == false && typeof (rootElement.flagMouseDragScroll) == "undefined") {
        return false;
    }
    if (rootElement.flagMouseDragScroll == setValue) {
        // 模式没有发生改变，无需再设置
        return false;
    }
    rootElement.flagMouseDragScroll = setValue;
    if (setValue == true) {
        // 进入拖拽滚动模式
        // 备份已有的鼠标处理事件
        rootElement.backDCMouseDown = rootElement.onmousedown;
        rootElement.backDCMouseMove = rootElement.onmousemove;
        rootElement.backDCMouseUp = rootElement.onmouseup;
        rootElement.backDCClick = rootElement.onclick;
        rootElement.backDCDBLClick = rootElement.ondblclick;
        rootElement.backDCCursor = rootElement.style.cursor;
        rootElement.fixForDragScrollX = 0;
        rootElement.fixForDragScrollY = 0;
        rootElement.dcMouseDownFlag = false;
        rootElement.style.cursor = "default";
        rootElement.onmousedown = function (eventObject) {
            if (eventObject == null) {
                eventObject = window.event;
            }
            if (eventObject == null) {
                return;
            }

            var srcElement = this;
            if (srcElement.componentFromPoint) {
                var cmp = srcElement.componentFromPoint(eventObject.clientX, eventObject.clientY);
                if (cmp != null && cmp.length > 0 && cmp != "outside") {
                    // 不是在客户区中按下鼠标按键的，则退出
                    return;
                }
            }
            // 清空选择状态
            var sel = window.getSelection ? window.getSelection() : document.selection;
            if (sel != null && sel.removeAllRanges) {
                sel.removeAllRanges();
            }
            else if (sel != null && sel.clear) {
                sel.clear();
            }
            if (eventObject.button == 0 || eventObject.buttonID == 0) {
                // 鼠标左键按下，开始拖拽滚动
                srcElement.style.cursor = "all-scroll";
                srcElement.dcLastClientX = eventObject.clientX;
                srcElement.dcLastClientY = eventObject.clientY;
                srcElement.dcMouseDownFlag = true;
                DCDomTools.completeEvent(eventObject);
                if (srcElement.setCapture) {
                    srcElement.setCapture(true);
                }
            }
        };

        rootElement.onmousemove = function (eventObject) {
            if (eventObject == null) {
                eventObject = window.event;
            }
            if (eventObject == null) {
                return;
            }
            var srcElement = this;
            if (srcElement.dcMouseDownFlag == true) {
                // 处于拖拽模式
                var sx = eventObject.clientX - srcElement.dcLastClientX;
                var sy = eventObject.clientY - srcElement.dcLastClientY;
                srcElement.dcLastClientX = eventObject.clientX;
                srcElement.dcLastClientY = eventObject.clientY;
                srcElement.scrollLeft -= sx;
                srcElement.scrollTop -= sy;
                if (srcElement.setCapture) {
                    srcElement.setCapture(true);
                }
            }
            DCDomTools.completeEvent(eventObject);
        };

        rootElement.onmouseup = function (eventObject) {
            if (eventObject == null) {
                eventObject = window.event;
            }
            if (eventObject == null) {
                return;
            }
            var srcElement = this;
            if (srcElement.dcMouseDownFlag == true) {
                srcElement.style.cursor = "default";
                srcElement.dcMouseDownFlag = false;
                if (srcElement.releaseCapture) {
                    srcElement.releaseCapture();
                }
                DCDomTools.completeEvent(eventObject);
            }
        };

        rootElement.onclick = function (eventObject) {
            if (eventObject == null) {
                eventObject = window.event;
            }
            if (eventObject != null) {
                DCDomTools.completeEvent(eventObject);
            }
        };
        rootElement.ondblclick = function (eventObject) {
            if (eventObject == null) {
                eventObject = window.event;
            }
            if (eventObject != null) {
                DCDomTools.completeEvent(eventObject);
            }
        };
    }
    else {
        // 退出拖拽滚动模式
        // 还原鼠标处理事件
        rootElement.fixForDragScrollX = 0;
        rootElement.fixForDragScrollY = 0;
        rootElement.onmousedown = rootElement.backDCMouseDown;
        rootElement.onmousemove = rootElement.backDCMouseMove;
        rootElement.onmouseup = rootElement.backDCMouseUp;
        rootElement.onclick = rootElement.backDCClick;
        rootElement.ondblclick = rootElement.backDCDBLClick;
        rootElement.dcMouseDownFlag = false;
        rootElement.style.cursor = rootElement.backDCCursor;
        rootElement.backDCMouseDown = null;
        rootElement.backDCMouseMove = null;
        rootElement.backDCMouseUp = null;
        rootElement.backDCCursor = null;
        rootElement.backDCClick = null;
        rootElement.backDCDBLClick = null;
    }
    return true;
};




// 删除CSS中的属性
// 参数 rootNode:要操作的节点
//      deeply:是否处理子孙节点
DCDomTools.removeCssAttribute = function (rootNode, attributeName, deeply) {
    if (rootNode.style) {
        if (rootNode.style.removeAttribute) {
            rootNode.style.removeAttribute(attributeName);
        }
        else if (rootNode.style.removeProperty) {
            rootNode.style.removeProperty(attributeName);
        }
        if (deeply == true && rootNode.childNodes) {
            var nodes = rootNode.childNodes;
            for (var iCount = 0; iCount < nodes.length; iCount++) {
                this.removeCssAttribute(nodes[iCount], attributeName, true);
            }
        }
    }
};


// 删除子节点中的CSS中的属性
// 参数 rootNode:要操作的节点
//      deeply:是否处理子孙节点
DCDomTools.removeChildNodesCssAttribute = function (rootNode, attributeName, deeply) {
    if (rootNode != null && rootNode.childNodes) {
        var nodes = rootNode.childNodes;
        for (var iCount = 0; iCount < nodes.length; iCount++) {
            this.removeCssAttribute(nodes[iCount], attributeName, deeply);
        }
    }
};

DCDomTools.isDocumentFocused = function (doc) {
    if (doc == null) {
        doc = document;
    }

};

// 判断节点是否在文档碎片中
DCDomTools.isInDocumentFragment = function (node) {
    if (node == null) {
        return false;
    }
    while (true) {
        if (node == null || node.nodeName == "#document-fragment") {
            return true;
        }
        if (node.nodeName == "BODY" || node.nodeName == "#document") {
            return true;
        }
        node = node.parentNode;
    }
    return false;
};

// 判断节点是否在可编辑区域
DCDomTools.isContentEditable = function (node) {
    while (node != null) {
        if (node.isContentEditable == false) {
            return false;
        }
        if (node.isContentEditable == true) {
            return true;
        }
        node = node.parentNode;
    }
    return false;
};

// 将数据转换为布尔值，若转换失败则返回默认值
DCDomTools.toBoolean = function (v, defaultValue) {
    if (v == null) {
        return defaultValue;
    }
    if (typeof (v) == "boolean") {
        return v;
    }
    if (typeof (v) != "string") {
        v = v.toString();
    }
    v = v.toLowerCase();
    if (v == "true") {
        return true;
    }
    if (v == "false") {
        return false;
    }
    return defaultValue;
};

// 获得节点在数组中的序号
DCDomTools.indexInArray = function (array, element) {
    if (array == null || element == null) {
        return -1;
    }
    for (var iCount = 0; iCount < array.length; iCount++) {
        if (array[iCount] == element) {
            return iCount;
        }
    }
    return -1;
};

// 创建一个二维数组
DCDomTools.create2DArray = function (length1, length2) {
    var result = new Array(length1);
    for (var iCount = 0; iCount < length1; iCount++) {
        result[iCount] = new Array(length2);
    }
    return result;
};

//在指定节点后插入新的节点
DCDomTools.insertAfter = function (oldNode, newNode) {
    var p = oldNode.parentNode;
    if (p != null) {
        if (oldNode.nextSibling == null) {
            p.appendChild(newNode);
        }
        else {
            p.insertBefore(newNode, oldNode.nextSibling);
        }
    }
};

// 向数组插入一个新的元素，返回插入元素后的新的数组
DCDomTools.insertElementToArray = function (array, index, newElement) {
    if (array == null) {
        array = new Array();
        array.push(newElement);
        return array;
    }
    if (index <= 0) {
        return array.unshift(newElement);
    }
    if (index >= array.length) {
        array.push(newElement);
        return array;
    }
    var a1 = array.slice(0, index);
    var a2 = array.slice(index + 1, array.length);
    a1.push(newElement);
    return a1.concat(a2);
};

// 获得指定名称的父节点
DCDomTools.getParentSpecifyNodeName = function (node, nodeName) {
    while (node != null) {
        if (node.nodeName == nodeName) {
            return node;
        }
        node = node.parentNode;
    }
    return null;
};

// 选中文档区域
DCDomTools.selectContent = function (startNode, startIndex, endNode, endIndex) {
    if (startNode == null && endNode == null) {
        return;
    }
    var range = null;
    if (document.createRange) {
        range = document.createRange();
    }
    else if (document.body.createRange) {
        range = document.body.createRange();
    }
    if (range != null) {
        if (startNode != null && startIndex >= 0) {
            if (range.setStart) {
                range.setStart(startNode, startIndex);
            }
        }
        if (endNode != null && endIndex >= 0) {
            if (range.setEnd) {
                range.setEnd(endNode, endIndex);
            }
        }
        if (range.select) {
            range.select();
        }
        else {
            var sel = DCDomTools.getSelection();
            if (sel.removeAllRanges) {
                sel.removeAllRanges();
                sel.addRange(range);
            }
        }
    }
};

// 比较两个节点在整体DOM树中的位置对比信息
DCDomTools.compareDOMTreePosition = function (node1, node2) {
    if (node1 == node2) {
        return 0;
    }
    if (node1.parentNode == node2.parentNode) {
        var idx1 = this.GetNodeIndex(node1);
        var idx2 = this.GetNodeIndex(node2);
        if (idx1 > idx2) {
            return 1;
        }
        else {
            return -1;
        }
    }
    var idx1 = new Array();
    var node = node1;
    while (node != null && node.parentNode) {
        idx1.push(this.GetNodeIndex(node));
        node = node.parentNode;
    }//while
    var idx2 = new Array();
    node = node2;
    while (node != null && node.parentNode) {
        idx2.push(this.GetNodeIndex(node));
        node = node.parentNode;
    }//while
    for (var iCount = 0; iCount < idx1.length && iCount < idx2.length; iCount++) {
        var dx = idx1[iCount] - idx2[iCount];
        if (dx > 0) {
            return 1;
        }
        else if (dx < 0) {
            return -1;
        }
    } //for
    if (idx1.length < idx2.length) {
        return 1;
    }
    else {
        return 0;
    }
};

// 获得文档节点全局唯一编号
DCDomTools.getGlobalNodeID = function (node) {
    if (node == null) {
        return null;
    }
    if (node.uniqueID) {
        return node.uniqueID;
    }
    return null;
};

// 复制属性值
DCDomTools.copyAttributes = function (srcNode, descNode) {
    if (srcNode == null || descNode == null) {
    }
    if (srcNode.attributes && descNode.attributes) {
        for (var iCount = 0; iCount < srcNode.attributes.length; iCount++) {
            var attr = srcNode.attributes[iCount];
            descNode.setAttribute(attr.nodeName, attr.nodeValue);
        }
    }
};

/**
Emulates MSIE function range.moveToPoint(x,y) b
returning the selection node info corresponding
to the given x/y location.

@param x the point X coordinate
@param y the point Y coordinate
@return the node and offset in characters as 
{node,offsetInsideNode} (e.g. can be passed to range.setStart) 
*/
DCDomTools.getSelectionNodeInfo = function (x, y) {
    // Implementation note: range.setStart offset is
    // counted in number of child elements if any or
    // in characters if there is no childs. Since we
    // want to compute in number of chars, we need to
    // get the node which has no child.
    var startRange = null;
    if (document.createRange) {
        startRange = document.createRange();
    }
    else if (document.body.createRange) {
        startRange = document.body.createRange();
    }
    var elem = document.elementFromPoint(x, y);
    if (elem == null) {
        return null;
    }
    var startNode = (elem.childNodes.length > 0 ? elem.childNodes[0] : elem);
    if (startNode.nodeName != "#text" && startNode.childNodes.length == 0) {
        return { node: startNode, offsetInsideNode: 0 };
    }
    var startCharIndexCharacter = -1;
    do {
        startCharIndexCharacter++;
        startRange.setStart(startNode, startCharIndexCharacter);
        startRange.setEnd(startNode, startCharIndexCharacter + 1);
        var rangeRect = startRange.getBoundingClientRect();
        if (rangeRect.left <= x && x <= rangeRect.right && rangeRect.top <= y && y <= rangeRect.bottom) {
            break;
        }
    } while (startCharIndexCharacter < startNode.length - 1);
    if (startCharIndexCharacter > 0) {
        var i = 0;
    }
    return { node: startNode, offsetInsideNode: startCharIndexCharacter };
};

DCDomTools.moveCaretToPoint = function (x, y) {
    var sel = DCDomTools.getSelection();
    var range = this.createSelectionRange();
    if (range.moveToPoint) {
        range.moveToPoint(x, y);
        return;
    }
    var info = this.getSelectionNodeInfo(x, y);
    if (info != null && info.node != null) {
        if (info.node.parentNode != null && info.node.parentNode.isContentEditable == true) {
            DCDomTools.MoveCaretToIndex(info.node, info.offsetInsideNode);
        }
    }
    return true;

};

// 获得表示选中区域的信息对象
DCDomTools.createSelectionRange = function () {
    var sel = DCDomTools.getSelection();
    var range = null;
    if (sel.getRangeAt && sel.rangeCount > 0) {
        range = sel.getRangeAt(0);
    }
    else if (sel.createRange) {
        range = sel.createRange();
    }
    else if (document.createRange) {
        range = document.createRange();
        sel.addRange(range);
    }
    else if (document.body.createRange) {
        range = document.body.createRange();
        sel.addRange(range);
    }
    return range;
};

// 快速判断是否为隐藏元素
DCDomTools.isHiddenElementFast = function (element) {
    if (element == null) {
        return true;
    }
    if (element.nodeName == "#text") {
        element = element.parentNode;
    }
    if (element.style.display == "none" || element.style.visibility == "hidden") {
        return true;
    }
    return false;
};

// 判断是否为隐藏的元素
DCDomTools.isHiddenElement = function (element) {
    if (element == null) {
        return true;
    }
    if (element.nodeName == "#text") {
        element = element.parentNode;
    }

    //    if (element.parentNode == null || element.parentNode.nodeName == "#document-fragment") {
    //        // 属于文档片段
    //        return false;
    //    }
    if (element.getClientRects) {
        var rects = element.getClientRects();
        if (rects == null || rects.length == 0) {
            return true;
        }
    }
    while (element != null && element.nodeName != "BODY") {
        if (element.style.display == "none" || element.style.visibility == "hidden") {
            return true;
        }
        element = element.parentNode;
    }
    return false;
};

// 判断元素是否在可视区域
DCDomTools.IsInVisibleArea = function (element, fixedHeaderHeight) {
    var left = element.offsetLeft;
    var top = element.offsetTop;
    var p = element.offsetParent;
    while (p != null) {
        if (p.clientHeight < p.scrollHeight) {
            if (top < p.scrollTop || top + element.offsetHeight > p.scrollTop + p.clientHeight) {
                return false;
            }
            if (left < p.scrollLeft || left + 10 > p.scrollLeft + p.clientWidth) {
                return false;
            }
        }
        if (p.nodeName == "BODY") {
            if (fixedHeaderHeight == null) {
                fixedHeaderHeight = 0;
            }
            if (isNaN(fixedHeaderHeight) == false && fixedHeaderHeight > 0) {
                if (top < p.scrollTop + fixedHeaderHeight || top + element.offsetHeight > p.scrollTop + fixedHeaderHeight + p.clientHeight) {
                    return false;
                }
            }
            break;
        }
        left = left + p.offsetLeft;
        top = top + p.offsetTop;
        p = p.offsetParent;
    }
    return true;
};

// 完成事件，事件不再后续分发,也不执行默认行为。
DCDomTools.completeEvent = function (eventObject) {
    if (eventObject == null) {
        if (window.event) {
            eventObject = window.event;
        }
    }
    if (eventObject != null) {
        eventObject.cancelBubble = true;
        if (eventObject.stopPropagation) {
            eventObject.stopPropagation();
        }
        if (eventObject.stopImmediatePropagation) {
            eventObject.stopImmediatePropagation();
        }
        if (eventObject.preventDefault) {
            eventObject.preventDefault();
        }
        eventObject.returnValue = false;
    }
};

//创建并添加一个CSS样式表引用
DCDomTools.createCssLinkElement = function (doc, url) {
    if (doc == null) {
        doc = document;
    }
    var head = doc.getElementsByTagName("head");
    if (head != null && head.length > 0) {
        var link = doc.createElement('link');
        link.type = 'text/css';
        link.rel = 'stylesheet';
        link.href = url;
        head[0].appendChild(link);
        return link;
    }
    else {
        return null;
    }
};

// 插入子节点
DCDomTools.insertChildNode = function (parentNode, index, childNode) {
    if (parentNode == null) {
        return false;
    }
    if (childNode == null) {
        return false;
    }
    if (parentNode.nodeName == "#text"
            || parentNode.nodeName == "INPUT"
            || parentNode.nodeName == "TEXTAREA") {
        return false;
    }
    if (index < parentNode.childNodes.length) {
        var node = null;
        if (index <= 0) {
            node = parentNode.firstChild;
        }
        else {
            node = parentNode.childNodes[index];
        }
        parentNode.insertBefore(childNode, node);
    }
    else {
        parentNode.appendChild(childNode);
    }
};

// 删除HTML中的<script>元素
DCDomTools.removeScriptElement = function (htmlString) {
    if (htmlString == null || htmlString.length == 0) {
        return htmlString;
    }
    htmlString = this.removeSpecifyElement(htmlString, "script");
    htmlString = this.removeSpecifyElement(htmlString, "SCRIPT");
    return htmlString;
};

// 删除HTML中的指定名称的元素
DCDomTools.removeSpecifyElement = function (htmlString , tagName ) {
    if (htmlString == null || htmlString.length == 0) {
        return htmlString;
    }
    while (true) {
        var match = false;
        var index1 = htmlString.indexOf("<" + tagName );
        if (index1 >= 0) {
            var index2 = htmlString.indexOf("</" + tagName + ">", index1);
            if (index2 > 0) {
                var txt = htmlString.substr(0, index1);
                var txt2 = htmlString.substr(index2 + 9);
                htmlString = txt + txt2;
                match = true;
            }
        }
        if (match == false) {
            break;
        }
    }
    return htmlString;
};

// 获得最顶层的窗体对象
DCDomTools.GetTopWindow = function () {
    var tw = window;
    while (true) {
        var pw = null;
        if (tw.frameElement) {
            pw = tw.frameElement.ownerDocument.parentWindow;
        }
        if (pw != null) {
            tw = pw;
        }
        else {
            break;
        }
    }
    return tw;
};

DCDomTools.trim = function (str) {
    str = str || '';
    return str.replace(/^\s|\s$/g, '').replace(/\s+/g, ' ');
};


// 从一个HTML字符串中获得标题值
DCDomTools.GetTitleFromHtml = function (strHtml) {
    if (strHtml == null || strHtml.length == 0) {
        return null;
    }
    var index1 = strHtml.indexOf("<title>");
    if (index1 < 0) {
        index1 = strHtml.indexOf("<TITLE>");
    }
    if (index1 < 0) {
        return null;
    }
    var index2 = strHtml.indexOf("</title>");
    if (index2 < 0) {
        index2 = strHtml.indexOf("</TITLE>");
    }
    if (index2 < index1) {
        return null;
    }
    var result = strHtml.substring(index1 + 7, index2);
    result = DCDomTools.trim(result);
    return result;
};

DCDomTools.GetDocumentClientHeight = function () {
    var v1 = document.body.clientHeight;
    var v2 = document.documentElement.clientHeight;
    if (window.innerHeight)
    {
        v2 = window.innerHeight;
    }
    if (v1 > 0 && v2 > 0) {
        return Math.max(v1, v2);
    }
    if (v1 > 0) {
        return v1;
    }
    return v2;
};

DCDomTools.GetDocumentClientWidth = function () {
    var v1 = document.body.clientWidth;
    var v2 = document.documentElement.clientWidth;
    if (window.innerWidth)
    {
        v2 = window.innerWidth;
    }
    if (v1 > 0 && v2 > 0) {
        return Math.max(v1, v2);
    }
    if (v1 > 0) {
        return v1;
    }
};


DCDomTools.hasClass = function (elem, cls) {
    elem = elem || {};
    return new RegExp('\\b' + cls + '\\b').test(elem.className);
};

DCDomTools.addClass = function (elem, cls) {
    elem = elem || {};
    DCDomTools.hasClass(elem, cls) || (elem.className += ' ' + cls);
    //elem.className = elem.className;
    return this;
};

DCDomTools.removeClass = function (elem, cls) {
    elem = elem || {};
    if (DCDomTools.hasClass(elem, cls)) {
        var reg = new RegExp('\\b' + cls + '\\b');
        elem.className = elem.className.replace(reg, '');
    }
    return this;
};

//清除css属性
DCDomTools.removeCssAttr = function (elem, attr) {
    var s = elem.style;
    if (s != null) {
        if (s.removeProperty) {
            s.removeProperty(attr);
        } else {
            s.removeAttribute(attr);
        }
    }
};

/**
 * 解析后台返回的数据
 * "true$dcsuccesssplit$$dcmessageplit$$dcviewstatesplit$DocumentPageCount:1"
 * @param responseText 后台返回的数据
 * @return 返回键值对的数组
 */
DCDomTools.AnalyseResponseText = function (responseText) {
    if (!responseText || typeof (responseText) != "string") {
        return responseText;
    }
    if (responseText) {
        var pattern = /\$dcsuccesssplit\$|\$dcmessageplit\$|\$dcviewstatesplit\$/g;
        var result = responseText.split(pattern);
        if (result.length == 4) {
            var obj = {
                success: result[0],
                message: result[1],
                viewstate: result[2],
                data: result[3]
            };
            return obj;
        }
    }
    return responseText;
}

/**
* @param text 键值对形式的字符串
* @return 返回键值对的数组
*/
DCDomTools.ParseAttributeString = function (text , callBack ) {
    if (text == null || text == undefined || text == '') {
        return null;
    }
    var resultArray = new Array()//结果数组

    while (text.length > 0) {
        var newName = ""; //键
        var newValue = ""; //值
        var index = text.indexOf(":");
        if (index > 0) {
            newName = text.substring(0, index);
            text = text.substring(index + 1);
            if (text.slice(0, 1) == "\'") {//值里面以单引号开头,slice效率比indexOf更高
                var index2 = text.indexOf("\'", 1);
                if (index2 < 0) {
                    index2 = text.indexOf(";");
                }
                if (index2 >= 0) {
                    newValue = text.substring(1, index2);
                    text = text.substring(index2 + 1);
                    if (text.slice(0, 1) == "\'") {
                        text = text.substring(1);
                    }
                }
                else {
                    newValue = text.substring(1);
                    text = "";
                }
            } //if
            else {//值里面不以单引号开头
                var index3 = text.indexOf(";");
                if (index3 >= 0) {
                    newValue = text.substring(0, index3);
                    text = text.substring(index3 + 1);
                }
                else {
                    newValue = text;
                    text = "";
                }
            }
        }
        else {
            newName = text.trim();
            text = "";
        }
        //alert(newName);
        //alert(newValue);
        if (callBack == null) {
            resultArray.push(newName);
            resultArray.push(newValue);
        }
        else {
            callBack(newName, newValue);
        }
    }
    return resultArray;
};

DCDomTools.AddProperty = function (obj, propertyName, funcGetter, funcSetter) {
    if (obj == null) {
        return false;
    }
    if (propertyName == null || propertyName.length == 0) {
        return false;
    }
    if (funcGetter == null && funcSetter == null) {
        return false;
    }
    if (Object.defineProperty) {
        Object.defineProperty(obj, propertyName, {
            set: funcSetter,
            get: funcGetter
        });
    }
    else {
        try {
            if (funcGetter != null) {
                obj.prototype.__defineGetter__(propertyName, funcGetter);
            }
            if (funcGetter != null) {
                obj.prototype.__defineSetter__(propertyName, funcSetter);
            }
        } catch (ex) {
            
        }
    }
};

//**************************************************************************************************************
// 获得毫秒为单位的时刻数
DCDomTools.GetDateMillisecondsTick = function (dtm) {
    var v = dtm.getFullYear();
    v = v * 12 + dtm.getMonth();
    v = v * 30 + dtm.getDate();
    v = v * 24 + dtm.getHours();
    v = v * 60 + dtm.getMinutes();
    v = v * 60 + dtm.getSeconds();
    v = v * 1000 + dtm.getMilliseconds();
    return v;
};

//**************************************************************************************************************
// 获得毫秒为单位的当前时刻数
DCDomTools.GetCurrentDateMillisecondsTick = function () {
    return DCDomTools.GetDateMillisecondsTick(new Date());
};

// 获得指定元素后面的若干个后续元素。
DCDomTools.GetNextNodes = function (startNode, maxCount) {
    if (startNode == null) {
        return null;
    }
    if (isNaN(maxCount)) {
        maxCount = 0;
    }
    var result = new Array();
    var nextNode = startNode;
    while (nextNode != null) {
        DCDomTools.GetSubNodes(nextNode, maxCount, result);
        nextNode = DCDomTools.GetAbsNextSibling(nextNode);
        if (nextNode == null) {
            break;
        }
        else {
            result.push(nextNode);
            if (maxCount >= 0 && result.length >= maxCount) {
                return result;
            }
        }
    }
    function InnerGetNextNodes(node, list, maxCount) {
        for (var iCount = 0; iCount < node.childNodes.length; iCount++) {
            var subNode = node.childNodes[iCount];
            list.push(subNode);
            if (maxCount >= 0 && list.length >= maxCount) {
                break;
            }
            if (subNode.nodeType == 1) {
                InnerGetNextNodes(subNode, list, maxCount);
                if (maxCount >= 0 && list.length >= maxCount) {
                    break;
                }
            }
        }
    }
    InnerGetNextNodes(startNode, result, maxCount);
    return result;
};

// 获得指定元素所有的子孙元素,按照前序递归来获取。获得的列表不包含根元素本身。
DCDomTools.GetSubNodes = function (rootNode, maxCount , resultList ) {
    if (rootNode == null) {
        return ;
    }
    var result = null;
    if (resultList instanceof Array) {
        result = resultList;
    }
    else {
        result = new Array();
    }
    var nextNode = rootNode;
    function InnerGetNextNodes(node, list, maxCount) {
        for (var iCount = 0; iCount < node.childNodes.length; iCount++) {
            var subNode = node.childNodes[iCount];
            list.push(subNode);
            if (maxCount >= 0 && list.length >= maxCount) {
                break;
            }
            if (subNode.nodeType == 1) {
                InnerGetNextNodes(subNode, list, maxCount);
                if (maxCount >= 0 && list.length >= maxCount) {
                    break;
                }
            }
        }
    }
    if (isNaN(maxCount)) {
        maxCount = 0;
    }
    InnerGetNextNodes(rootNode, result, maxCount);
    return result;
};


// 获得DOM树中的绝对坐标上的下一个元素,搜索时包含子孙元素
DCDomTools.GetAbsNextSiblingIncludeSubNode = function (element) {
    if (element == null) {
        return null;
    }
    var nextNode = element;
    while (nextNode != null) {
        // 这里有可能导致死循环，需要判断 firstChild != element
        if (nextNode.firstChild != null && nextNode.firstChild != element ) {
            return nextNode.firstChild;
        }
        if (nextNode.nextSibling == null) {
            nextNode = nextNode.parentNode;
        }
        else {
            return nextNode.nextSibling;
        }
    }
    return null;
};


// 获得DOM树中的绝对坐标上的下一个元素
DCDomTools.GetAbsNextSibling = function (element) {
    if (element == null) {
        return null;
    }
    var nextNode = element;
    while (nextNode != null ) {
        if (nextNode.nextSibling == null) {
            nextNode = nextNode.parentNode;
        }
        else {
            return nextNode.nextSibling;
        }
    }
    return null;
};

// 获得DOM树中的绝对坐标上的上一个元素
DCDomTools.GetAbspreviousSibling = function (element) {
    if (element == null) {
        return null;
    }
    var nextNode = element;
    while (true) {
        if (nextNode.previousSibling == null) {
            nextNode = nextNode.parentNode;
        }
        else {
            return nextNode.previousSibling;
        }
    }
    return null;
};

// 获得DOM树中的绝对坐标上的上一个元素。搜索时包含子孙节点。
DCDomTools.GetAbspreviousSiblingIncludeSubNodes = function (element) {
    if (element == null) {
        return null;
    }
    var nextNode = element;
    while (true) {
        if (nextNode.previousSibling == null) {
            nextNode = nextNode.parentNode;
        }
        else {
            var nextNode = nextNode.previousSibling;
            while (nextNode.lastChild != null) {
                nextNode = nextNode.lastChild;
            }
            return nextNode;
        }
    }
    return null;
};


// 获得文档元素可供排版的客户区的宽度
DCDomTools.GetClientWidth = function (element) {
    if (element == null) {
        return 0;
    }
    var w = element.clientWidth;
    var pl = element.style.paddingLeft;
    if (pl != null && pl.length > 0) {
        pl = pl.replace("px", "");
    }
    var pw = parseFloat(pl);
    if (isNaN(pw) == false) {
        w = w - pw;
    }
    var pr = element.style.paddingRight;
    if (pr != null && pr.length > 0) {
        pr = pr.replace("px", "");
    }
    pw = parseFloat(pr);
    if (isNaN(pw) == false) {
        w = w - pw;
    }
    return w;
};

// 清空编辑器重做、撤销操作信息
DCDomTools.ClearUndo = function () {
    if (document.queryCommandSupported == null || document.queryCommandSupported("ms-clearUndoStack") == true) {
        document.execCommand("ms-clearUndoStack", false, null);
    }
};

// 设置文档获得焦点
DCDomTools.FoucsDocument = function ( target ) {
    //    var sel = document.selection;
    //    var range = sel.createRange();
    if (target != null) {
        if (target.focus) {
            target.focus();
        }
        if (target.setActive) {
            target.setActive();
        }
        return;
    }
    var frame = window.frameElement;
    if (frame != null) {
        if (frame.focus) {
            frame.focus();
        }
        if (frame.setActive) {
            frame.setActive();
        }
    }
    if (window.setActive) {
        window.setActive();
    }
    if (window.focus) {
        window.focus();
    }
    if (document.focus) {
        document.focus();
    }
    if (document.setActive) {
        document.setActive();
    }
};

// 设置框架内文档获得焦点
DCDomTools.FoucsFrameContent = function (frameElement) {
    if (frameElement == null) {
        return;
    }
    if (frameElement.focus) {
        frameElement.focus();
    }
    if (frameElement.setActive) {
        frameElement.setActive();
    }
    var win = frameElement.contentWindow;
    if (win != null) {
        if (win.setActive) {
            win.setActive();
        }
        if (win.focus) {
            win.focus();
        }
        var doc = win.document;
        if (doc != null) {
            if (doc.focus) {
                doc.focus();
            }
            if (doc.setActive) {
                doc.setActive();
            }
        }
    }
};

//判断是否为纯文本节点
DCDomTools.IsTextNode = function (node) {
    if (node == null) {
        return false;
    }
    if (node.nodeName && node.nodeName == "#text") {
        return true;
    }
    return false;
};

//**************************************************************************************************************
// 获得节点在其父节点中的子节点序号
DCDomTools.GetNodeIndex = function (node) {
    if (node == null || node.parentNode == null) {
        return -1;
    }
    var nodes = node.parentNode.childNodes;
    for (var iCount = 0; iCount < nodes.length; iCount++) {
        if (nodes[iCount] == node) {
            return iCount;
        }
    }
    return -1;
};

//**************************************************************************************************************
// 获得子节点在父节点的节点集合中的序号
DCDomTools.IndexOfChildNode = function (parentNode, childNode) {
    if (parentNode == null || childNode == null) {
        return -1;
    }
    var nodes = parentNode.childNodes;
    for (var iCount = 0; iCount < nodes.length; iCount++) {
        if (nodes[iCount] == childNode) {
            return iCount;
        }
    }
    return -1;
};

//移动并替换所有的置属子元素
DCDomTools.ReplaceAllNodes = function (rootNode, childNodes ,copyMode) {
    if (rootNode != null) {
        while (rootNode.lastChild != null) {
            rootNode.removeChild(rootNode.lastChild);
        }
        if (childNodes != null) {
            var len = childNodes.length;
            for (var iCount = 0; iCount < len; iCount++) {
                if (copyMode == true) {
                    rootNode.appendChild(childNodes[iCount].cloneNode(true));
                }
                else {
                    rootNode.appendChild(childNodes[iCount]);
                }
            }
        }
        // 表示内容被修改
        $(rootNode).parents("body").data("isCanAnchorPage", true);
    }
};
//
// 删除指定元素的所有子元素
// 
DCDomTools.removeAllChilds = function (element) {
    if (element != null) {
        while (element.firstChild != null) {
            element.removeChild(element.firstChild);
        }
    }
};


//**************************************************************************************************************
// 获得两个节点共同的根节点
DCDomTools.GetSameRootNode = function (node1, node2) {
    if (node1 == node2) {
        return node1;
    }
    if (node1 == null || node2 == null) {
        return null;
    }
    var nodes1 = new Array();
    var p = node1;
    while (p != null) {
        nodes1.push(p);
        p = p.parentNode;
    }

    var nodes2 = new Array();
    p = node2;
    while (p != null) {
        nodes2.push(p);
        p = p.parentNode;
    }

    for (var iCount1 = 0 ; iCount1 < nodes1.length ; iCount1 ++ ) {
        var p1 = nodes1[iCount1];
        for (var iCount2 = 0; iCount2 < nodes2.length; iCount2++) {
            var p2 = nodes2[iCount2];
            if (p1 == p2) {
                return p1;
            }
        }
    }
    return null;
};

//*******************************************************************************
// 取消对指定元素的闪烁操作
DCDomTools.FlashElement = function (element) {
    if (DCDomTools.FlashInfos) {
        for (var iCount = 0; iCount < DCDomTools.FlashInfos.length; iCount++) {
            var obj = DCDomTools.FlashInfos[iCount];
            if (obj.element == element) {
                DCDomTools.FlashInfos.splice(iCount);
                break;
            }//if
        }//for
    }//if
};

//*******************************************************************************
// 闪烁指定文档元素
// 参数 element:文档元素对象
//      count:闪烁次数
//      borderColor:闪烁时使用的边框色
DCDomTools.FlashElement = function (element, count, borderColor) {
    if (element == null) {
        return false;
    }
    if (!element.getAttribute) {
        return false;
    }
    if (!DCDomTools.FlashInfos) {
        DCDomTools.FlashInfos = new Array();
    }
    for (var iCount = 0; iCount < DCDomTools.FlashInfos.length; iCount++) {
        var obj = DCDomTools.FlashInfos[iCount];
        if (obj.element == element) {
            obj.count = count;
            obj.color = borderColor;
            // 已经存在闪烁信息
            return;
        }
    }
    var obj2 = new Object();
    obj2.element = element;
    obj2.count = 5;
    obj2.color = borderColor;
    obj2.back = element.style.border;
    element.style.border = "1px solid " + borderColor;
    DCDomTools.FlashInfos.push(obj2);
    if (!DCDomTools.timerHandle) {
        DCDomTools.timerHandler = window.setTimeout("DCDomTools.InvokeFlashElement()", 300);
    }
    //alert("zzz");
};

DCDomTools.InvokeFlashElement = function () {
    if (DCDomTools.FlashInfos && DCDomTools.FlashInfos.length > 0) {
        for (var iCount = DCDomTools.FlashInfos.length - 1; iCount >= 0; iCount--) {
            var obj = DCDomTools.FlashInfos[iCount];
            obj.count--;
            if (obj.count <= 0) {
                // 闪烁结束
                obj.element.style.border = obj.back;
                DCDomTools.FlashInfos.splice(iCount);
                continue;
            }
            if ((obj.count % 2) == 0) {
                obj.element.style.border = "1px solid " + obj.color;
                //alert(obj.color);
            }
            else {
                obj.element.style.border = obj.back;
            }
        } //for
        if (DCDomTools.FlashInfos.length > 0) {
            DCDomTools.timerHandler = window.setTimeout(DCDomTools.InvokeFlashElement, 400);
        }
        else {
            DCDomTools.timerHandler = null;
        }
    }

};

// 根据URL加载JSON内容
// 参数：url:JSON访问地址；jsonName:数据名称
DCDomTools.LoadJsonByUrl = function (url) {
    if (url == null || url.length == 0) {
        return new Object();
    }
    var result = new Object();
    var settings = {url: url, async: false, dataType: "json" };
    DCDomTools.fixAjaxSettings(settings);
    $.ajax(settings ).done(function (data) {
        result = data;
    });
    return result;
};

//*****************************************************************************
// 使用XMLHTTP技术以GET方法获得一个页面内容,而且不采用异步模式，采用同步模式。
DCDomTools.GetContentByUrlNotAsync = function (url) {
    var result = "";
    var settings = {url: url, async: false };
    DCDomTools.fixAjaxSettings(settings);
    $.ajax(settings ).done(function (data, textStatus, jqXHR ) {
        result = data;
    });
    return result;
};

//*****************************************************************************
// 使用XMLHTTP技术以POST方法获得一个页面内容
DCDomTools.GetContentByUrl = function (url, promptError, readystatechangeCallback, parameter) {
    $.ajax(DCDomTools.fixAjaxSettings({url: url, async: true, type: "POST" })).done(function (data, textStatus, jqXHR) {
        if (textStatus == "success")
        {
            readystatechangeCallback(data, jqXHR.status == 200, parameter, jqXHR);
        }
        result = data;
    });
    return true;
};

// 使用XMLHTTP技术以POST方法获得一个页面内容,而且不采用异步模式，采用同步模式,并且有回调函数
DCDomTools.PostContentByUrlNotAsyncHasCallback = function (url, promptError, readystatechangeCallback, parameter) {
    var settings = {
        url: url,
        async: false,
        type: "POST"
    };
    $.ajax(DCDomTools.fixAjaxSettings(settings)).done(function (data, textStatus, jqXHR) {
        if (textStatus == "success")
        {
            readystatechangeCallback(data, jqXHR.status == 200, parameter, jqXHR);
        }
        result = data;
    });
    return true;
};

//*****************************************************************************
// 使用XMLHTTP技术以POST方法获得一个页面内容
DCDomTools.PostContentByUrl = function (url, promptError, readystatechangeCallback, parameter, content) {
    var settings = {
        url: url,
        async: true,
        data: content,
        method: "POST",
        type: "POST"
    };
    $.ajax(
        DCDomTools.fixAjaxSettings(settings)
    ).done(function (data, textStatus, jqXHR) {
        if (textStatus == "success") {
            readystatechangeCallback(data, jqXHR.status == 200, parameter, jqXHR);
        }
        result = data;
    });
    return true;
};

DCDomTools.getResponseText = function (responseText, jqXHR) {
    if (responseText != null) {
        if (typeof (responseText) == "object") {
            responseText = responseText.responseText;
        }
    }
    if (responseText == null && typeof (jqXHR) == "object") {
        responseText = jqXHR.responseText;
    }
    return responseText;
};

//使用XMLHTTP技术以POST方法获得一个页面内容并返回一个结果,而且不采用异步模式，采用同步模式。 张昊 2017-2-15 EMREDGE-28
DCDomTools.PostContentByUrlNotAsync = function (url, promptError, content, parseJson) {
    var result = false;
    var settings = {
        url: url,
        async: false,
        data: content,
        method: "POST",
        type: "POST"
    };
    $.ajax(
        DCDomTools.fixAjaxSettings(settings)
    ).always(
        function (data, textStatus, jqXHR) {
            data = DCDomTools.getResponseText(data, jqXHR);
            if (textStatus == "success") {
                var isJson = false;
                if (jqXHR.getResponseHeader) {
                    isJson = jqXHR.getResponseHeader("json") == "1";
                }
                if (isJson || parseJson == true) {
                    result = DCDomTools.ParseJSON(data);
                }
                else if (data == "true") {
                    result = true;
                }
                else if (data == "false") {
                    result = false;
                }
                else {
                    result = data;
                }
            }
            else {
                throw data;
                result = null;
            }
    });
    return result;
};

//**************************************************************************************************************
DCDomTools.showModalDialog = function (url, arguments, features) {
    var dtm1 = new Date();
    //alert(url);
    if (document.WriterControl) {
        var eventObject = new Object();
        eventObject.Message = url;
        eventObject.State = document.WriterControl.ErrorInfo.Error;
        document.WriterControl.MessageHandler(eventObject);
    }
    var result = null;
    if (window.showModalDialog) {
        result = window.showModalDialog(url, arguments, features);
    }
    else if (window.open) {
        result = window.open(url, null, features + ";modal=yes");
    }
    var dtm2 = new Date();
    // 比较两个时间差
    var tick = DCDomTools.GetDateMillisecondsTick(dtm2) - DCDomTools.GetDateMillisecondsTick(dtm1);
    if (tick < 500) {
        //alert("浏览器被设置为禁止弹出对话框了");
        if (document.WriterControl) {
            var eventObject = new Object();
            eventObject.Message = "浏览器被设置为禁止弹出对话框了";
            eventObject.State = document.WriterControl.ErrorInfo.Error;
            document.WriterControl.MessageHandler(eventObject);
        }
    }
    return result;
};

DCDomTools.addEventHandler = function (oTarget, sEventType, fnHandler) {
    if (oTarget == null || sEventType == null || fnHandler == null) {
        return;
    }
    // jquery版本兼容
    if ($(oTarget).bind) {
        $(oTarget).bind(sEventType, fnHandler);
    } else {
        $(oTarget).on(sEventType, fnHandler);
    }
};

DCDomTools.appendEventHandler = function (oTarget, sEventName, fnHandler) {
    if (oTarget == null || sEventType == null || fnHandler == null) {
        return;
    }
    $(oTarget).on(sEventType, fnHandler);
};


// 删除所有的子节点
DCDomTools.RemoveAllChildNodes = function (element) {
    if (element != null) {
        while (element.firstChild != null) {
            element.removeChild(element.firstChild);
        }
    }
};

DCDomTools.setActive = function (element) {
    //alert(element.focus);
    if (element == null) {
        return;
    }
    var flag = false;
    if (element.focus) {
        element.focus();
        flag = true;
    }
    if (element.setActive) {
        try {
            element.setActive();
            flag = true;
        }
        catch (ext) {
        }
    }
    if (flag) {
        return;
    }
    for (var iCount = 0; iCount < element.childNodes.length; iCount++) {
        if (element.childNodes[iCount].focus) {
            element.childNodes[iCount].focus();
            return;
        }
    }
    if (this.isChrome) {
        var input = element.ownerDocument.createElement("input");
        input.setAttribute("type", "input");
        element.appendChild(input);
        input.focus();
        element.removeChild(input);
        return;
    }
};

DCDomTools.ScrollIntoView = function (element, scrollParent) {
    if (element == null) {
        return;
    }
    if (!scrollParent) {
        scrollParent = document.body;
    }
    //    if (element.scrollIntoView) {
    //        element.scrollIntoView();
    //        return;
    //    }
    // if (element.getAttribute("dc_type") != null) {
    //     var a = 0;
    // }
    // if(element.nodeName == "#text"){
    //     element = element.parentElement || element.parentNode;
    // }
    // var zoomNum = isNaN(parseFloat($(document.body).css("zoom"))) ? 1 : parseFloat($(document.body).css("zoom"));
    // if (document.body.getAttribute("browser") == "FireFox") {
    //     zoomNum = 1;
    // }
    // var p = element.offsetParent;
    // var pos = element.offsetTop;
    // while (p != null && p.style) {
    //     if (p.style.overflowY == "auto" || p.style.overflowY == "scroll" || p == document.body) {
    //         // p.scrollTop = pos - p.clientHeight * 0.4;
    //         p.scrollTop = pos * zoomNum;
    //         return;
    //     }
    //     pos = pos + p.offsetTop;
    //     p = p.offsetParent;
    // }
    // //alert(element.offsetTop);
    // p = element.offsetParent;
    // if (p != null) {
    //     p.scrollTop = element.offsetTop * zoomNum;
    // }

    if(element.nodeName == "#text"){
        element = element.parentElement || element.parentNode;
    }
    var zoomNum = isNaN(parseFloat($(document.body).css("zoom"))) ? 1 : parseFloat($(document.body).css("zoom"));
    if (document.body.getAttribute("browser") == "FireFox") {
        zoomNum = 1;
    }
    var _top = DCDomTools.GetViewTopInDocument(element) - DCDomTools.GetViewTopInDocument(scrollParent);
    // var _top_new = $(element).offset().top;
    $(scrollParent).scrollTop(_top * zoomNum);
};

// 获得元素在文档中的绝对坐标边界矩形
DCDomTools.GetAbsBoundsInDocument = function (element) {
    var result = new Object();
    result.Left = 0;
    result.Top = 0;
    result.Width = 0;
    result.Height = 0;
    result.Right = 0;
    result.Bottom = 0;
    if (element != null) {
        result.Left = DCDomTools.GetViewLeftInDocument(element);
        result.Top = DCDomTools.GetViewTopInDocument(element);
        result.Width = element.offsetWidth;
        result.Height = element.offsetHeight;
    }
    result.Right = result.Left + result.Width;
    result.Bottom = result.Top + result.Height;

    return result;
};

DCDomTools.GetViewLeftInDocument = function (element) {
    if (element == null) {
        return 0;
    }
    var p = element;
    var result = 0;
    var rate = 1.0;
    while (p != null && p.tagName != "BODY") {
//        if (p.offsetParent != null) {
//            rate = parseFloat(p.offsetParent.style.zoom);
//            if (isNaN(rate) == true) {
//                rate = 1.0;
//            }
//        }
        result = result + p.offsetLeft * rate;
        p = p.offsetParent;
    }
    if (document.body.offsetLeft) {
        result += document.body.offsetLeft;
    }
    return result;
};

DCDomTools.GetViewTopInDocument = function (element) {
    if (element == null) {
        return 0;
    }
    var p = element;
    var result = 0;
    var rate = 1.0;
    while (p != null && p.tagName != "BODY") {
//        if (p.offsetParent != null) {
//            rate = parseFloat(p.offsetParent.style.zoom);
//            if (isNaN(rate) == true) {
//                rate = 1.0;
//            }
//        }
        result = result + p.offsetTop * rate;
        p = p.offsetParent;
    }
    if (document.body.offsetTop) {
        result += document.body.offsetTop;
    }
    return result;
};

//
// 获得元素的内部的HTML代码文本
//
DCDomTools.GetOuterHTML = function (element) {
    if (element == null) {
        return null;
    }
    if (element.outerHTML) {
        return element.outerHTML;
    }
    if (element.nodeName == "#text") {
        return element.nodeValue;
    }

    var result = "<" + element.nodeName + " ";
    for (var iCount = 0; iCount < element.attributes.length; iCount++) {
        var name = element.attributes[iCount].nodeName;
        var v = element.attributes[iCount].nodeValue;
        result = result + " " + name + "=\"" + v + "\"";
    }
    result = result + ">" + DCDomTools.GetInnerHTML(element) + "<\\" + element.nodeName + ">";
    return result;
};

//
// 获得元素的内部的HTML代码文本
//
DCDomTools.GetInnerHTML = function (element) {
    if (element == null) {
        return null;
    }
    var result = element.innerHTML;
    //alert(resul);
    return result;
};

DCDomTools.GetInnerTextByModified = function(node){
    if (!node) {
        return null;
    }
    // var result = WriterCommandModuleFormat.clearNoNeedText(element.innerHTML, true, null, true);
    var result = "";
    if (node.nodeType == 3) {
        result += node.textContent;
    } else if (node.nodeType == 1) {
        var dctypename = node.getAttribute("dctype");
        if(dctypename == "backgroundtext"){
            result += "bg=";
        }
        var nodes = node.childNodes;
        if (nodes.length == 0) {
            if (node.nodeName == "BR") {
                result += "\n";
            } else {

            }
        } else {
            for (var i = 0; i < nodes.length; i++) {
                result += DCDomTools.GetInnerTextByModified(nodes[i]);
            }
        }
        switch (node.nodeName) {
            case "P":
            case "TABLE":
            case "TR":
                if(node.nextSibling == null){
                    break;
                }
                result += "\n";
                break;
            default:
                break;
        }
    }
    return result;
}

//wyc20200528：判断该P标签的段落是否是只包含一个换行的空白段落
DCDomTools.IsLineBreakParagraph = function (element) {
    if (element == null || element == undefined) {
        return false;
    }
    if (element.nodeName == "P" &&
        ((element.childNodes.length == 1 && element.childNodes[0].nodeName == "BR")
            || (element.childNodes.length == 1 && element.childNodes[0].nodeName == "SPAN" && element.childNodes[0].childNodes.length == 1 && element.childNodes[0].childNodes[0].nodeName == "BR"))) {
        return true;
    } else {
        return false;
    }
};

//
// 获得元素的内部的纯文本
//
DCDomTools.GetInnerText = function (element,method) {
    if (element == null) {
        return null;
    }
    var result = "";
    //wyc20190506：添加判断若是输入域则从dc_innertext上取内容
    if (DCInputFieldManager.IsInputFieldElement(element) === true) {
        var innertext = element.getAttribute("dc_innertext");
        if (innertext !== null && innertext !== "" && innertext !== undefined) {
            result = innertext;
        }
    }
    //wyc20200527：针对单元格元素做特殊处理
    else if (element.nodeName == "TD") {
        for (var i = 0; i < element.childNodes.length; i++) {
            var tempelemenet = element.childNodes[i];

            if (DCDomTools.IsLineBreakParagraph(tempelemenet) == true) {
                //遇到内容完全为空白换行的段落，文本直接加上换行
                result = result + "\r\n";
            }
            else if (tempelemenet.nodeName == "P" && i != 0 && DCDomTools.IsLineBreakParagraph(tempelemenet.previousSibling) == false) {
                result = result + "\r\n" + tempelemenet.innerText;
            } else {
                result = result + tempelemenet.innerText;
            }
        }
    }
    //////////////////////////////////////////////////////////////
    else {
        //zhangbin 20220722 使用getSelectionText获取纯文本时去掉输入域边框和隐藏元素
        if(method == 'getSelectionText'){
            deleteBorderAndHide(element)
            function deleteBorderAndHide(ele){
                for(var z=0;z<ele.childNodes.length;z++){
                    if(ele.childNodes[z].nodeName != '#text'){
                        var dctype = $(ele.childNodes[z]).attr('dctype')
                        var display = ele.childNodes[z].style.getPropertyValue('display')
                        if(dctype == 'start' || dctype == 'end' || dctype == 'backgroundtext' || display == 'none'){
                            $(ele.childNodes[z]).remove()
                            z--
                        }else{
                            if(ele.childNodes[z].childNodes.length > 0){
                                deleteBorderAndHide(ele.childNodes[z])
                            }
                        }
                    }
                }
            }
        }
        result = element.innerText;//$(element).text();
    }
    // 20210819 xym 将ensp;空格替换为标准空格
    var reg = new RegExp(String.fromCharCode(8194), "g");
    result = result.replace(reg, " ");
    // 20210929 xym 删除零宽度占位符
    result = result.replace("\u200B","");
    return result;
};

//
// 设置元素的内部的纯文本
//
DCDomTools.SetInnerText = function (element, text) {
    if (element == null) {
        return null;
    }
    //wyc20190506：添加判断若是输入域则连带修改dc_innertext
    if (DCInputFieldManager.IsInputFieldElement(element) === true) {
        element.setAttribute("dc_innertext", text);
    }
    //////////////////////////////////////////////////////////////
    //wyc20200311：若是单元格，采用特殊处理方式
    if (element.nodeName == "TD"
        /*&& element.parentElement.parentElement.parentElement.getAttribute("dctype") == "XTextTableElement"*/) {
        if (element.childNodes.length > 0 && element.childNodes[0].nodeName == "P") {
            var elementp = element.childNodes[0];
            var p = elementp.cloneNode(true);
            if (text == "") {//如果赋值为空字符则特殊处理
                p.innerHTML = "";
                element.innerHTML = "";
                var br = document.createElement("br");
                br.setAttribute("dcpf", "1");
                p.appendChild(br);
                element.appendChild(p);
                return;
            }
            text = text.replace(/ /g, "&ensp;");
            text = text.replace(/\r\n/g, "<br dcpf='1'>");
            text = text.replace(/\n/g, "<br dcpf='1'>");
            text = text.replace(/\r/g, "<br dcpf='1'>");
            var tszf = "<span>" + text + "</span>";
            var node = $(tszf)[0];
            p.innerHTML = "";
            p.appendChild(node);
            element.innerHTML = "";
            element.appendChild(p); 
        }
        if (element.getAttribute("id") != null) {
            DCWriterExpressionManager.ExecuteEffectExpression(element);
        }
        return;
    }
    /////////////////////////////////////////////
    $(element).text(text);
};


//
// 删除指定元素的所有子元素
// 
DCDomTools.ClearChild = function (element) {
    if (element != null) {
        while (element.firstChild != null) {
            element.removeChild(element.firstChild);
        }
    }
};

//
// 设置指定元素的内部HTML代码
// 
DCDomTools.SetInnerHTML = function (element, strHtml) {
    if (element.nodeName == "TD") {
        if (element.childNodes.length > 0 && element.childNodes[0].nodeName == "P") {
            var elementp = element.childNodes[0];
            var p = elementp.cloneNode(true);
            if (strHtml == "" || strHtml == '\r\n') {//如果赋值为空字符则特殊处理
                p.innerHTML = "";
                element.innerHTML = "";
                var br = document.createElement("br");
                br.setAttribute("dcignore", "1");
                p.appendChild(br);
                element.appendChild(p);
                return;
            }
            // 20200907 xym 添加表格的单元格支持html标签语言赋值
            text = WriterCommandModuleFormat.clearNoNeedText(strHtml, false, null, false, true);
            p.innerHTML = strHtml;
            element.innerHTML = "";
            element.appendChild(p); 
        }
        if (element.getAttribute("id") != null) {
            DCWriterExpressionManager.ExecuteEffectExpression(element);
        }
        return;
    }
    $(element).html(strHtml);
    
};

// 根据HTML代码创建文档节点
DCDomTools.createContextualFragment = function (html) {
    if (html == null || html.length == 0) {
        return null;
    }
    var range = document.createRange();
    var df = range.createContextualFragment(html);
    return df;
};

//
// 设置指定框架的元素的内部HTML代码
//
DCDomTools.SetFrameInnerHTML = function (frameElement, strHtml) {
    //alert(strHtml.length);
    if (frameElement != null && frameElement.contentWindow) {
        frameElement.contentWindow.document.write(strHtml);
        frameElement.contentWindow.document.close();
    }
};


//
// 设置指定框架元素的内容HTML代码
//
DCDomTools.GetFrameInnerHTML = function (frameElement) {
    if (frameElement != null && frameElement.contentWindow) {
        //var bodyElement = frameElement.contentWindow.document.body;
        var txt = frameElement.contentWindow.document.documentElement.innerHTML;
        txt = "<html>" + txt + "</html>";
        //alert(txt);
        return txt;
    }
    return null;
};

//
// 设置指定元素的内部HTML代码，并保持内容视图滚动不变
//
DCDomTools.SetInnerHTMLWithoutScroll = function (element, strHtml) {

    var fscrollLeft = element.scrollLeft;
    var fscrollTop = element.scrollTop;
    while (element.firstChild != null) {
        element.removeChild(element.firstChild);
    }
    if (strHtml != null) {
        if (this.isIE) {
            element.insertAdjacentHTML("afterBegin", strHtml);
        }
        else {
            var range = element.ownerDocument.createRange();
            range.selectNodeContents(element);
            range.collapse(true);
            var df = range.createContextualFragment(strHtml);
            element.appendChild(df);
        }
        element.scrollLeft = fscrollLeft;
        element.scrollTop = fscrollTop;
    }
};

// 在容器元素的指定位置插入HTML代码
DCDomTools.inertHTML = function (element, index, strHtml, htmlMode) {
    if (element == null) {
        return false;
    }
    
    var df = null;
    if (htmlMode) {
        // HTML模式
        var range = null;
        if (document.createRange) {
            range = document.createRange();
        } else if (document.body.createRange) {
            range = document.body.createRange();
        }
        if (range != null) {
            range.setStartBefore(element);
            if (range.createContextualFragment) {
                df = range.createContextualFragment(strHtml);
            }
        }
        // 20200804 xym 修复ie9以下出错问题
        if (df == null) {
            df = document.createDocumentFragment();
            var div = document.createElement("div");
            df.appendChild(div);
            div.outerHTML = strHtml;
        }
    } else {
        // 纯文本模式
        df = document.createTextNode(strHtml);
    }
    if (element.childNodes.length == 0) {
        // 没有子元素，则直接添加
        element.appendChild(df);
    } else {
        if (index <= 0) {
            // 插入到第一个位置
            element.insertBefore(df, element.firstChild);
        } else if (index >= element.childNodes.length) {
            // 追加内容
            element.appendChild(df);
        } else {
            // 插入到中间
            element.insertBefore(df, element.childNodes[index]);
        }
    }
};

//
// 在指定的位置插入HTML代码
//
DCDomTools.insertAdjacentHTML = function (element, where, strHtml) {
    if (strHtml != null) {
        if (element.insertAdjacentHTML) {
            element.insertAdjacentHTML(where, strHtml);
        }
        else {
            var range = document.createRange();
            range.setStartBefore(element);
            var df = range.createContextualFragment(strHtml);
            switch (where) {
                case "beforeBegin":
                    element.parentNode.insertBefore(df, element);
                    break;
                case "afterBegin":
                    element.insertBefore(df, element.firstChild);
                    break;
                case "beforeEnd":
                    element.appendChild(df);
                    break;
                case "afterEnd":
                    if (element.nextSibling == null)
                        element.parentNode.appendChild(df);
                    else
                        element.parentNode.insertBefore(df, element.nextSibling);
                    break;
            } //switch
        }
    }
};

//**************************************************************************************************************
// 获得节点在其父节点中的子节点序号
DCDomTools.GetNodeIndex = function (node) {
    if (node == null || node.parentNode == null) {
        return -1;
    }
    var nodes = node.parentNode.childNodes;
    for (var iCount = 0; iCount < nodes.length; iCount++) {
        if (nodes[iCount] == node) {
            return iCount;
        }
    }
    return -1;
};


//**************************************************************************************************************
// 移动插入点到指定元素前
DCDomTools.MoveCaretToIndex = function (element, index) {
    if (element == null) {
        return;
    }
    if (element.nodeName == "INPUT"
        || element.nodeName == "SELECT"
        || element.nodeName == "TEXTAREA") {
        if (element.focus) {
            element.focus();
        }
        if (element.select) {
            element.select();
        }
        if (element.setActive) {
            element.setActive();
        }
        if (element.value != null) {
            var len = element.value.length;
            if (index >= 0 && index <= len) {
                if (element.type == "text" || element.type == "password") {
                    element.selectionStart = index;
                    element.selectionEnd = index;
                }
            }
        }
    }
    else {
        var sParent = element.parentNode;
        while (sParent != null) {
            if (sParent.clientHeight < sParent.scrollHeight) {
                break;
            }
            sParent = sParent.parentNode;
        }
        if (sParent == null) {
            sParent = document.body;
        }
        var sLeft = sParent.scrollLeft;
        var sTop = sParent.scrollTop;
        if (element.focus) {
            element.focus();
        }
        if (element.getClientRects) {
            var node2 = element;
            if (index >= 0 && index < element.childNodes.length) {
                node2 = element.childNodes[index];
                if (!node2.getClientRects) {
                    node2 = element;
                }
            }
            var rects = node2.getClientRects();
            if (rects.length > 0) {
                var sel2 = DCDomTools.getSelection();
                var range = null;
                if (document.createRange) {
                    range = document.createRange();
                }
                else if (document.body.createRange) {
                    range = document.body.createRange();
                }
                //                if (range.moveToPoint) {
                //                    range.moveToPoint(
                //                        rects[0].left + document.body.scrollLeft,
                //                        rects[0].top + document.body.scrollTop);
                //                    sel2.addRange(range);
                //                    return;
                //                }
            }
        }

        var sel = DCDomTools.getSelection();
        //var range = document.createRange();
        //range.setStart(element, index);
        //sel.removeAllRanges();
        //sel.addRange(range);
        //sel.collapseToStart();
        if (sel.collapse) {
            sel.collapse(element, index);
        }

        if (sel.anchorNode == null) {
            sel = DCDomTools.getSelection();
            var range = null;
            if (document.createRange) {
                range = document.createRange();
            }
            else {
                range = sel.createRange();
            }
            if (range.setStart) {
                range.setStart(element, index);
            }
            else {
                // 插入一个临时的按钮 
                if (element.nodeName != "#text") {
                    var btn = document.createElement("input");
                    btn.type = "button";
                    DCDomTools.insertChildNode(element, index, btn);
                    if (btn.focus) {
                        btn.focus();
                    }
                    if (btn.setActive) {
                        btn.setActive();
                    }
                    element.removeChild(btn);
                }
            }
            //            else if (range.moveToElementText) {
            //                range.moveToElementText(element);
            //            }
            if (sel.removeAllRanges) {
                sel.removeAllRanges();
                sel.addRange(range);
            }
        }
        sParent.scrollLeft = sLeft;
        sParent.scrollTop = sTop;
    }
    return;
};

//**************************************************************************************************************
// 移动插入点到指定元素前
DCDomTools.MoveCaretTo = function (element) {
    if (element == null) {
        return;
    }
    var sel = DCDomTools.getSelection();
    if (sel.collapse
        && sel.baseOffset && sel.extentOffset && sel.baseOffset == sel.extentOffset) {//WYC20190924：添加条件只有当选区为0时才移动光标
        try {
            sel.collapse(element, 0);
            return;
        }
        catch (e) {
        }
        try {
            // 在IE中曾经报错。
            var range = document.createRange();
            range.selectNode(element);
            sel.removeAllRanges();

            sel.addRange(range);
        }
        catch (e) {

        }
        return;
        //        if (element.nodeName == "SELECT" || element.nodeName == "INPUT") {
        //            var p = element.parentNode;
        //            var index = DCDomTools.GetNodeIndex(element);
        //            sel.colapse(p, index);
        //        }
        //        else {
        //            sel.collapse(element, 0);
        //        }
    }
    else if (sel.createRange){//WYC20190926：当双击时导致产生选区，此处可能会报错
        var rng = sel.createRange();
        if (element.nodeType == 1
            && rng.moveToElementText) {
            rng.moveToElementText(element.parentNode);
            //rng.select();
            rng.collapse(false);
        }
        else {
            if (element.focus) {
                element.focus();
            }
            if (element.setActive) {
                element.setActive();
            }
        }
    }
    return;
};

//**************************************************************************************************************
// 移动插入点到指定元素后
DCDomTools.MoveCaretToEnd = function (element) {
    if (element == null) {
        return;
    }
    var sel = DCDomTools.getSelection();
    if (sel.collapse) {
        var index = 0;
        if (element.nodeName == "#text") {
            var txt = element.nodeValue;
            if (txt == null || txt.length == 0) {
                sel.collapse(element, 0);
            }
            else {
                sel.collapse(element, txt.length);
            }
        } else if (element.nodeName == "INPUT"
            && element.type == "text"
            || element.nodeName == "TEXTAREA") {
            element.focus();
            element.select();
            var txt = element.value;
            if (txt != null || txt.length > 0) {
                element.selectionStart = txt.length;
                element.selectionEnd = txt.length;
            }
        } else if (element.nodeName == "TABLE") {
            var index = DCDomTools.GetNodeIndex(element) + 1;
            sel.collapse(element.parentNode, index);
        } else if (element.nodeName == "IMG" && $(element).parent()[0]) {
            // 20211103 xym 修复移动光标到图片后面无效的问题
            var c = $.inArray(element, $(element).parent()[0].childNodes);
            DCDomTools.MoveCaretToIndex($(element).parent()[0], c + 1);
        } else {
            var child = element.lastChild;
            while (child != null) {
                if (child.nodeName == "#text") {
                    DCDomTools.MoveCaretToEnd(child);
                    return;
                }
                else if (child.nodeName == "INPUT" && child.type == "text"
                    || child.nodeName == "TEXTAREA") {
                    DCDomTools.MoveCaretToEnd(child);
                    return;
                }
                child = child.lastChild;
            }//while
            sel.collapse(element, element.childNodes.length);
        }
    } else {
        var rng = document.body.createTextRange();
        if (rng && rng.moveToElementText) {
            try {
                if (element.nodeType == 3) {
                    rng.moveToElementText(element.parentNode);
                }
                else {
                    rng.moveToElementText(element);
                }
            }
            catch (ext) {
                //debugger;
            }
            var nodes = element.parentNode.childNodes;
            var pos = 0;
            for (var iCount = 0; iCount < nodes.length; iCount++) {
                if (nodes[iCount].nodeType == 1) {
                    var txt = nodes[iCount].innerText;
                    if (txt != null) {
                        pos = pos + txt.length;
                    }
                }
                else if (nodes[iCount].nodeType == 3) {
                    pos = pos + nodes[iCount].nodeValue.length;
                }
                if (nodes[iCount] == element) {
                    break;
                }
            }//for

            rng.move("character", pos);
            rng.select();
            rng.collapse();
            rng.select();
        } else {
            var span = document.createElement("span");
            element.parentNode.insertAdjacentElement("afterEnd", span);
            if (span.focus) {
                span.focus();
            }
            if (span.setActive) {
                span.setActive();
            }
            span.parentNode.removeChild(span);
        }
    }
    return;
};

//**************************************************************************************************************
// 获得选中区域信息对象
DCDomTools.getSelection = function (element) {
    var doc = document;
    if (element != null) {
        if (element.nodeName == "#document") {
            doc = element;
        }
        else {
            doc = element.ownerDocument;
        }
    }
    if (doc == null) {
        doc = document;
    }
    if (doc.getSelection) {
        return doc.getSelection();
    }
    if (doc.selection) {
        return doc.selection;
    }
    if (doc.parentWindow) {
        if (doc.parentWindow.getSelection) {
            return doc.parentWindow.getSelection();
        }
    }
    if (window.getSelection) {
        return window.getSelection();
    }
    return null;
};

DCDomTools.clearSelection = function () {
    var sel = DCDomTools.getSelection(null);
    if (sel != null) {
        if (sel.anchorNode == sel.focusNode
            && sel.anchorOffset == sel.focusOffset) {
            // 无需操作
            return;
        }
        if (sel.collapseToStart) {
            sel.collapseToStart();
        }
        else if (sel.createTextRange) {
            var rng = sel.createTextRange();
            rng.collapse(true);
        }
    }
};

DCDomTools.hasSelection = function () {

    var sel = DCDomTools.getSelection();
    if (sel == null) {
        return false;
    }
    if (sel.getRangeAt) {
        var rng = sel.getRangeAt(0);
        if (rng.startContainer == rng.endContainer
            && rng.startOffset == rng.endOffset) {
            return true;
        }
    }
    return false;
};
//***********************************************************************************
// 获得被选中的所有节点
// 徐逸铭 徐逸铭 2019-5-24
DCDomTools.getSelectAlldoms = function () {
    var sel = DCDomTools.getSelection();
    if (sel == null) {
        return false;
    }
    if (sel.getRangeAt) {
        var range = sel.getRangeAt(0);
        var dom = range.commonAncestorContainer; //dom节点
        //如果只有文本
        if (dom.nodeName == "#text") {
            var lastArr = [];
            lastArr.push(dom.parentElement);
            return lastArr;
        }
        var start_index, end_index;
        //***************************************
        var endNode = range.endContainer;
        if (endNode.nodeName == "#text") {
            if (endNode.nodeType == 3 && endNode.length > range.endOffset && range.endOffset > 0) {
                // 拆分结尾文本节点
                var node2 = endNode.splitText(range.endOffset);
            }
        }
        var startNode = range.startContainer;
        if (startNode.nodeName == "#text") {
            if (startNode.nodeType == 3 && startNode.length > range.startOffset && range.startOffset > 0) {
                // 拆分起始文本节点
                var node2 = startNode.splitText(range.startOffset);
                if (startNode == endNode) {
                    endNode = node2;
                }
                startNode = node2;
            }
        }
        //****************************************
        if (startNode.nodeName == "#text" && startNode.parentElement.innerText != startNode.nodeValue) {
            if (startNode.nextElementSibling) {
                $(startNode.nextElementSibling).prepend($(startNode));
                startNode.nextElementSibling.normalize();
            } else {
                $(startNode).wrap("<span></span>");
            }
        }
        if (endNode.nodeName == "#text" && endNode.parentElement.innerText != endNode.nodeValue) {
            if (endNode.previousElementSibling) {
                $(endNode.previousElementSibling).append($(endNode));
                endNode.previousElementSibling.normalize();
            } else {
                $(endNode).wrap("<span></span>");
            }
        }
        var domArr = [];
        getAlldom(dom, domArr);
        for (var i = 0; i < domArr.length; i++) {
            if (startNode.nodeName == "#text") {
                if (startNode.parentElement == domArr[i]) {
                    start_index = i;
                }
            } else {
                if (startNode.children.length > 0) {
                    var startArr = [];
                    getAlldom(startNode, startArr)
                    if (startArr[startArr.length - 1] == domArr[i]) {
                        start_index = i;
                    }
                } else {
                    start_index = i;
                }
            }
            if (endNode.nodeName == "#text") {
                if (endNode.parentElement == domArr[i]) {
                    end_index = i;
                }
            } else {
                if (endNode.children.length > 0) {
                    var endArr = [];
                    getAlldom(endNode, endArr)
                    if (endArr[endArr.length - 1] == domArr[i]) {
                        end_index = i;
                    }
                } else {
                    end_index = i;
                }
            }
        }
        //console.log(start_index, end_index)
        var lastArr = [];
        if (startNode != dom) {
            for (var i = 0; i < domArr.length; i++) {
                if (i >= start_index && i <= end_index) {
                    lastArr.push(domArr[i]);
                }
            }
        } else {//全选
            lastArr = domArr;
        }
        //如果结束部分文本节点被拆分
        if (endNode.nodeType == 3 && endNode.length > range.endOffset && range.endOffset > 0) {
            lastArr.splice($.inArray(endNode.parentElement.parentElement, lastArr), 1);
        }
        //开始没有选择文本
        if (startNode.length == range.startOffset) {
            if (startNode.nodeName == "#text") {
                lastArr.splice($.inArray(startNode.parentElement, lastArr), 1);
            } else {
                lastArr.splice($.inArray(startNode, lastArr), 1);
            }
        }
        //结束没有选择文本
        if (range.endOffset == 0) {
            if (endNode.nodeName == "#text") {
                lastArr.splice($.inArray(endNode.parentElement, lastArr), 1);
            } else {
                lastArr.splice($.inArray(endNode, lastArr), 1);
            }
        }
        //保存父节点
        //lastArr.unshift(dom);
        //console.log(lastArr);
        return lastArr;
    }
    function getAlldom(dom, domArr) {
        for (var i = 0; i < dom.children.length; i++) {
            domArr.push(dom.children[i]);
            if (dom.children[i].children.length > 0) {
                getAlldom(dom.children[i], domArr);
            }
        }
    }
}
//***********************************************************************************
// 判断插入点是否在指定容器中
DCDomTools.ContainsSelection = function (element) {
    var sel = DCDomTools.getSelection(element);
    if (sel.focusNode != null) {
        var node = sel.focusNode;
        while (node != null) {
            if (node == element) {
                return true;
            }
            node = node.parentNode;
        }
    }
    return false;
};

DCDomTools.FillFrameContentNotAsync = function (frameElement, url , funcFilter ) {
    var result = false;
    $.ajax(DCDomTools.fixAjaxSettings({ url: url, async: false, method: "POST", type: "POST" })).done(function (data, textStatus, jqXHR) {
        if (data.responseText) {
            data = data.responseText;
        }
        if (typeof (funcFilter) == "function") {
            data = funcFilter(data);
        }
        DCDomTools.SetFrameInnerHTML(frameElement, data);
        result = true;
    });
    return result;
};

//WYC20191030:颜(我)色(抄)RGB(抄)与(抄)HEX(抄)转(抄)换(抄)
DCDomTools.colorRGBToHex = function (colorRGBString) {
    var that = colorRGBString;
    var reg = /^#([0-9a-fA-f]{3}|[0-9a-fA-f]{6})$/;
    if (/^(rgb|RGB)/.test(that)) {
        var aColor = that.replace(/(?:\(|\)|rgb|RGB)*/g, "").split(",");
        var strHex = "#";
        for (var i = 0; i < aColor.length; i++) {
            var hex = Number(aColor[i]).toString(16);
            if (hex === "0") {
                hex += hex;
            }
            if(hex.length == 1){
                hex = "0"+ hex
            }
            strHex += hex;
        }
        if (strHex.length !== 7) {
            strHex = that;
        }
        return strHex;
    } else if (reg.test(that)) {
        var aNum = that.replace(/#/, "").split("");
        if (aNum.length === 6) {
            return that;
        } else if (aNum.length === 3) {
            var numHex = "#";
            for (var i = 0; i < aNum.length; i += 1) {
                numHex += (aNum[i] + aNum[i]);
            }
            return numHex;
        }
    }
    return that;
};
DCDomTools.colorHexToRGB = function (colorHexString) {
    var sColor = colorHexString.toLowerCase();
    var reg = /^#([0-9a-fA-f]{3}|[0-9a-fA-f]{6})$/;
    if (sColor && reg.test(sColor)) {
        if (sColor.length === 4) {
            var sColorNew = "#";
            for (var i = 1; i < 4; i += 1) {
                sColorNew += sColor.slice(i, i + 1).concat(sColor.slice(i, i + 1));
            }
            sColor = sColorNew;
        }
        var sColorChange = [];
        for (var i = 1; i < 7; i += 2) {
            sColorChange.push(parseInt("0x" + sColor.slice(i, i + 2)));
        }
        return "RGB(" + sColorChange.join(",") + ")";
    }
    return sColor;
};
//-------------------------------------------------
DCDomTools.GetSizeFromSpecifyFont = function (fontname, fontsize) {
    var fn = "";
    var fs = "";
    if (fontname !== undefined && fontname !== null && fontname !== "") {
        fn = fontname;
    }
    if (fontsize !== undefined && fontsize !== null && fontsize !== "") {
        fs = fontsize;
    }
    // 20220929 xym 修复下拉列表项高度问题【DCDomTools.GetSizeFromSpecifyFont】
    var span = document.createElement("div");
    span.id="spanformeasure"
    span.innerText = "H";
    span.style.fontSize = fs;
    span.style.fontFamily = fn;
    document.body.appendChild(span);
    var length = span.offsetHeight;
    document.body.removeChild(span);
    return length;
};

//WYC20191216:处理按钮元素在前端切换图片数据
DCDomTools.ProcessButtonImg = function (element, eventname) {
    if (element == null || element.nodeName != "INPUT") {
        return;
    }
    var img64data = element.getAttribute("dc_imgbase64");
    var img64dataforover = element.getAttribute("dc_imgbase64forover");
    var img64datafordown = element.getAttribute("dc_imgbase64fordown");
    
    switch (eventname) {
        case "onload":
            if (img64data != null && img64data.length > 0) {
                element.style.backgroundImage = "url('data:image/png;base64, " + img64data + "')";
            }
            break;
        case "onmouseenter":
            if (img64dataforover != null && img64dataforover.length > 0) {
                element.style.backgroundImage = "url('data:image/png;base64, " + img64dataforover + "')";
            }else{
                element.style.removeProperty('background-image')
            }
            break;
        case "onmouseleave":
            if (img64data != null && img64data.length > 0) {
                element.style.backgroundImage = "url('data:image/png;base64, " + img64data + "')";
            }else{
                element.style.removeProperty('background-image')
            }
            break;
        case "onmousedown":
            if (img64datafordown != null && img64datafordown.length > 0) {
                element.style.backgroundImage = "url('data:image/png;base64, " + img64datafordown + "')";
            }else{
                element.style.removeProperty('background-image')
            }
            break;
        case "onmouseup":
            if (img64data != null && img64data.length > 0) {
                var lastimg = "";
                if (element.focus) {
                    lastimg = img64dataforover;
                } else {
                    lastimg = img64data;
                }
                element.style.backgroundImage = "url('data:image/png;base64, " + lastimg + "')";
            }
            break;
        default:
            break;
    }
};

//WYC20191222：反转HTML元素的可见性
DCDomTools.ReverseElementVisibility = function (element) {
    //var element = document.WriterControl.GetContentDocument().getElementById(id);
    if (element != null && element.style) {
        if (element.style.display !== "none") {
            element.style.display = "none";
        } else {
            element.style.display = "";
        }
    }
};

//WYC20200915：处理html去除p元素将其子元素暴露出来
DCDomTools.RemoveHtmlParagraphElement = function (strHTML) {
    var div = document.createElement("div");
    div.innerHTML = strHTML;
    var ps = div.querySelectorAll("p");
    if (ps == null || ps.length > 1 || ps.length == 0) {
        return strHTML;
    } else {
        var tempdiv = document.createElement("div");
        for (var i = 0; i < div.childNodes.length; i++) {
            var node = div.childNodes[i].cloneNode(true);
            if (node.nodeName == "P") {
                for (var j = 0; j < node.childNodes.length; j++) {
                    tempdiv.appendChild(node.childNodes[j].cloneNode(true));
                }
            } else {
                tempdiv.appendChild(node);
            }
        }
        return tempdiv.innerHTML;
    }
};

//wyc20201116添加两个临时函数用于保存session内容，需要判断如果窗体顶层位于其它框架内则改变保存位置试图避开跨域问题
DCDomTools.GetDCSessionID20201022 = function () {
    var strCookie = document.cookie;
    if (strCookie != null && strCookie.length > 0) {
        var index = strCookie.indexOf("SessionId");
        if (index > 0) {

        }
    }
    if (window.top == window.self) {
        return window.top.dc_sessionid20201022;
    } else if (window.self) {
        return window.self.dc_sessionid20201022;// window.localStorage["dc_sessionid20201022"];
    } else {
        return null;
    }
};
DCDomTools.SetDCSessionID20201022 = function (value) {
    if (window.top == window.self) {
        window.top.dc_sessionid20201022 = value;
    } else if (window.self) {
        window.self.dc_sessionid20201022 = value; //window.localStorage["dc_sessionid20201022"] = value;
    }
};
//wyc20201123:新增函数对movecaretto函数进行前处理与后处理，将表单模式下的属性暂时放开，避免69版本的火狐浏览器的兼容性问题
DCDomTools.processMoveCaret = function (type) {
    if (document.WriterControl) {
        var win = document.WriterControl.GetContentWindow();
        if (win != null && win.DCWriterControllerEditor.IsFormView() == true) {
            var doc = document.WriterControl.GetContentDocument();
            var contenttable = doc.getElementById("dctable_AllContent");
            var tr = contenttable.querySelector("tr");
            if (type == "pre") {
                tr.setAttribute("contenteditable", "true");
            } else if (type == "post") {
                tr.setAttribute("contenteditable", "false");
            }
        }
    }
};

//wyc20210121:调整设置元素四维边框的代码
DCDomTools.SetElementBorder = function (element, options) {
    if (element.hasAttribute == null || element.hasAttribute("dctype") == false) {
        return;
    }
    if (document.WriterControl != null) {
        var bleft = true;
        var bleftcolor = "";
        var bright = true;
        var brightcolor = "";
        var bbottom = true;
        var bbottomcolor = "";
        var btop = true;
        var btopcolor = "";
        var bwidth = "1px";
        // 设置ShowCellNoneBorder只对单元格生效
        var showNoneBorder = element.nodeName == "TD" ? DCDomTools.toBoolean(document.WriterControl.DocumentOptions.ViewOptions.ShowCellNoneBorder, true) : null;

        var hassetborderstyle = false;
        if (options.BorderStyle &&
            (options.BorderStyle == "Dash" || options.BorderStyle == "Dot")) {
            hassetborderstyle = true;
        }
        var runtimeborderstyle = null;
        if (hassetborderstyle == false) {
            runtimeborderstyle = "solid";
        } else if (options.BorderStyle == "Dash") {
            runtimeborderstyle = "dashed";
        } else if (options.BorderStyle == "Dot") {
            runtimeborderstyle = "dotted";
        } else {
            runtimeborderstyle = "solid";
        }
        
        if (Object.prototype.hasOwnProperty.call(options, "BorderWidth")) {
            var i = parseInt(options.BorderWidth);
            if (i !== null && i !== undefined) {
                bwidth = i.toString() + "px";
            }
        }

        if (options.BorderLeft != null) {
            if (options.BorderLeft === "false" || options.BorderLeft === false) {
                bleft = false;
            } else if (options.BorderLeft === "true" || options.BorderLeft === true) {
                bleft = true;
            }
        }
        if (options.BorderRight != null) {
            if (options.BorderRight === "false" || options.BorderRight === false) {
                bright = false;
            } else if (options.BorderRight === "true" || options.BorderRight === true) {
                bright = true;
            }
        }
        if (options.BorderTop != null) {
            if (options.BorderTop === "false" || options.BorderTop === false) {
                btop = false;
            } else if (options.BorderTop === "true" || options.BorderTop === true) {
                btop = true;
            }
        }
        if (options.BorderBottom != null) {
            if (options.BorderBottom === "false" || options.BorderBottom === false) {
                bbottom = false;
            } else if (options.BorderBottom === "true" || options.BorderBottom === true) {
                bbottom = true;
            }
        }

        if (bwidth == "0px") {
            // 如果宽度为0，表示没有边框
            bleft = false;
            bright = false;
            btop = false;
            bbottom = false;
        }

        if (options.BorderLeftColor) {
            bleftcolor = options.BorderLeftColor;
        }
        if (options.BorderRightColor) {
            brightcolor = options.BorderRightColor;
        }
        if (options.BorderTopColor) {
            btopcolor = options.BorderTopColor;
        }
        if (options.BorderBottomColor) {
            bbottomcolor = options.BorderBottomColor;
        }

        if (bleft) {
            element.style.borderLeftWidth = bwidth;
            element.style.borderLeftStyle = runtimeborderstyle;
            element.style.borderLeftColor = bleftcolor;
        } else if (showNoneBorder) {
            element.style.borderLeftWidth = "1px";
            element.style.borderLeftStyle = runtimeborderstyle;
            element.style.borderLeftColor = "#D3D3D3";
        } else {
            element.style.borderLeftStyle = "none";
        }

        if (bright) {
            element.style.borderRightWidth = bwidth;
            element.style.borderRightStyle = runtimeborderstyle;
            element.style.borderRightColor = brightcolor;
        } else if (showNoneBorder) {
            element.style.borderRightWidth = "1px";
            element.style.borderRightStyle = runtimeborderstyle;
            element.style.borderRightColor = "D3D3D3";
        } else {
            element.style.borderRightStyle = "none";
        }

        if (btop) {
            element.style.borderTopWidth = bwidth;
            element.style.borderTopStyle = runtimeborderstyle;
            element.style.borderTopColor = btopcolor;
        } else if (showNoneBorder) {
            element.style.borderTopWidth = "1px";
            element.style.borderTopStyle = runtimeborderstyle;
            element.style.borderTopColor = "D3D3D3";
        } else {
            element.style.borderTopStyle = "none";
        }
        if (bbottom) {
            element.style.borderBottomWidth = bwidth;
            element.style.borderBottomStyle = runtimeborderstyle;
            element.style.borderBottomColor = bbottomcolor;
        } else if (showNoneBorder) {
            element.style.borderBottomWidth = "1px";
            element.style.borderBottomStyle = runtimeborderstyle;
            element.style.borderBottomColor = "D3D3D3";
        } else {
            element.style.borderBottomStyle = "none";
        }

        //拼接bd2019字符串用于组合边框信息
        var colorstring = "";
        var bdresult = "";
        if (bleftcolor == brightcolor
            && brightcolor == btopcolor
            && btopcolor == bbottomcolor) {
            colorstring = bleftcolor;
        } else {
            colorstring = bleftcolor + "," + btopcolor + "," + brightcolor + "," + bbottomcolor;
        }
        bdresult = colorstring + "|";
        if (bleft) {
            bdresult = bdresult + "1";
        } else {
            bdresult = bdresult + "0";
        }
        if (btop) {
            bdresult = bdresult + "1";
        } else {
            bdresult = bdresult + "0";
        }
        if (bright) {
            bdresult = bdresult + "1";
        } else {
            bdresult = bdresult + "0";
        }
        if (bbottom) {
            bdresult = bdresult + "1";
        } else {
            bdresult = bdresult + "0";
        }
        bdresult = bdresult + "|" + bwidth.replace("px", "");
        if (hassetborderstyle == true) {
            bdresult = bdresult + "|" + options.BorderStyle;
        }
        element.setAttribute("bd2019", bdresult);
    }
};

DCDomTools.getServerTickSpan = function (jqXHR) {
    if (jqXHR != null && jqXHR.getResponseHeader) {
        var strTick = jqXHR.getResponseHeader("dcservertickspan");
        if (strTick != null && strTick.length > 0) {
            return parseInt(strTick);
        }
    }
    return null;
}

DCDomTools.ParseAttributeToObject = function (str) {
    if (str == null) {
        return null;
    }
    var resultobj = new Object();
    if (str.charAt(0) == '[') {
        // JSON格式
        var obj;
        // 20220812 xym 添加trycatch避免报错【DCDomTools.GetElementCustomAttributes】
        try {
            obj = JSON.parse(str);
        } catch (error) {

        }
        if (obj == null || Array.isArray(obj) == false) {
            return null;
        }
        for (var i = 0; i < obj.length; i++) {
            var o = obj[i];
            if (o.Name) {
                resultobj[o.Name] = o.Value;
            }
        }
    }
    else {
        DCDomTools.ParseAttributeString(str, function (newName, newValue) {
            if (newName != null && newName.length > 0) {
                resultobj[newName] = newValue;
            }
        });
    }
    return resultobj;
};

//wyc20210127:获取文档元素的自定义Attributes，将后端传递的name:value对转换成前端直接使用的JSON对象
DCDomTools.GetElementCustomAttributes = function (element) {
    if (element.hasAttribute && element.hasAttribute("dc_attributes") == false) {
        return null;
    }
    var strAttributes = element.getAttribute("dc_attributes");
    if (strAttributes.length == 0) {
        return null;
    }
    return DCDomTools.ParseAttributeToObject(strAttributes);
};
//wyc20210127:设置文档元素的自定义Attributes，将前端直接使用的JSON对象转换成后端识别的name:value对字符串
DCDomTools.SetElementCustomAttributes = function (element, obj) {
    if (typeof (obj) !== "object" || element == undefined || element.setAttribute == undefined) {
        return false;
    }
    
    var resultobj = new Array();
    for (var i in obj) {
        var o = new Object();
        o.Name = i;
        o.Value = obj[i].toString();
        resultobj.push(o);
    }
    if (resultobj.length > 0) {
        element.setAttribute("dc_attributes", JSON.stringify(resultobj));
        return true;
    } else {
        return false;
    }
};

//wyc20210712:特殊加载文档模式，只加载要加载内容的文档体或页眉面脚并改变当前文档的相关部分，其它地方不动
DCDomTools.LoadDocumentFromHtmlTextSpecifyPart = function (doc, htmlContent, specifyLoadPart) {
    var originHeader = doc.getElementById("divXTextDocumentHeaderElement");
    var originFooter = doc.getElementById("divXTextDocumentFooterElement");
    var tempdiv = document.createElement("div");
    tempdiv.innerHTML = htmlContent;
    var loadedHeader = tempdiv.querySelector("#divXTextDocumentHeaderElement");
    var loadedFooter = tempdiv.querySelector("#divXTextDocumentFooterElement");
    //wyc20220713:补充初始化操作
    var win = document.WriterControl.GetContentWindow();
    if (win && win.DCWriterControllerEditor) {
        win.DCWriterControllerEditor.InitFileContentDom(loadedHeader, true);
        win.DCWriterControllerEditor.InitFileContentDom(loadedFooter, true);
    }
    

    switch (specifyLoadPart) {
        case "Header":
            originHeader.parentNode.replaceChild(loadedHeader, originHeader);
            break;
        case "Footer":
            originFooter.parentNode.replaceChild(loadedFooter, originFooter);
            break;
        case "Body":
            var copyHeader = originHeader.cloneNode(true);
            var copyFooter = originFooter.cloneNode(true);
            var tempdiv = document.createElement("div");
            //wyc20220323:调整逻辑 20220713再次调整，在doc.ready事件里替换
            doc.NeedReplaceHeader = copyHeader;
            doc.NeedReplaceFooter = copyFooter;

            doc.write(htmlContent);

            //tempdiv.innerHTML = htmlContent;
            //var header = doc.querySelector("#divXTextDocumentHeaderElement");
            //var footer = doc.querySelector("#divXTextDocumentFooterElement");
            //header.parentNode.replaceChild(copyHeader, header);
            //footer.parentNode.replaceChild(copyFooter, footer);
            //doc.write(tempdiv.innerHTML);           
            break;
        case "HeaderFooter":
            originHeader.parentNode.replaceChild(loadedHeader, originHeader);
            originFooter.parentNode.replaceChild(loadedFooter, originFooter);
            break;
        default:
            doc.write(htmlContent);
            break;
    }
};

//wyc20210713:前端设置文档元素的内容锁定
DCDomTools.SetElementContentLock = function (element, obj) {
    var ele = null;
    if (typeof (element) == "string") {
        var doc = document.WriterControl.GetContentDocument();
        ele = doc.getElementById(element);
    } else if (element.nodeName) {
        ele = element;
    }
    if (ele == null || ele.getAttribute == undefined) {
        console.log("未找到元素");
        return;
    }
    var dctype = ele.getAttribute("dctype");
    if (dctype !== "XTextInputFieldElement" &&
        dctype !== "XTextTableElement" &&
        dctype !== "XTextTableRowElement" &&
        dctype !== "XTextTableCellElement" &&
        dctype !== "XTextSubDocumentElement") {
        console.log("未找到元素");
        return;
    }
    if (obj == null || obj == undefined) {
        ele.removeAttribute("dc_contentlock");
    }
    if (typeof (obj) == "string") {
        ele.setAttribute("dc_contentlock", obj.toString());
    } else if (typeof (obj) == "object") {
        var AuthorisedUserIDList = "";
        var OwnerUserID = "";
        if (obj.AuthorisedUserIDList) {
            if (typeof (obj.AuthorisedUserIDList) == "string") {
                AuthorisedUserIDList = obj.AuthorisedUserIDList.toString();
            } else if (Array.isArray(obj.AuthorisedUserIDList) == true) {
                for (var i = 0; i < obj.AuthorisedUserIDList.length; i++) {
                    AuthorisedUserIDList = AuthorisedUserIDList + obj.AuthorisedUserIDList[i].toString();
                    if (i < obj.AuthorisedUserIDList.length - 1) {
                        AuthorisedUserIDList = AuthorisedUserIDList + ","
                    }
                }
            }
        }
        if (obj.OwnerUserID) {
            OwnerUserID = obj.OwnerUserID.toString();
        } else if (document.WriterControl.Options.CurrentUserID != null) {
            OwnerUserID = document.WriterControl.Options.CurrentUserID;
        }
        var resultstring = "AuthorisedUserIDList:" + AuthorisedUserIDList + ";OwnerUserID:" + OwnerUserID;
        ele.setAttribute("dc_contentlock", resultstring);
    }
    var win = document.WriterControl.GetContentWindow();
    if (win && win.DCWriterControllerEditor) {
        win.DCWriterControllerEditor.InitElementContentLock(ele, true);
    }
};

//wyc20210818:获取标签元素的连接模式设置
DCDomTools.GetLabelElementContactSettings = function (element) {
    var resultobj = new Object();
    var contactaction = element.getAttribute("dc_contactaction");
    if (contactaction == null || contactaction.length === 0) {
        contactaction = "Disable";
    }
    resultobj.ContactAction = contactaction;
    resultobj.AttributeNameForContactAction = element.getAttribute("dc_attributenameforcontactaction");
    resultobj.LinkTextForContactAction = element.getAttribute("dc_linktextforcontactaction");   
    return resultobj;
};
//wyc20210818:设置标签元素的连接模式
DCDomTools.SetLabelElementContactSettings = function (element, obj) {
    if (typeof (obj) !== "object" || element == undefined || element.setAttribute == undefined) {
        return false;
    }
    var contactmodes = ["Disable", "Normal", "FirstSectionInPage", "LastSectionInPage", "TableRow", "FirstTableRowInPage", "LastTableRowInPage"];
    if (contactmodes.indexOf(obj.ContactAction) >= 0) {
        element.setAttribute("dc_contactaction", obj.ContactAction.toString());
        if (obj.AttributeNameForContactAction == null) {
            obj.AttributeNameForContactAction = "";
        }
        if (obj.LinkTextForContactAction == null) {
            obj.LinkTextForContactAction = "";
        }
        element.setAttribute("dc_attributenameforcontactaction", obj.AttributeNameForContactAction.toString());
        element.setAttribute("dc_linktextforcontactaction", obj.LinkTextForContactAction.toString());
        return true;
    } else {
        console.log("合法的ContactAction的值：'Disable', 'Normal', 'FirstSectionInPage', 'LastSectionInPage', 'TableRow', 'FirstTableRowInPage', 'LastTableRowInPage'");
        return false;
    }   
};

//wyc20211122:前端获取文档元素所属的子文档
DCDomTools.GetElementOwnerSubDocument = function (element) {
    if (element == null) {
        return null;
    }
    var ele = null;
    if (typeof (element) == "string") {
        var doc = document.WriterControl.GetContentDocument();
        ele = doc.getElementById(element);
    } else if (element.nodeName) {
        ele = element;
    }
    if (ele == null || ele.parentElement == null) {
        console.log("未找到元素");
        return null;
    }
    var parent = ele.parentElement;
    var dctype = parent.getAttribute("dctype");
    if (dctype === "XTextDocumentHeaderElement" ||
        dctype === "XTextDocumentBodyElement" ||
        dctype === "XTextDocumentFooterElement") {
        return null;
    }
    while (dctype !== "XTextSubDocumentElement") {
        parent = parent.parentElement;
        if (parent != null && parent.getAttribute) {
            var dctype = parent.getAttribute("dctype");
        } else {
            return null;
        }
    }
    return parent;
};

//wyc20220414:前端判断HTMLDOM节点是否是编辑器文档元素
DCDomTools.IsDCWriterElement = function (element) {
    if (element == null) {
        return false;
    }
    if (element.hasAttribute && element.hasAttribute("dctype") == true) {
        return true;
    }
    if (element.nodeName == "TR" || element.nodeName == "TD") {
        var parent = element.parentElement;
        while (parent != null) {
            if (parent.nodeName == "TABLE" &&
                parent.hasAttribute &&
                parent.hasAttribute("dctype") == true) {
                return true;
            }
            parent = parent.parentElement;
        }
        return false;
    }
    return false;
};

DCDomTools.GetElementValueBindingAttr = function (element) {
    if (element == null || element.getAttribute == null) {
        return null;
    }
    var vbstr = element.getAttribute("dc_valuebinding");
    if (vbstr == null || vbstr.length == 0) {
        return null;
    }
    var resultObj = {
        AutoUpdate: false,
        BindingPath: "",
        BindingPathForText: "",
        DataSource: "",
        Enabled: true,
        FormatString: "",
        Handled: false,
        ProcessState: "Always",
        Readonly: false
    };
    var groups = vbstr.split(';');
    for (var i = 0; i < groups.length; i++) {
        var items = groups[i].split(':');
        var name = items[0];
        var value = items[1];
        switch (name) {
            case "AutoUpdate":
                if (value === "True") {
                    resultObj.AutoUpdate = true;
                } else if (value === "False") {
                    resultObj.AutoUpdate = false;
                }
                break;
            case "Enabled":
                if (value === "True") {
                    resultObj.Enabled = true;
                } else if (value === "False") {
                    resultObj.Enabled = false;
                }
                break;
            case "Handled":
                if (value === "True") {
                    resultObj.Handled = true;
                } else if (value === "False") {
                    resultObj.Handled = false;
                }
                break;
            case "Readonly":
                if (value === "True") {
                    resultObj.Readonly = true;
                } else if (value === "False") {
                    resultObj.Readonly = false;
                }
                break;
            case "BindingPath":
                if (value.toString) {
                    resultObj.BindingPath = value.toString();
                }
                break;
            case "BindingPathForText":
                if (value.toString) {
                    resultObj.BindingPathForText = value.toString();
                }
                break;
            case "DataSource":
                if (value.toString) {
                    resultObj.DataSource = value.toString();
                }
                break;          
            case "FormatString":
                if (value.toString) {
                    resultObj.FormatString = value.toString();
                }
                break;           
            case "ProcessState":
                if (value === "Always" || value === "Never" || value === "Once") {
                    resultObj.ProcessState = value;
                }
                break;           
            default:
                break;
        }
    }
    return resultObj;
};
DCDomTools.SetElementValueBindingAttr = function (element, obj) {
    if (DCDomTools.IsDCWriterElement(element) === false) {
        return false;
    }
    if (obj === null) {
        element.removeAttribute("dc_valuebinding");
        return true;
    }
    if (typeof (obj) !== "object") {
        return false;
    }
    var resultstr = "";
    if (obj.AutoUpdate === true) {
        resultstr = resultstr + "AutoUpdate:True;";
    }
    if (obj.Enabled === false) {
        resultstr = resultstr + "Enabled:False;";
    }
    if (obj.Handled === true) {
        resultstr = resultstr + "Handled:True;";
    }
    if (obj.Readonly === true) {
        resultstr = resultstr + "Readonly:True;";
    }
    if (obj.BindingPath && obj.BindingPath.length > 0) {
        resultstr = resultstr + "BindingPath:" + obj.BindingPath.toString() + ";";
    }
    if (obj.BindingPathForText && obj.BindingPathForText.length > 0) {
        resultstr = resultstr + "BindingPathForText:" + obj.BindingPathForText.toString() + ";";
    }
    if (obj.DataSource && obj.DataSource.length > 0) {
        resultstr = resultstr + "DataSource:" + obj.DataSource.toString() + ";";
    }
    if (obj.FormatString && obj.FormatString.length > 0) {
        resultstr = resultstr + "FormatString:" + obj.FormatString.toString() + ";";
    }
    if (obj.ProcessState === "Once" || obj.ProcessState === "Never") {
        resultstr = resultstr + "ProcessState:" + obj.ProcessState + ";";
    }

    if (resultstr.length > 0) {
        resultstr = resultstr.slice(0, resultstr.length - 1);
        element.setAttribute("dc_valuebinding", resultstr);
        return true;
    }
    return false;
};

//wyc20220823:针对多个组名不同但组内项ID相同的多选框选中时前端的接应处理函数
DCDomTools.HandleCheckedLabelClick = function (element) {
    var input = null;
    if (element != null && element.parentElement != null) {
        input = element.parentElement.querySelector("input");
    }
    if (input != null && input.click) {
        input.click();
        // DCWriterControllerEditor.HandleCheckedChanged(input);
    }
};

//  json2.js
//  2017-06-12

if (typeof JSON !== "object") {
    JSON = {};
}
(function () {
    "use strict";
    var rx_one = /^[\],:{}\s]*$/;
    var rx_two = /\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g;
    var rx_three = /"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g;
    var rx_four = /(?:^|:|,)(?:\s*\[)+/g;
    var rx_escapable = /[\\"\u0000-\u001f\u007f-\u009f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g;
    var rx_dangerous = /[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g;
    function f(n) {
        return (n < 10) ? "0" + n : n;
    }
    function this_value() {
        return this.valueOf();
    }
    if (typeof Date.prototype.toJSON !== "function") {

        Date.prototype.toJSON = function () {

            return isFinite(this.valueOf())
                ? (
                    this.getUTCFullYear()
                    + "-"
                    + f(this.getUTCMonth() + 1)
                    + "-"
                    + f(this.getUTCDate())
                    + "T"
                    + f(this.getUTCHours())
                    + ":"
                    + f(this.getUTCMinutes())
                    + ":"
                    + f(this.getUTCSeconds())
                    + "Z"
                )
                : null;
        };
        Boolean.prototype.toJSON = this_value;
        Number.prototype.toJSON = this_value;
        String.prototype.toJSON = this_value;
    }
    var gap;
    var indent;
    var meta;
    var rep;
    function quote(string) {
        rx_escapable.lastIndex = 0;
        return rx_escapable.test(string)
            ? "\"" + string.replace(rx_escapable, function (a) {
                var c = meta[a];
                return typeof c === "string"
                    ? c
                    : "\\u" + ("0000" + a.charCodeAt(0).toString(16)).slice(-4);
            }) + "\""
            : "\"" + string + "\"";
    }
    function str(key, holder) {
        var i;
        var k;
        var v;
        var length;
        var mind = gap;
        var partial;
        var value = holder[key];
        if (
            value
            && typeof value === "object"
            && typeof value.toJSON === "function"
        ) {
            value = value.toJSON(key);
        }
        if (typeof rep === "function") {
            value = rep.call(holder, key, value);
        }
        switch (typeof value) {
            case "string":
                return quote(value);
            case "number":
                return (isFinite(value)) ? String(value) : "null";
            case "boolean":
            case "null":
                return String(value);
            case "object":
                if (!value) {
                    return "null";
                }
                gap += indent;
                partial = [];
                if (Object.prototype.toString.apply(value) === "[object Array]") {
                    length = value.length;
                    for (i = 0; i < length; i += 1) {
                        partial[i] = str(i, value) || "null";
                    }
                    v = partial.length === 0
                        ? "[]"
                        : gap
                            ? (
                                "[\n"
                                + gap
                                + partial.join(",\n" + gap)
                                + "\n"
                                + mind
                                + "]"
                            )
                            : "[" + partial.join(",") + "]";
                    gap = mind;
                    return v;
                }
                if (rep && typeof rep === "object") {
                    length = rep.length;
                    for (i = 0; i < length; i += 1) {
                        if (typeof rep[i] === "string") {
                            k = rep[i];
                            v = str(k, value);
                            if (v) {
                                partial.push(quote(k) + (
                                    (gap)
                                        ? ": "
                                        : ":"
                                ) + v);
                            }
                        }
                    }
                } else {
                    for (k in value) {
                        if (Object.prototype.hasOwnProperty.call(value, k)) {
                            v = str(k, value);
                            if (v) {
                                partial.push(quote(k) + (
                                    (gap)
                                        ? ": "
                                        : ":"
                                ) + v);
                            }
                        }
                    }
                }
                v = partial.length === 0
                    ? "{}"
                    : gap
                        ? "{\n" + gap + partial.join(",\n" + gap) + "\n" + mind + "}"
                        : "{" + partial.join(",") + "}";
                gap = mind;
                return v;
        }
    }
    if (typeof JSON.stringify !== "function") {
        meta = {
            "\b": "\\b",
            "\t": "\\t",
            "\n": "\\n",
            "\f": "\\f",
            "\r": "\\r",
            "\"": "\\\"",
            "\\": "\\\\"
        };
        JSON.stringify = function (value, replacer, space) {
            var i;
            gap = "";
            indent = "";
            if (typeof space === "number") {
                for (i = 0; i < space; i += 1) {
                    indent += " ";
                }
            } else if (typeof space === "string") {
                indent = space;
            }
            rep = replacer;
            if (replacer && typeof replacer !== "function" && (
                typeof replacer !== "object"
                || typeof replacer.length !== "number"
            )) {
                throw new Error("JSON.stringify");
            }

            return str("", { "": value });
        };
    }
    if (typeof JSON.parse !== "function") {
        JSON.parse = function (text, reviver) {
            var j;

            function walk(holder, key) {
                var k;
                var v;
                var value = holder[key];
                if (value && typeof value === "object") {
                    for (k in value) {
                        if (Object.prototype.hasOwnProperty.call(value, k)) {
                            v = walk(value, k);
                            if (v !== undefined) {
                                value[k] = v;
                            } else {
                                delete value[k];
                            }
                        }
                    }
                }
                return reviver.call(holder, key, value);
            }
            text = String(text);
            rx_dangerous.lastIndex = 0;
            if (rx_dangerous.test(text)) {
                text = text.replace(rx_dangerous, function (a) {
                    return (
                        "\\u"
                        + ("0000" + a.charCodeAt(0).toString(16)).slice(-4)
                    );
                });
            }
            if (
                rx_one.test(
                    text
                        .replace(rx_two, "@")
                        .replace(rx_three, "]")
                        .replace(rx_four, "")
                )
            ) {
                j = eval("(" + text + ")");
                return (typeof reviver === "function")
                    ? walk({ "": j }, "")
                    : j;
            }
            throw new SyntaxError("JSON.parse");
        };
    }
}());

// 返回指定类型的全部节点的数组
DCDomTools.allChildNodes = function (node, type) {
    // 1.创建全部节点的数组
    var allCN = [];
    // 2.递归获取全部节点
    var getAllChildNodes = function (node, type, allCN) {
        // 获取当前元素所有的子节点nodes
        var nodes = node.childNodes;
        // 获取nodes的子节点
        for (var i = 0; i < nodes.length; i++) {
            var child = nodes[i];
            // 判断是否为指定类型节点
            if (child.nodeType == type) {
                allCN.push(child);
            }
            getAllChildNodes(child, type, allCN);
        }
    }
    getAllChildNodes(node, type, allCN);
    // 3.返回全部节点的数组
    return allCN;
}

// 当前选中的文本节点
DCDomTools.selectNodes = function () {
    var needChangeArr = [];//needChangeArr存储选中的文本节点
    var lastArr = DCDomTools.GetSelectionNodes();
    if (lastArr.length == 0) {
        return lastArr;
    }
    var lastNode = lastArr.length - 1 >=0 ? lastArr[lastArr.length - 1] : null;
    if (lastNode != null && lastNode.nodeName != "#text") {
        lastArr = $.merge(lastArr, DCDomTools.allChildNodes(lastNode, 3));
    }
    var range = DCDomTools.getSelectionRange();
    if (range == null || range.collapsed == true || range.cloneContents == null) {//20200824 xym 解决移动端输入文字报错问题
        return [];
    }
    if (range.startContainer != null && range.startContainer.nodeType == 3 && range.startContainer.textContent.length == range.startOffset && $.inArray(range.startContainer, lastArr) >= 0) {
        lastArr.splice($.inArray(range.startContainer, lastArr), 1);
    }
    if (range.endContainer != null && range.endOffset == 0) {
        var endArr = DCDomTools.allChildNodes(range.endContainer, 3);
        for (var i = 0; i < endArr.length; i++) {
            if ($.inArray(endArr[i], lastArr) >= 0) {
                lastArr.splice($.inArray(endArr[i], lastArr), 1);
            }
        }
        
    }
    var cloneDom = range.cloneContents();
    var div = $("<div></div>").append(cloneDom);
    var otherArr = DCDomTools.allChildNodes(div[0], 3);//存储选中的复制的文本节点
    for (var i = 0; i < otherArr.length; i++) {
        otherArr.splice(i, 1, otherArr[i].textContent);
    }
    for (var index = 0; index < lastArr.length; index++) {
        if (lastArr[index].nodeName == "#text" && $.inArray(lastArr[index].textContent, otherArr) >= 0) {//判断文本节点是否选中
            needChangeArr.push(lastArr[index]);
        }
    }
    return needChangeArr;
}

// 不可以删除的元素
DCDomTools.noDeleteNodes = function () {
    var noDelete = [];
    var selectNodes = DCDomTools.selectNodes();
    for (var i = 0; i < selectNodes.length; i++) {
        var noDeleteNode = $(selectNodes[i]).parents("[dc_deleteable='false']");
        if (noDeleteNode.length > 0 && $.inArray(noDeleteNode[0], noDelete) < 0) {
            var textArr = DCDomTools.allChildNodes(noDeleteNode[0], 3);
            var isFullSelect = true;
            for (var j = 0; j < textArr.length; j++) {
                if ($.inArray(textArr[j], selectNodes) < 0) {
                    isFullSelect = false;
                }
            }
            if (isFullSelect) {
                var needPush = true;
                $(noDeleteNode[0]).parents("[dc_deleteable='false']").each(function(){
                    if ($.inArray(this, noDelete) >= 0) {
                        needPush = false;
                    }
                })
                if (needPush) {
                    if (noDeleteNode[0].getAttribute("dctype") == "XTextCheckBoxElementBaseLabel") {
                        var isLeft = true;
                        if (noDeleteNode[0].firstChild.nodeName == "#text") {
                            isLeft = false;
                        }
                        if (isLeft) {
                            noDelete.push($(noDeleteNode[0]).prev()[0]);
                            noDelete.push(noDeleteNode[0]);
                        } else {
                            noDelete.push(noDeleteNode[0]);
                            noDelete.push($(noDeleteNode[0]).next()[0]);
                        }
                    } else {
                        noDelete.push(noDeleteNode[0]);
                    }
                }
            }
        }
    }
    return noDelete;
}

// 自定义删除接口
DCDomTools.delectNode = function (insertBr) {
    if (insertBr == null) {
        insertBr = false;
    }
    DCDomTools.completeEvent();
    var isFormView = DCWriterControllerEditor.IsFormView();//是否是表单模式
    var noDeleteNodes = DCDomTools.noDeleteNodes();
    var selectNodes = DCDomTools.selectNodes();
    for (var i = 0; i < noDeleteNodes.length; i++) {
        // 修复表单模式下全选输入域粘贴时无法覆盖问题
        if (isFormView == true && DCInputFieldManager.IsInputFieldElement(noDeleteNodes[i]) == true) {
            if ($(noDeleteNodes[i]).parents("[dctype='XTextInputFieldElement']").length == 0) {
                continue;
            }
        }
        var nodePar = $(noDeleteNodes[i]).parents("[dctype='XTextTableElement'],[dctype='XTextInputFieldElement']")[0];
        if (nodePar != null) {
            var textNodeArr1 = DCDomTools.allChildNodes(nodePar, 3);
            var flag = true;
            for (var j = 0; j < textNodeArr1.length; j++) {
                if ($.inArray(textNodeArr1[j], selectNodes) < 0) {
                    flag = false;
                }
            }
            if (!flag) {
                textNodeArr1 = [];
            }
        } else {
            var textNodeArr1 = [];
        }
        var textNodeArr2 = DCDomTools.allChildNodes(noDeleteNodes[i], 3);
        var textNodeArr = $.merge(textNodeArr1, textNodeArr2);
        for (var j = 0; j < textNodeArr.length; j++) {
            if ($.inArray(textNodeArr[j], selectNodes) >= 0) {
                selectNodes.splice($.inArray(textNodeArr[j], selectNodes), 1);
            }
        }
    }
    if (selectNodes.length == 0) {
        var range = DCDomTools.getSelectionRange();
        return range;
    }
    // 判断节点是否在可编辑区域
    var isAllContentEditable = function (node) {
        while (node != null) {
            if (node.isContentEditable == false) {
                // return false;
            }
            if (node.isContentEditable == true) {
                return true;
            }
            node = node.parentNode || node.parentElement;
        }
        return false;
    };
    // 判断是否选中了整个输入域
    for (var i = 0; i < selectNodes.length; i++) {
        var parNode = $(selectNodes[i]).parents("[dctype='XTextInputFieldElement']:first");
        if(parNode.length == 1){
            var textNodeArr3 = DCDomTools.allChildNodes(parNode[0], 3);
            if ($.inArray(parNode[0], selectNodes) > -1) {
                continue;
            }
            // 20220328 xym 禁止表单模式下删除最外输入域
            if (isFormView == true && parNode.parents("[dctype='XTextInputFieldElement']").length == 0) {
                continue;
            }
            var flag = true;
            for (var j = 0; j < textNodeArr3.length; j++) {
                if ($.inArray(textNodeArr3[j], selectNodes) < 0) {
                    flag = false;
                }
            }
            if(flag && parNode.attr("dc_deleteable") != "false"){
                selectNodes.push(parNode[0]);
            }
        }
    }
    // var needFixInputElementArr = [];//存储需要重新修正DOM结构的输入域元素
    var lastArr = DCDomTools.GetSelectionNodes();//补充没有内容的可删除元素（包括单复选框，表格）
    for (var i = 0; i < selectNodes.length; i++) {
        if (isAllContentEditable(selectNodes[i]) == true) {
            var $parent = $(selectNodes[i]).parent();
            if ($parent[0].nodeName == "P" && $parent.parent()[0].nodeName == "TD" && $parent[0].childNodes.length == 1) {
                $(selectNodes[i]).replaceWith($("<br/>"));
            } else {
                var _dctype = $parent.attr("dctype");
                if (_dctype == "start" || _dctype == "end") {
                    // var input = $parent.parent()[0];
                    // if ($.inArray(input, needFixInputElementArr) == -1) {
                    //     needFixInputElementArr.push(input);
                    // }
                } else {
                    $(selectNodes[i]).remove();
                }
                // $(selectNodes[i]).remove();
            }
        }
    }
    for (var i = 0; i < lastArr.length; i++) {
        if (lastArr[i].nodeType == 1 && !lastArr[i].getAttribute("dcignore")) {
            var dctype = lastArr[i].getAttribute("dctype");
            if (dctype == "XTextImageElement") {
                $(lastArr[i]).remove();
                continue;
            } else if (DCDomTools.allChildNodes(lastArr[i], 3).length == 0 && lastArr[i].getAttribute("dc_deleteable") != "false") {
                if (dctype != null || lastArr[i].nodeName == "SPAN") {
                    $(lastArr[i]).remove();
                    continue;
                }
            }
            if (lastArr[i].nodeName == "P" && lastArr[i].parentElement.nodeName != "TD" && lastArr[i].innerText.replace(/[\r\n]/g, "") == "") {
                if ($(lastArr[i]).find("[dc_deleteable='false']").length == 0) {
                    $(lastArr[i]).remove();
                    continue;
                }
            }
            if (lastArr[i].nodeName == "P" && lastArr[i].parentElement.nodeName == "TD" && lastArr[i].parentElement.innerText == "" && lastArr[i].innerText == "") {
                lastArr[i].innerHTML = "<br />";
                continue;
            }
            if(lastArr[i].nodeName == "BR"){
                if($(lastArr[i]).parent()[0].nodeName == "P" && $(lastArr[i]).parent().parent()[0].nodeName == "TD"){
                }else{
                    $(lastArr[i]).remove();
                    continue; 
                }
            }
        }
    }
    // var range = DCDomTools.getSelectionRange();
    // if (range != null && range.collapse) {
    //     range.collapse(toStart);
    //     // 记录最后一次有效的当前状态
    //     DCSelectionManager.LastSelectionInfo = DCSelectionManager.getSelection();
    //     DCSelectionManager.LastSelectionInfoWithFix = DCSelectionManager.getSelectionWithFix();//wyc20200217：补充更新当前光标所在位
    // }
    var range = DCDomTools.getSelectionRange();
    if (range && range.commonAncestorContainer) {
        var commonAncestorContainer = range.commonAncestorContainer;
        if (commonAncestorContainer.nodeName == "P" && insertBr == true) {
            if ((commonAncestorContainer.parentElement.nodeName == "TD" && commonAncestorContainer.parentElement.innerText == "") || commonAncestorContainer.innerText == "") {
                commonAncestorContainer.innerHTML = "<br />";
            }
        }
    }
    // 记录最后一次有效的当前状态
    DCSelectionManager.LastSelectionInfo = DCSelectionManager.getSelection();
    DCSelectionManager.LastSelectionInfoWithFix = DCSelectionManager.getSelectionWithFix();//wyc20200217：补充更新当前光标所在位
    return range;
}

// 循环获取节点下的所有子节点
//@param targetEle 目标元素
//@param deep 是否递归获取所有子节点
DCDomTools.loopGetAllChildNode = function(targetEle,deep){
    //所有的子节点
    if(deep){
        var allChildNodes = deepLoop(targetEle)
    }else{
        var allChildNodes = []
        if(targetEle){
            for(var i=0;i<targetEle.childNodes.length;i++){
                allChildNodes.push(targetEle.childNodes[i])
            }
        }
    }
    return allChildNodes

    //递归获取所有子节点
    function deepLoop(targetEle){
        var childNodes = {
            element: targetEle,
            childNodes: []
        }
        for(var i=0;i<targetEle.childNodes.length;i++){
            childNodes.childNodes.push(deepLoop(targetEle.childNodes[i]))
        }
        return childNodes
    }
    
} 

///*
// *  base64.js
// *
// *  Licensed under the BSD 3-Clause License.
// *    http://opensource.org/licenses/BSD-3-Clause
// *
// *  References:
// *    http://en.wikipedia.org/wiki/Base64
// */
//; (function (global, factory) {
//    typeof exports === 'object' && typeof module !== 'undefined'
//        ? module.exports = factory(global)
//        : typeof define === 'function' && define.amd
//            ? define(factory) : factory(global)
//}((
//    typeof self !== 'undefined' ? self
//        : typeof window !== 'undefined' ? window
//            : typeof global !== 'undefined' ? global
//                : this
//), function (global) {
//    'use strict';
//    // existing version for noConflict()
//    global = global || {};
//    var _Base64 = global.Base64;
//    var version = "2.5.1";
//    // if node.js and NOT React Native, we use Buffer
//    var buffer;
//    if (typeof module !== 'undefined' && module.exports) {
//        try {
//            buffer = eval("require('buffer').Buffer");
//        } catch (err) {
//            buffer = undefined;
//        }
//    }
//    // constants
//    var b64chars
//        = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/';
//    var b64tab = function (bin) {
//        var t = {};
//        for (var i = 0, l = bin.length; i < l; i++) t[bin.charAt(i)] = i;
//        return t;
//    }(b64chars);
//    var fromCharCode = String.fromCharCode;
//    // encoder stuff
//    var cb_utob = function (c) {
//        if (c.length < 2) {
//            var cc = c.charCodeAt(0);
//            return cc < 0x80 ? c
//                : cc < 0x800 ? (fromCharCode(0xc0 | (cc >>> 6))
//                    + fromCharCode(0x80 | (cc & 0x3f)))
//                    : (fromCharCode(0xe0 | ((cc >>> 12) & 0x0f))
//                        + fromCharCode(0x80 | ((cc >>> 6) & 0x3f))
//                        + fromCharCode(0x80 | (cc & 0x3f)));
//        } else {
//            var cc = 0x10000
//                + (c.charCodeAt(0) - 0xD800) * 0x400
//                + (c.charCodeAt(1) - 0xDC00);
//            return (fromCharCode(0xf0 | ((cc >>> 18) & 0x07))
//                + fromCharCode(0x80 | ((cc >>> 12) & 0x3f))
//                + fromCharCode(0x80 | ((cc >>> 6) & 0x3f))
//                + fromCharCode(0x80 | (cc & 0x3f)));
//        }
//    };
//    var re_utob = /[\uD800-\uDBFF][\uDC00-\uDFFFF]|[^\x00-\x7F]/g;
//    var utob = function (u) {
//        return u.replace(re_utob, cb_utob);
//    };
//    var cb_encode = function (ccc) {
//        var padlen = [0, 2, 1][ccc.length % 3],
//            ord = ccc.charCodeAt(0) << 16
//                | ((ccc.length > 1 ? ccc.charCodeAt(1) : 0) << 8)
//                | ((ccc.length > 2 ? ccc.charCodeAt(2) : 0)),
//            chars = [
//                b64chars.charAt(ord >>> 18),
//                b64chars.charAt((ord >>> 12) & 63),
//                padlen >= 2 ? '=' : b64chars.charAt((ord >>> 6) & 63),
//                padlen >= 1 ? '=' : b64chars.charAt(ord & 63)
//            ];
//        return chars.join('');
//    };
//    var btoa = global.btoa ? function (b) {
//        return global.btoa(b);
//    } : function (b) {
//        return b.replace(/[\s\S]{1,3}/g, cb_encode);
//    };
//    var _encode = function (u) {
//        const isUint8Array = Object.prototype.toString.call(u) === '[object Uint8Array]';
//        return isUint8Array ? u.toString('base64')
//            : btoa(utob(String(u)));
//    }
//    var encode = function (u, urisafe) {
//        return !urisafe
//            ? _encode(u)
//            : _encode(String(u)).replace(/[+\/]/g, function (m0) {
//                return m0 == '+' ? '-' : '_';
//            }).replace(/=/g, '');
//    };
//    var encodeURI = function (u) { return encode(u, true) };
//    // decoder stuff
//    var re_btou = new RegExp([
//        '[\xC0-\xDF][\x80-\xBF]',
//        '[\xE0-\xEF][\x80-\xBF]{2}',
//        '[\xF0-\xF7][\x80-\xBF]{3}'
//    ].join('|'), 'g');
//    var cb_btou = function (cccc) {
//        switch (cccc.length) {
//            case 4:
//                var cp = ((0x07 & cccc.charCodeAt(0)) << 18)
//                    | ((0x3f & cccc.charCodeAt(1)) << 12)
//                    | ((0x3f & cccc.charCodeAt(2)) << 6)
//                    | (0x3f & cccc.charCodeAt(3)),
//                    offset = cp - 0x10000;
//                return (fromCharCode((offset >>> 10) + 0xD800)
//                    + fromCharCode((offset & 0x3FF) + 0xDC00));
//            case 3:
//                return fromCharCode(
//                    ((0x0f & cccc.charCodeAt(0)) << 12)
//                    | ((0x3f & cccc.charCodeAt(1)) << 6)
//                    | (0x3f & cccc.charCodeAt(2))
//                );
//            default:
//                return fromCharCode(
//                    ((0x1f & cccc.charCodeAt(0)) << 6)
//                    | (0x3f & cccc.charCodeAt(1))
//                );
//        }
//    };
//    var btou = function (b) {
//        return b.replace(re_btou, cb_btou);
//    };
//    var cb_decode = function (cccc) {
//        var len = cccc.length,
//            padlen = len % 4,
//            n = (len > 0 ? b64tab[cccc.charAt(0)] << 18 : 0)
//                | (len > 1 ? b64tab[cccc.charAt(1)] << 12 : 0)
//                | (len > 2 ? b64tab[cccc.charAt(2)] << 6 : 0)
//                | (len > 3 ? b64tab[cccc.charAt(3)] : 0),
//            chars = [
//                fromCharCode(n >>> 16),
//                fromCharCode((n >>> 8) & 0xff),
//                fromCharCode(n & 0xff)
//            ];
//        chars.length -= [0, 0, 2, 1][padlen];
//        return chars.join('');
//    };
//    var _atob = global.atob ? function (a) {
//        return global.atob(a);
//    } : function (a) {
//        return a.replace(/\S{1,4}/g, cb_decode);
//    };
//    var atob = function (a) {
//        return _atob(String(a).replace(/[^A-Za-z0-9\+\/]/g, ''));
//    };
//    var _decode = buffer ?
//        buffer.from && Uint8Array && buffer.from !== Uint8Array.from
//            ? function (a) {
//                return (a.constructor === buffer.constructor
//                    ? a : buffer.from(a, 'base64')).toString();
//            }
//            : function (a) {
//                return (a.constructor === buffer.constructor
//                    ? a : new buffer(a, 'base64')).toString();
//            }
//        : function (a) { return btou(_atob(a)) };
//    var decode = function (a) {
//        return _decode(
//            String(a).replace(/[-_]/g, function (m0) { return m0 == '-' ? '+' : '/' })
//                .replace(/[^A-Za-z0-9\+\/]/g, '')
//        );
//    };
//    var noConflict = function () {
//        var Base64 = global.Base64;
//        global.Base64 = _Base64;
//        return Base64;
//    };
//    // export Base64
//    global.Base64 = {
//        VERSION: version,
//        atob: atob,
//        btoa: btoa,
//        fromBase64: decode,
//        toBase64: encode,
//        utob: utob,
//        encode: encode,
//        encodeURI: encodeURI,
//        btou: btou,
//        decode: decode,
//        noConflict: noConflict,
//        __buffer__: buffer
//    };
//    // if ES5 is available, make Base64.extendString() available
//    if (typeof Object.defineProperty === 'function') {
//        var noEnum = function (v) {
//            return { value: v, enumerable: false, writable: true, configurable: true };
//        };
//        global.Base64.extendString = function () {
//            Object.defineProperty(
//                String.prototype, 'fromBase64', noEnum(function () {
//                    return decode(this)
//                }));
//            Object.defineProperty(
//                String.prototype, 'toBase64', noEnum(function (urisafe) {
//                    return encode(this, urisafe)
//                }));
//            Object.defineProperty(
//                String.prototype, 'toBase64URI', noEnum(function () {
//                    return encode(this, true)
//                }));
//        };
//    }
//    //
//    // export Base64 to the namespace
//    //
//    if (global['Meteor']) { // Meteor.js
//        Base64 = global.Base64;
//    }
//    // module.exports and AMD are mutually exclusive.
//    // module.exports has precedence.
//    if (typeof module !== 'undefined' && module.exports) {
//        module.exports.Base64 = global.Base64;
//    }
//    else if (typeof define === 'function' && define.amd) {
//        // AMD. Register as an anonymous module.
//        define([], function () { return global.Base64 });
//    }
//    // that's it!
//    return { Base64: global.Base64 }
//}));


var LZString = (function () {

    // private property
    var f = String.fromCharCode;
    var keyStrBase64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
    var keyStrUriSafe = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+-$";
    var baseReverseDic = {};

    function getBaseValue(alphabet, character) {
        if (!baseReverseDic[alphabet]) {
            baseReverseDic[alphabet] = {};
            for (var i = 0; i < alphabet.length; i++) {
                baseReverseDic[alphabet][alphabet.charAt(i)] = i;
            }
        }
        return baseReverseDic[alphabet][character];
    }

    var LZString = {
        compressToBase64: function (input) {
            if (input == null) return "";
            var res = LZString._compress(input, 6, function (a) { return keyStrBase64.charAt(a); });
            switch (res.length % 4) { // To produce valid Base64
                default: // When could this happen ?
                case 0: return res;
                case 1: return res + "===";
                case 2: return res + "==";
                case 3: return res + "=";
            }
        },

        decompressFromBase64: function (input) {
            if (input == null) return "";
            if (input == "") return null;
            return LZString._decompress(input.length, 32, function (index) { return getBaseValue(keyStrBase64, input.charAt(index)); });
        },

        compressToUTF16: function (input) {
            if (input == null) return "";
            return LZString._compress(input, 15, function (a) { return f(a + 32); }) + " ";
        },

        decompressFromUTF16: function (compressed) {
            if (compressed == null) return "";
            if (compressed == "") return null;
            return LZString._decompress(compressed.length, 16384, function (index) { return compressed.charCodeAt(index) - 32; });
        },

        //compress into uint8array (UCS-2 big endian format)
        compressToUint8Array: function (uncompressed) {
            var compressed = LZString.compress(uncompressed);
            var buf = new Uint8Array(compressed.length * 2); // 2 bytes per character

            for (var i = 0, TotalLen = compressed.length; i < TotalLen; i++) {
                var current_value = compressed.charCodeAt(i);
                buf[i * 2] = current_value >>> 8;
                buf[i * 2 + 1] = current_value % 256;
            }
            return buf;
        },

        //decompress from uint8array (UCS-2 big endian format)
        decompressFromUint8Array: function (compressed) {
            if (compressed === null || compressed === undefined) {
                return LZString.decompress(compressed);
            } else {
                var buf = new Array(compressed.length / 2); // 2 bytes per character
                for (var i = 0, TotalLen = buf.length; i < TotalLen; i++) {
                    buf[i] = compressed[i * 2] * 256 + compressed[i * 2 + 1];
                }

                var result = [];
                buf.forEach(function (c) {
                    result.push(f(c));
                });
                return LZString.decompress(result.join(''));

            }

        },


        //compress into a string that is already URI encoded
        compressToEncodedURIComponent: function (input) {
            if (input == null) return "";
            return LZString._compress(input, 6, function (a) { return keyStrUriSafe.charAt(a); });
        },

        //decompress from an output of compressToEncodedURIComponent
        decompressFromEncodedURIComponent: function (input) {
            if (input == null) return "";
            if (input == "") return null;
            input = input.replace(/ /g, "+");
            return LZString._decompress(input.length, 32, function (index) { return getBaseValue(keyStrUriSafe, input.charAt(index)); });
        },

        compress: function (uncompressed) {
            return LZString._compress(uncompressed, 16, function (a) { return f(a); });
        },
        _compress: function (uncompressed, bitsPerChar, getCharFromInt) {
            if (uncompressed == null) return "";
            var i, value,
                context_dictionary = {},
                context_dictionaryToCreate = {},
                context_c = "",
                context_wc = "",
                context_w = "",
                context_enlargeIn = 2, // Compensate for the first entry which should not count
                context_dictSize = 3,
                context_numBits = 2,
                context_data = [],
                context_data_val = 0,
                context_data_position = 0,
                ii;

            for (ii = 0; ii < uncompressed.length; ii += 1) {
                context_c = uncompressed.charAt(ii);
                if (!Object.prototype.hasOwnProperty.call(context_dictionary, context_c)) {
                    context_dictionary[context_c] = context_dictSize++;
                    context_dictionaryToCreate[context_c] = true;
                }

                context_wc = context_w + context_c;
                if (Object.prototype.hasOwnProperty.call(context_dictionary, context_wc)) {
                    context_w = context_wc;
                } else {
                    if (Object.prototype.hasOwnProperty.call(context_dictionaryToCreate, context_w)) {
                        if (context_w.charCodeAt(0) < 256) {
                            for (i = 0; i < context_numBits; i++) {
                                context_data_val = (context_data_val << 1);
                                if (context_data_position == bitsPerChar - 1) {
                                    context_data_position = 0;
                                    context_data.push(getCharFromInt(context_data_val));
                                    context_data_val = 0;
                                } else {
                                    context_data_position++;
                                }
                            }
                            value = context_w.charCodeAt(0);
                            for (i = 0; i < 8; i++) {
                                context_data_val = (context_data_val << 1) | (value & 1);
                                if (context_data_position == bitsPerChar - 1) {
                                    context_data_position = 0;
                                    context_data.push(getCharFromInt(context_data_val));
                                    context_data_val = 0;
                                } else {
                                    context_data_position++;
                                }
                                value = value >> 1;
                            }
                        } else {
                            value = 1;
                            for (i = 0; i < context_numBits; i++) {
                                context_data_val = (context_data_val << 1) | value;
                                if (context_data_position == bitsPerChar - 1) {
                                    context_data_position = 0;
                                    context_data.push(getCharFromInt(context_data_val));
                                    context_data_val = 0;
                                } else {
                                    context_data_position++;
                                }
                                value = 0;
                            }
                            value = context_w.charCodeAt(0);
                            for (i = 0; i < 16; i++) {
                                context_data_val = (context_data_val << 1) | (value & 1);
                                if (context_data_position == bitsPerChar - 1) {
                                    context_data_position = 0;
                                    context_data.push(getCharFromInt(context_data_val));
                                    context_data_val = 0;
                                } else {
                                    context_data_position++;
                                }
                                value = value >> 1;
                            }
                        }
                        context_enlargeIn--;
                        if (context_enlargeIn == 0) {
                            context_enlargeIn = Math.pow(2, context_numBits);
                            context_numBits++;
                        }
                        delete context_dictionaryToCreate[context_w];
                    } else {
                        value = context_dictionary[context_w];
                        for (i = 0; i < context_numBits; i++) {
                            context_data_val = (context_data_val << 1) | (value & 1);
                            if (context_data_position == bitsPerChar - 1) {
                                context_data_position = 0;
                                context_data.push(getCharFromInt(context_data_val));
                                context_data_val = 0;
                            } else {
                                context_data_position++;
                            }
                            value = value >> 1;
                        }


                    }
                    context_enlargeIn--;
                    if (context_enlargeIn == 0) {
                        context_enlargeIn = Math.pow(2, context_numBits);
                        context_numBits++;
                    }
                    // Add wc to the dictionary.
                    context_dictionary[context_wc] = context_dictSize++;
                    context_w = String(context_c);
                }
            }

            // Output the code for w.
            if (context_w !== "") {
                if (Object.prototype.hasOwnProperty.call(context_dictionaryToCreate, context_w)) {
                    if (context_w.charCodeAt(0) < 256) {
                        for (i = 0; i < context_numBits; i++) {
                            context_data_val = (context_data_val << 1);
                            if (context_data_position == bitsPerChar - 1) {
                                context_data_position = 0;
                                context_data.push(getCharFromInt(context_data_val));
                                context_data_val = 0;
                            } else {
                                context_data_position++;
                            }
                        }
                        value = context_w.charCodeAt(0);
                        for (i = 0; i < 8; i++) {
                            context_data_val = (context_data_val << 1) | (value & 1);
                            if (context_data_position == bitsPerChar - 1) {
                                context_data_position = 0;
                                context_data.push(getCharFromInt(context_data_val));
                                context_data_val = 0;
                            } else {
                                context_data_position++;
                            }
                            value = value >> 1;
                        }
                    } else {
                        value = 1;
                        for (i = 0; i < context_numBits; i++) {
                            context_data_val = (context_data_val << 1) | value;
                            if (context_data_position == bitsPerChar - 1) {
                                context_data_position = 0;
                                context_data.push(getCharFromInt(context_data_val));
                                context_data_val = 0;
                            } else {
                                context_data_position++;
                            }
                            value = 0;
                        }
                        value = context_w.charCodeAt(0);
                        for (i = 0; i < 16; i++) {
                            context_data_val = (context_data_val << 1) | (value & 1);
                            if (context_data_position == bitsPerChar - 1) {
                                context_data_position = 0;
                                context_data.push(getCharFromInt(context_data_val));
                                context_data_val = 0;
                            } else {
                                context_data_position++;
                            }
                            value = value >> 1;
                        }
                    }
                    context_enlargeIn--;
                    if (context_enlargeIn == 0) {
                        context_enlargeIn = Math.pow(2, context_numBits);
                        context_numBits++;
                    }
                    delete context_dictionaryToCreate[context_w];
                } else {
                    value = context_dictionary[context_w];
                    for (i = 0; i < context_numBits; i++) {
                        context_data_val = (context_data_val << 1) | (value & 1);
                        if (context_data_position == bitsPerChar - 1) {
                            context_data_position = 0;
                            context_data.push(getCharFromInt(context_data_val));
                            context_data_val = 0;
                        } else {
                            context_data_position++;
                        }
                        value = value >> 1;
                    }


                }
                context_enlargeIn--;
                if (context_enlargeIn == 0) {
                    context_enlargeIn = Math.pow(2, context_numBits);
                    context_numBits++;
                }
            }

            // Mark the end of the stream
            value = 2;
            for (i = 0; i < context_numBits; i++) {
                context_data_val = (context_data_val << 1) | (value & 1);
                if (context_data_position == bitsPerChar - 1) {
                    context_data_position = 0;
                    context_data.push(getCharFromInt(context_data_val));
                    context_data_val = 0;
                } else {
                    context_data_position++;
                }
                value = value >> 1;
            }

            // Flush the last char
            while (true) {
                context_data_val = (context_data_val << 1);
                if (context_data_position == bitsPerChar - 1) {
                    context_data.push(getCharFromInt(context_data_val));
                    break;
                }
                else context_data_position++;
            }
            return context_data.join('');
        },

        decompress: function (compressed) {
            if (compressed == null) return "";
            if (compressed == "") return null;
            return LZString._decompress(compressed.length, 32768, function (index) { return compressed.charCodeAt(index); });
        },

        _decompress: function (length, resetValue, getNextValue) {
            var dictionary = [],
                next,
                enlargeIn = 4,
                dictSize = 4,
                numBits = 3,
                entry = "",
                result = [],
                i,
                w,
                bits, resb, maxpower, power,
                c,
                data = { val: getNextValue(0), position: resetValue, index: 1 };

            for (i = 0; i < 3; i += 1) {
                dictionary[i] = i;
            }

            bits = 0;
            maxpower = Math.pow(2, 2);
            power = 1;
            while (power != maxpower) {
                resb = data.val & data.position;
                data.position >>= 1;
                if (data.position == 0) {
                    data.position = resetValue;
                    data.val = getNextValue(data.index++);
                }
                bits |= (resb > 0 ? 1 : 0) * power;
                power <<= 1;
            }

            switch (next = bits) {
                case 0:
                    bits = 0;
                    maxpower = Math.pow(2, 8);
                    power = 1;
                    while (power != maxpower) {
                        resb = data.val & data.position;
                        data.position >>= 1;
                        if (data.position == 0) {
                            data.position = resetValue;
                            data.val = getNextValue(data.index++);
                        }
                        bits |= (resb > 0 ? 1 : 0) * power;
                        power <<= 1;
                    }
                    c = f(bits);
                    break;
                case 1:
                    bits = 0;
                    maxpower = Math.pow(2, 16);
                    power = 1;
                    while (power != maxpower) {
                        resb = data.val & data.position;
                        data.position >>= 1;
                        if (data.position == 0) {
                            data.position = resetValue;
                            data.val = getNextValue(data.index++);
                        }
                        bits |= (resb > 0 ? 1 : 0) * power;
                        power <<= 1;
                    }
                    c = f(bits);
                    break;
                case 2:
                    return "";
            }
            dictionary[3] = c;
            w = c;
            result.push(c);
            while (true) {
                if (data.index > length) {
                    return "";
                }

                bits = 0;
                maxpower = Math.pow(2, numBits);
                power = 1;
                while (power != maxpower) {
                    resb = data.val & data.position;
                    data.position >>= 1;
                    if (data.position == 0) {
                        data.position = resetValue;
                        data.val = getNextValue(data.index++);
                    }
                    bits |= (resb > 0 ? 1 : 0) * power;
                    power <<= 1;
                }

                switch (c = bits) {
                    case 0:
                        bits = 0;
                        maxpower = Math.pow(2, 8);
                        power = 1;
                        while (power != maxpower) {
                            resb = data.val & data.position;
                            data.position >>= 1;
                            if (data.position == 0) {
                                data.position = resetValue;
                                data.val = getNextValue(data.index++);
                            }
                            bits |= (resb > 0 ? 1 : 0) * power;
                            power <<= 1;
                        }

                        dictionary[dictSize++] = f(bits);
                        c = dictSize - 1;
                        enlargeIn--;
                        break;
                    case 1:
                        bits = 0;
                        maxpower = Math.pow(2, 16);
                        power = 1;
                        while (power != maxpower) {
                            resb = data.val & data.position;
                            data.position >>= 1;
                            if (data.position == 0) {
                                data.position = resetValue;
                                data.val = getNextValue(data.index++);
                            }
                            bits |= (resb > 0 ? 1 : 0) * power;
                            power <<= 1;
                        }
                        dictionary[dictSize++] = f(bits);
                        c = dictSize - 1;
                        enlargeIn--;
                        break;
                    case 2:
                        return result.join('');
                }

                if (enlargeIn == 0) {
                    enlargeIn = Math.pow(2, numBits);
                    numBits++;
                }

                if (dictionary[c]) {
                    entry = dictionary[c];
                } else {
                    if (c === dictSize) {
                        entry = w + w.charAt(0);
                    } else {
                        return null;
                    }
                }
                result.push(entry);

                // Add w+entry[0] to the dictionary.
                dictionary[dictSize++] = w + entry.charAt(0);
                enlargeIn--;

                w = entry;

                if (enlargeIn == 0) {
                    enlargeIn = Math.pow(2, numBits);
                    numBits++;
                }

            }
        }
    };
    return LZString;
})();


 
// 判断是否是通过服务器页面加载资源内容的。-
// 如果本函数返回True,则ReferencePathForDebug属性无效。
//
function DCIsResourceFromServicePage() {
    return "12345678" === "12345678";
}

// 当客户端以<script type="text/javascript" src="ShowDocumentImageService.aspx?js=1"></script>引用本JS文件内容时。
// 则服务端故意将"% # ServicePage # %"替换为"12345678"。此时DCIsResourceFromServicePage()返回值固定为true.
//
// 如果以<script type="text/javascript" src="ctlReference/DCWriterControl.js"></script>引用JS文件内容时，
// 则本代码不变，则返回值固定为false。


// DCWriter 的ＷＥＢ控件使用的ＪＳ代码，编制袁永福2018-5-24
//debugger;

// 客户端控件支持的属性名组成的数组，注释后面的是默认值。
// 这里列出的属性名是DCSoft.Writer.Controls.WebWriterControlOptions中所有公开的属性名。
// 这里使用代码注释预留一个位置，在DCSoft.Writer.Controls.Web.WC_JS中执行替换操作。
var AttributeNamesForWriterControlOptions = [
	"AdditionPageTitle" , // 类型:System.String
	"AJAXAsync" , // 类型:System.Boolean,默认值:True
	"AllowCopy" , // 类型:System.Boolean,默认值:True
	"AllowDrop" , // 类型:System.Boolean,默认值:True
	"AllowSmallPiecesWhenMergeDocument" , // 类型:System.Boolean,默认值:False
	"AttachedAJAXHeader" , // 类型:System.String
	"AutoFixFontName" , // 类型:System.Boolean,默认值:True
	"AutoGenerateCABFile" , // 类型:System.Boolean,默认值:True
	"AutoGenerateOrderedList" , // 类型:System.Boolean,默认值:False
	"AutoHeightInMobileDevice" , // 类型:System.Boolean,默认值:True
	"AutoLine" , // 类型:System.Boolean,默认值:False
	"AutoPostBack" , // 类型:System.Boolean,默认值:True
	"AutoSaveIntervalInSecond" , // 类型:System.Int32,默认值:0
	"AutoZoom" , // 类型:System.Boolean,默认值:False
	"BackgroundTextOutputMode" , // 类型:DCSoft.Writer.DCBackgroundTextOutputMode,默认值:Output
	"BaseUrl" , // 类型:System.String
	"BindingDataOnlyToBlankField" , // 类型:System.Boolean,默认值:False
	"BrowserStyle" , // 类型:DCSoft.Common.XWebBrowsersStyle,默认值:AutoDetect
	"CABUrl" , // 类型:System.String
	"CABVersion" , // 类型:System.String
	"Chartset" , // 类型:System.String,默认值:gb2312
	"CheckBoxCheckedImageBase64" , // 类型:System.String
	"CheckBoxUnCheckedImageBase64" , // 类型:System.String
	"ClientContextMenuType" , // 类型:DCSoft.Writer.Controls.WebClientContextMenuType,默认值:None
	"ClientID" , // 类型:System.String
	"ClientMachineName" , // 类型:System.String
	"CloneTableContentWithOnlyCleanField" , // 类型:System.Boolean,默认值:False
	"CommentPrintVisible" , // 类型:System.Boolean,默认值:False
	"CommentVisibility" , // 类型:DCSoft.Writer.Controls.FunctionControlVisibility,默认值:Auto
	"CommentVisible" , // 类型:System.Boolean,默认值:False
	"CompressSessionData" , // 类型:System.Boolean,默认值:True
	"ContentEncoding" , // 类型:System.Text.Encoding
	"ContentRenderMode" , // 类型:DCSoft.Writer.Controls.WebWriterControlRenderMode,默认值:PageImage
	"ControlName" , // 类型:System.String
	"CrossDomain" , // 类型:System.Boolean,默认值:False
	"CurrentLoadOptions" , // 类型:DCSoft.Writer.Controls.WebWriterControlLoadDocumentOptions
	"CurrentUserID" , // 类型:System.String
	"CurrentUserName" , // 类型:System.String
	"CurrentUserPermissionLevel" , // 类型:System.Int32,默认值:0
	"CustomCSSString" , // 类型:System.String
	"CustomizeSpanInputElementBorderCssStyle" , // 类型:System.String
	"CustomLogoImage" , // 类型:System.String
	"CustomSessionID" , // 类型:System.Int32
	"DebugMode" , // 类型:System.Boolean,默认值:False
	"DefaultFontName" , // 类型:System.String,默认值:宋体
	"DefaultFontSize" , // 类型:System.Int32,默认值:12
	"DocumentBufferedName" , // 类型:System.String
	"DocumentOptions" , // 类型:DCSoft.Writer.DocumentOptions
	"EnableBrowseCache" , // 类型:System.Boolean,默认值:True
	"EnabledClientContextMenu" , // 类型:System.Boolean,默认值:False
	"EnableEncryptView" , // 类型:System.Boolean,默认值:True
	"EnableLandscapeView" , // 类型:System.Boolean,默认值:True
	"EnablePermission" , // 类型:System.Boolean,默认值:False
	"EnableValueFormat" , // 类型:System.Boolean,默认值:True
	"ExcludeKeywords" , // 类型:System.String
	"ExcludeLogicDeleted" , // 类型:System.Boolean,默认值:False
	"FieldBorderPrintVisibility" , // 类型:DCSoft.Writer.DCRenderVisibility,默认值:Default
	"FixedHeader" , // 类型:System.Boolean,默认值:False
	"ForHtmlEditable" , // 类型:System.Boolean,默认值:False
	"FormView" , // 类型:DCSoft.Writer.Controls.FormViewMode,默认值:Disable
	"ForPrint" , // 类型:System.Boolean,默认值:False
	"FreeSelection" , // 类型:System.Boolean,默认值:False
	"GenerateFormElement" , // 类型:System.Boolean,默认值:False
	"GeneratePrintPreviewPDF" , // 类型:System.Boolean,默认值:False
	"HeaderFooterEditable" , // 类型:System.Boolean,默认值:True
	"HeaderFooterReadOnly" , // 类型:System.Boolean,默认值:False
	"HeaderFooterSelect" , // 类型:System.Boolean,默认值:False
	"HeightCompress" , // 类型:System.Boolean,默认值:True
	"ImageDataEmbedInHtml" , // 类型:System.Boolean,默认值:True
	"IndentHtmlCode" , // 类型:System.Boolean,默认值:False
	"InitJSUsingAJAX" , // 类型:System.Boolean,默认值:False
	"InnerSPBDCS" , // 类型:System.String
	"InsertImageButtonID" , // 类型:System.String
	"isActiveCurrentComment" , // 类型:System.Boolean,默认值:True
	"IsMobileDevice" , // 类型:DCSoft.Writer.Controls.DCOptionMode,默认值:AutoDetect
	"isTransformLandscape" , // 类型:System.Boolean,默认值:True
	"KBLibraryUrl" , // 类型:System.String
	"KeepLineBreak" , // 类型:System.Boolean,默认值:True
	"LicenseServicePageUrl" , // 类型:System.String
	"LocalFileMode" , // 类型:System.Boolean,默认值:False
	"LocalServerPort" , // 类型:System.Int32,默认值:2020
	"LogUserEditTrack" , // 类型:System.Boolean,默认值:False
	"MaxPageCount" , // 类型:System.Int32,默认值:0
	"MinContentPixelHeight" , // 类型:System.Int32,默认值:0
	"MobileLayoutMode" , // 类型:DCSoft.Writer.Controls.WebClientMobileLayoutMode,默认值:None
	"MultiDocument" , // 类型:System.Boolean,默认值:False
	"MultiPage" , // 类型:System.Boolean,默认值:False
	"NarrowBorder" , // 类型:System.Boolean,默认值:False
	"OldBrowser" , // 类型:System.Boolean,默认值:False
	"OutputBase64ServerMessage" , // 类型:System.Boolean,默认值:False
	"OutputFieldBorderChar" , // 类型:System.Boolean,默认值:False
	"OutputHeaderFooter" , // 类型:System.Boolean,默认值:True
	"OutputJavaScript" , // 类型:System.Boolean,默认值:True
	"OutputOriginUserTrackInfos" , // 类型:System.Boolean,默认值:False
	"OutputRowHeight" , // 类型:System.Boolean,默认值:True
	"PageBackColor" , // 类型:System.Drawing.Color,默认值:Color [White]
	"PageName" , // 类型:System.String
	"PageShadow" , // 类型:System.Boolean,默认值:True
	"PageTitlePosition" , // 类型:System.String,默认值:TopLeft
	"PageViewInMobileDevice" , // 类型:System.Boolean,默认值:False
	"PageZoomRate" , // 类型:System.Single,默认值:1
	"PixelPageSpacing" , // 类型:System.Int32,默认值:20
	"PositiveSpecifyWidthFieldAsInputElement" , // 类型:System.Boolean,默认值:False
	"PreviewUseAdvancedLayout" , // 类型:System.Boolean,默认值:False
	"PrintCollate" , // 类型:System.Boolean,默认值:False
	"PrintCopies" , // 类型:System.Int32,默认值:1
	"PrintENSPasWhitespace" , // 类型:System.Boolean,默认值:False
	"PrintMode" , // 类型:DCSoft.Printing.DCPrintMode,默认值:Normal
	"PrintSpecifyPageIndexsString" , // 类型:System.String
	"PrintZoomRate" , // 类型:System.Single,默认值:1
	"RadioBoxCheckedImageBase64" , // 类型:System.String
	"RadioBoxUnCheckedImageBase64" , // 类型:System.String
	"Readonly" , // 类型:System.Boolean,默认值:False
	"ReferencePathForDebug" , // 类型:System.String
	"RegisterCodeIndex" , // 类型:System.Int32,默认值:-1
	"ResourceReferencePath" , // 类型:System.String
	"SerializeParameterValue" , // 类型:System.Boolean,默认值:True
	"ServerEventNameList" , // 类型:System.String
	"ServerSleepForDebug" , // 类型:System.Int32,默认值:0
	"ServicePageURL" , // 类型:System.String
	"SessionTimeout" , // 类型:System.Int32,默认值:0
	"ShowDebugInfo" , // 类型:System.Boolean,默认值:False
	"ShowJSVersionConflictTip" , // 类型:System.Boolean,默认值:True
	"ShowMainDocumentBodyWhenMultiDocument" , // 类型:System.Boolean,默认值:True
	"ShowMarginLine" , // 类型:System.Boolean,默认值:False
	"ShowPageBorderLine" , // 类型:System.Boolean,默认值:True
	"ShowPageLineForEdit" , // 类型:System.Boolean,默认值:False
	"SupportActiveX" , // 类型:System.Boolean,默认值:True
	"SupportFixedWidthInputFieldElement" , // 类型:System.Boolean,默认值:True
	"TidyHtmlForEdit" , // 类型:System.Boolean,默认值:False
	"TidyHtmlForPrint" , // 类型:System.Boolean,默认值:False
	"TransformUseBase64" , // 类型:System.Boolean,默认值:False
	"UrlEncodeCountForPostData" , // 类型:System.Int32,默认值:2
	"UseClassAttribute" , // 类型:System.Boolean,默认值:False
	"UseDCWriterMiniJS" , // 类型:DCSoft.Writer.DCBooleanValueHasDefault,默认值:Default
	"ValidateFieldUsingText" , // 类型:System.Boolean,默认值:False
	"ViewStyle" , // 类型:DCSoft.Writer.Serialization.Html.WriterHtmlViewStyle,默认值:Page
	"WorkspaceBackColor" , // 类型:System.Drawing.Color,默认值:Color [AppWorkspace]
	"WorkspaceBackColorString" , // 类型:System.String
	"WorkspaceBackgroundImage" , // 类型:System.String
	"WriteJSStringResourcesInDocument" , // 类型:System.Boolean,默认值:False
	"WriterControlWebServiceUrl" , // 类型:System.String
	"WriteScriptRefence"  // 类型:System.Boolean,默认值:True

];
 


//@method 绑定元素对象，准备创建编辑器控件客户端。
//@param rootElement HTML元素对象，一般为DIV。
function BindingDCWriterClientControl(rootElement) {
    if (rootElement === null) {
        return false;
    }
    // 设置默认的参数
    rootElement.Options = {
    };

    rootElement.DocumentOptions = {
        "SecurityOptions": {
            "CAMode": "Software",
            "CAServerIP": null,
            "CAServerPort": 0,
            "CreatorTipFormatString": null,
            "DeleterTipFormatString": null,
            "AutoEnablePermissionWhenUserLogin": true,
            "EnablePermission": true,
            "CanModifyDeleteSameLevelContent": true,
            "RealDeleteOwnerContent": false,
            "ShowPermissionTip": true,
            "PowerfulSignDocument": true,
            "EnableLogicDelete": true,
            "ShowLogicDeletedContent": true,
            "ShowPermissionMark": true,
            "TrackVisibleLevel0": {
                "Enabled": true,
                "BackgroundColor": "Transparent",
                "BackgroundColorString": null,
                "UnderLineColor": "LightBlue",
                "UnderLineColorString": "#ADD8E6",
                "UnderLineColorNum": 1,
                "DeleteLineColor": "Coral",
                "DeleteLineColorString": "#FF7F50",
                "DeleteLineNum": 1
            },
            "TrackVisibleLevel1": {
                "Enabled": true,
                "BackgroundColor": "Transparent",
                "BackgroundColorString": null,
                "UnderLineColor": "Blue",
                "UnderLineColorString": null,
                "UnderLineColorNum": 1,
                "DeleteLineColor": "Red",
                "DeleteLineColorString": null,
                "DeleteLineNum": 1
            },
            "TrackVisibleLevel2": {
                "Enabled": true,
                "BackgroundColor": "LightYellow",
                "BackgroundColorString": "#FFFFE0",
                "UnderLineColor": "Blue",
                "UnderLineColorString": null,
                "UnderLineColorNum": 2,
                "DeleteLineColor": "Red",
                "DeleteLineColorString": null,
                "DeleteLineNum": 2
            },
            "TrackVisibleLevel3": {
                "Enabled": true,
                "BackgroundColor": "LightYellow",
                "BackgroundColorString": "#FFFFE0",
                "UnderLineColor": "Blue",
                "UnderLineColorString": null,
                "UnderLineColorNum": 2,
                "DeleteLineColor": "Red",
                "DeleteLineColorString": null,
                "DeleteLineNum": 2
            }
        },
        "ViewOptions": {
            "FieldBorderElementPixelWidth": 1.0,
            "NewInputContentUnderlineColor": "Transparent",
            "NewInputContentUnderlineColorValue": null,
            "SupportUG": false,
            "HiddenTableBorderJumpPrintPage": false,
            "ImageInterpolationMode": "High",
            "EmphasisMarkSize": 10.0,
            "MaskColorForJumpPrint": "#0000FF",
            "MaskColorForJumpPrintValue": null,
            "BothBlackWhenPrint": false,
            "ProtectedContentBackColor": "",
            "ProtectedContentBackColorValue": null,
            "DefaultLineColorForImageEditor": "",
            "DefaultLineColorForImageEditorValue": null,
            "ShowInputFieldStateTag": true,
            "SectionBorderVisibility": "All",
            "TableCellBorderVisibility": "All",
            "ShowPageBreak": false,
            "DefaultAdornTextType": "DataSource",
            "AdornTextVisibility": "Hidden",
            "AdornTextBackColor": "#808080",
            "AdornTextBackColorValue": "#64808080",
            "ShowGrayMaskOverDisableContentParty": true,
            "ShowFormButton": false,
            "PageMarginLineLength": 30,
            "DefaultInputFieldHighlight": "Enabled",
            "HighlightProtectedContent": false,
            "ShowLineNumber": false,
            "UseLanguage2": false,
            "SpecifyExtenGridLineStep": 0.0,
            "GridLineStyle": "Solid",
            "EnableRightToLeft": true,
            "AutoZoomDropdownListFontSize": true,
            "DropdownListFontSize": 0.0,
            "DropdownListFontName": null,
            "ShowBackgroundCellID": false,
            "ShowExpressionFlag": true,
            "CommentFontName": null,
            "CommentFontSize": 10.0,
            "CommentDateFormatString": "yyyy-MM-dd HH:mm",
            "OldWhitespaceWidth": false,
            "ShowGridLine": false,
            "EnableEncryptView": true,
            "GridLineColor": "Gray",
            "GridLineColorValue": null,
            "PreserveBackgroundTextWhenPrint": false,
            "PrintBackgroundText": false,
            "IgnoreFieldBorderWhenPrint": true,
            "PrintGridLine": false,
            "PrintImageAltText": false,
            "ShowHeaderBottomLine": true,
            "HeaderBottomLineWidth": 1.0,
            "ShowCellNoneBorder": true,
            "NoneBorderColor": "LightGrey",
            "NoneBorderColorValue": null,
            "GraphicsSmoothingMode": "None",
            "TextRenderStyle": "ClearTypeGridFit",
            "ShowParagraphFlag": false,
            "HiddenFieldBorderWhenLostFocus": true,
            "ShowFieldBorderElement": true,
            "FieldBorderColor": "",
            "FieldBorderColorValue": null,
            "ShowPageLine": true,
            "RichTextBoxCompatibility": false,
            "MinTableColumnWidth": 50.0,
            "DefaultInputFieldTextColor": "Transparent",
            "DefaultInputFieldTextColorValue": null,
            "EnableFieldTextColor": false,
            "FieldTextColor": "Black",
            "FieldTextColorValue": null,
            "FieldTextPrintColor": "Transparent",
            "FieldTextPrintColorValue": null,
            "ReadonlyFieldBackColor": "LightGrey",
            "ReadonlyFieldBackColorValue": null,
            "FieldBackColor": "AliceBlue",
            "FieldBackColorValue": null,
            "FieldHoverBackColor": "LightBlue",
            "FieldHoverBackColorValue": null,
            "FieldFocusedBackColor": "LightBlue",
            "FieldFocusedBackColorValue": null,
            "FieldInvalidateValueForeColor": "LightCoral",
            "FieldInvalidateValueForeColorValue": null,
            "FieldInvalidateValueBackColor": "LightPink",
            "FieldInvalidateValueBackColorValue": "#FFB6C1",
            "ReadonlyFieldBorderColor": "Gray",
            "TagColorForReadonlyField": "Gray",
            "TagColorForReadonlyFieldValue": null,
            "TagColorForModifiedField": "Blue",
            "TagColorForModifiedFieldValue": null,
            "TagColorForNormalField": "Red",
            "TagColorForNormalFieldValue": "#FF0000",
            "TagColorForValueInvalidateField": "Red",
            "TagColorForValueInvalidateFieldValue": null,
            "ReadonlyFieldBorderColorValue": null,
            "UnEditableFieldBorderColor": "Red",
            "UnEditableFieldBorderColorValue": null,
            "NormalFieldBorderColor": "Blue",
            "NormalFieldBorderColorValue": null,
            "BackgroundTextColor": "Gray",
            "BackgroundTextColorValue": null,
            "SelectionHighlight": "MaskColor",
            "SelectionHightlightMaskColor": "#0000FF",
            "SelectionHightlightMaskColorValue": null,
            "LayoutDirection": "LeftToRight"
        },
        "BehaviorOptions": {
            "AutoClearInvalidateCharacter": true,
            "ShowNoLicenseFunctionRisk": true,
            "EnableAIForSuspiciousContent": false,
            "EncodingCodePageForWriteRTF": 936,
            "AutoSaveScriptAssemblyToTempFile": true,
            "EnabledShowWinTaskBarProgress": true,
            "CompressXMLContent": false,
            "SpeedupByMultiThread": true,
            "LocalAutoSaveWorkDirectory": null,
            "NewExpressionExecuteMode": true,
            "AutoFocusWhenOpenDocument": true,
            "LockScrollPositionWhenStrictFormViewMode": false,
            "CheckedValueBindingHandledForTableRow": true,
            "EnableContentChangedEventWhenLoadDocument": false,
            "EnableCollapseSection": false,
            "MaxTextLengthForPaste": 0,
            "OutputFieldBorderTextToContentText": true,
            "AutoShowScriptErrorMessage": false,
            "AutoClearTextFormatWhenPasteOrInsertContent": false,
            "AutoDocumentValueValidate": false,
            "AutoSaveIntervalInSecond": 0,
            "EnableContentLock": true,
            "MinCountForDropdownListSpellCodeArea": 4,
            "AutoFixElementIDWhenInsertElements": true,
            "MaxZoomRate": 5.0,
            "MinZoomRate": 0.2,
            "RemoveLastParagraphFlagWhenInsertDocument": false,
            "MoveCaretWhenDeleteFail": true,
            "DoubleCompressHtmlWhitespace": true,
            "AllowDeleteJumpOutOfField": true,
            "ContinueHeaderParagrahStyle": false,
            "ActiveCheckInstallWindowsMediaPlayer": false,
            "EnableChineseFontSizeName": true,
            "MaximizedPrintPreviewDialog": false,
            "RaiseQueryListItemsWhenUserEditText": false,
            "AppErrorHandleMode": "ThrowException",
            "XMLContentEncodingName": null,
            "AutoAssistInsertString": false,
            "AutoAssistInsertStringDetectTextLength": 0,
            "AutoTranslateSourceString": null,
            "AutoTranslateDescString": null,
            "AutoScrollToCaretWhenGotFocus": false,
            "MoveFocusHotKey": "Tab",
            "EnabledElementEvent": true,
            "ShowTooltip": true,
            "DoubleClickToEditComment": true,
            "DataObjectRange": "OS",
            "CommentEditableWhenReadonly": false,
            "CommentVisibility": "Auto",
            "AllowDragContent": false,
            "AcceptDataFormats": "All",
            "CreationDataFormats": "All",
            "FormView": "Disable",
            "PreserveClipbardDataWhenExit": false,
            "FastInputMode": false,
            "CompressLayoutForFieldBorder": true,
            "SmoothScrollView": true,
            "AutoActiveSystemTaskbarBeforeShowDialog": false,
            "EnableCalculateControl": true,
            "EnableEditElementValue": true,
            "TitleForToolTip": null,
            "AutoUppercaseFirstCharInFirstLine": false,
            "AutoRefreshUserTrackInfos": false,
            "ValidateIDRepeatMode": "None",
            "IgnorePrintableAreaOffset": false,
            "PageLineUnderPageBreak": false,
            "ParagraphFlagFollowTableOrSection": true,
            "NotificationWorkingTimeout": 2000,
            "HandleCommandException": true,
            "GeneratePageContentVersion": false,
            "DisplayFormatToInnerValue": true,
            "AutoUpdateButtonVisible": false,
            "StdLablesForImageEditor": null,
            "EnableCheckControlLoaded": false,
            "EnableDeleteReadonlyEmptyContainer": true,
            "SimpleElementProperties": false,
            "EnableHyperLink": true,
            "MinImageFileSizeForConfirmCompressSaveMode": 51200,
            "ImageCompressSaveMode": "Prompt",
            "FillCommentToUserTrackList": false,
            "PromptJumpBackForSearch": true,
            "EnableSetJumpPrintPositionWhenPreview": true,
            "ExtendingPrintDialog": true,
            "ImageShapeEditorBackgroundMenuItemConfig": null,
            "MoveFieldWhenDragWholeContent": true,
            "EnableLogUndo": true,
            "ShowDebugMessage": false,
            "EnableCompressUserHistories": true,
            "EnableElementEvents": true,
            "CloneSerialize": true,
            "WeakMode": false,
            "ForceCollateWhenPrint": false,
            "ThreeClickToSelectParagraph": true,
            "DoubleClickToSelectWord": true,
            "EnableKBEntryRangeMask": true,
            "PromptForExcludeSystemClipboardRange": true,
            "PromptForRejectFormat": true,
            "AutoIgnoreLastEmptyPage": true,
            "ValidateUserIDWhenEditDeleteComment": false,
            "InsertCommentBindingUserTrack": false,
            "PowerfulCommentCommand": true,
            "AutoCreateInstanceInProperty": false,
            "GlobalSpecifyDebugModeValue": false,
            "SpecifyDebugMode": false,
            "EnableDataBinding": true,
            "ForcePopupFormTopMost": false,
            "OutputFormatedXMLSource": true,
            "TableCellOperationDetectDistance": 3,
            "WidelyRaiseFocusEvent": false,
            "ExcludeKeywords": null,
            "InsertDocumentWithCheckMRID": "None",
            "DisableCheckMRIDWhenMRIDIsEmptyForOuterDataObject": false,
            "CustomWarringCheckMRID": null,
            "CustomPromptForbitCheckMRID": null,
            "DesignMode": false,
            "EnableControlHostAtDesignTime": true,
            "DebugMode": false,
            "EnableCopySource": true,
            "EnableExpression": true,
            "Printable": true,
            "OutputBackgroundTextToRTF": true,
            "EnableScript": true,
            "DefaultEditorActiveMode": "None",
            "PromptProtectedContent": "Details"
        },
        "EditOptions": {
            "CopyWithoutInputFieldStructure": false,
            "CopyInTextFormatOnly": false,
            "CloneWithoutElementBorderStyle": true,
            "CloneWithoutLogicDeletedContent": false,
            "GridLinePreviewText": "DCWriter是南京都昌信息科技有限公司自主研发的专业的电子病历文档编辑器。",
            "ClearFieldValueWhenCopy": false,
            "KeepTableWidthWhenInsertDeleteColumn": true,
            "FixSizeWhenInsertImage": true,
            "FixWidthWhenInsertImage": true,
            "FixWidthWhenInsertTable": true,
            "TabKeyToFirstLineIndent": true,
            "AutoInsertTableRowWhenMoveToNextCell": true,
            "TabKeyToInsertTableRow": true,
            "ValueValidateMode": "LostFocus"
        }
    };

    // 复制选项属性
    if (rootElement.attributes != null && rootElement.attributes.length > 0) {
        for (var iCount = 0; iCount < rootElement.attributes.length; iCount++) {
            var attrName = rootElement.attributes[iCount].name.toLowerCase();
            for (var iCount2 = 0; iCount2 < AttributeNamesForWriterControlOptions.length; iCount2++) {
                if (AttributeNamesForWriterControlOptions[iCount2].toLowerCase() == attrName) {
                    if (attrName != "documentoptions") {
                        // 对于IE浏览器，会检索到DocumentOptions属性，导致错误。
                        rootElement.Options[AttributeNamesForWriterControlOptions[iCount2]] = rootElement.attributes[iCount].value;
                        break;
                    }
                }
            }
        }
    }

    rootElement.Options.ControlName = rootElement.id;

    //// 获得对象在文档视图中的绝对左边边界。
    //rootElement.GetAbsBoundsInDocument = function () {
    //    return DCDomTools.GetAbsBoundsInDocument(rootElement);
    //};

    //@method 创建编辑器客户端架构
    //@param successCallback 创建成功后的回调函数。
    rootElement.BuildFrame = function (successCallback) {
        this.onloadconentonce = null;
        rootElement.FrameBuilded = true;
        if (DCIsResourceFromServicePage() == true) {
            // 从服务页面加载资源内容，则ReferencePathForDebug属性无效。
            rootElement.Options.ReferencePathForDebug = null;
            rootElement.setAttribute("referencepathfordebug", "");
        }
        if (typeof (JSON) == "undefined") {
            this.appendChild(document.createTextNode("本浏览器不支持JSON对象，本功能无法实现。"));
            return;
        }
        

        DCWriterEnsureJQuery();
        if(!this.Options.ServicePageURL){
            if (typeof (__ServicePageUrl2022) == "string" && __ServicePageUrl2022.length > 0) {
                this.Options.ServicePageURL = __ServicePageUrl2022;
            }
        }
        var svrUrl = this.Options.ServicePageURL;
        if (svrUrl != null && svrUrl.length > 0 ) {
            var index = svrUrl.indexOf("?");
            if (index > 0) {
                this.Options.ServicePageURL = svrUrl.substring(0, index);
            }
        }
        this.setAttribute("ServicePageURL", this.Options.ServicePageURL);
        this.setAttribute("referencepathfordebug", this.Options.ReferencePathForDebug);
        this.setAttribute("contentreandermode", this.Options.ContentRenderMode);
        this.setAttribute("dctype", "WebWriterControl");
        this.setAttribute("insertimagebuttonid", this.Options.InsertImageButtonID);
         

        // 清空所有子元素
        while (this.firstChild != null) {
            this.removeChild(this.firstChild);
        }
        this.appendChild(document.createTextNode("正在初始化DCWriter控件..."));
        this.Options.ControlName = this.id;
        var name1 = "documentoptions.behavioroptions.";
        var name2 = "documentoptions.viewoptions.";
        var name3 = "documentoptions.editoptions.";
        var name4 = "documentoptions.securityoptions.";
        for (var iCount = 0; iCount < this.attributes.length; iCount++) {
            var attrName = this.attributes[iCount].name;
            var attrValue = this.attributes[iCount].value;
            if (DCDomTools.StartsWith(attrName.toLowerCase(), name1) == true) {
                //wyc20220119：使用新判断逻辑
                var attrname = attrName.substr(name1.length);
                for (var i in this.DocumentOptions.BehaviorOptions) {
                    if (i.toLowerCase() === attrname) {
                        this.DocumentOptions.BehaviorOptions[i] = attrValue;
                        break;
                    }
                }
                //this.DocumentOptions.BehaviorOptions[attrName.substr(name1.length)] = attrValue;
            }
            if (DCDomTools.StartsWith(attrName.toLowerCase(), name2) == true) {
                //wyc20220119：使用新判断逻辑
                var attrname = attrName.substr(name2.length);
                for (var i in this.DocumentOptions.ViewOptions) {
                    if (i.toLowerCase() === attrname) {
                        this.DocumentOptions.ViewOptions[i] = attrValue;
                        break;
                    }
                }
                //this.DocumentOptions.ViewOptions[attrName.substr(name2.length)] = attrValue;
            }
            if (DCDomTools.StartsWith(attrName.toLowerCase(), name3) == true) {
                //wyc20220119：使用新判断逻辑
                var attrname = attrName.substr(name3.length);
                for (var i in this.DocumentOptions.EditOptions) {
                    if (i.toLowerCase() === attrname) {
                        this.DocumentOptions.EditOptions[i] = attrValue;
                        break;
                    }
                }
                //this.DocumentOptions.EditOptions[attrName.substr(name3.length)] = attrValue;
            }
            if (DCDomTools.StartsWith(attrName.toLowerCase(), name4) == true) {
                //wyc20220119：使用新判断逻辑
                var attrname = attrName.substr(name4.length);
                for (var i in this.DocumentOptions.SecurityOptions) {
                    if (i.toLowerCase() === attrname) {
                        this.DocumentOptions.SecurityOptions[i] = attrValue;
                        break;
                    }
                }
                //this.DocumentOptions.SecurityOptions[attrName.substr(name4.length)] = attrValue;
            }
        }//for
        var opts = {
            "Options": this.Options,
            "DocumentOptions": this.DocumentOptions
        };

        var jsonText = JSON.stringify(opts);
        //alert(jsonText);
        //console.log(jsonText);
        var svcurl = this.Options.ServicePageURL + "?initstructure=1";
        var settings = {
            url: svcurl,
            async: true,
            method: "POST",
            type: "POST",
            context: this,
            data: jsonText
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        $.ajax(settings).always(
                function (responseText, textStatus, jqXHR) {
                    var sid = null;
                    if (jqXHR && jqXHR.getResponseHeader) {
                        sid = jqXHR.getResponseHeader("dcbid2022");
                    }
                    else if (responseText && responseText.getResponseHeader) {
                        sid = responseText.getResponseHeader("dcbid2022");
                    }
                    if (sid != null && sid.length > 0) {
                        DCDomTools.SetDCSessionID20201022(sid);//window.top.dc_sessionid20201022 = sid;
                    }
                    var sessioninof = DCDomTools.GetDCSessionID20201022()
                    if (sessioninof == null) {
                        this.setAttribute("dcbid2022", "");
                    }
                    else {
                        this.setAttribute("dcbid2022", sessioninof);
                    }
                    if (typeof (responseText) == "object") {
                        
                        responseText = responseText.responseText;
                    } 
                    if (responseText == null || responseText.length == 0) {
                        this.appendChild(document.createTextNode("未构造任何内容,请仔细检查系统配置的ServicePageUrl【"));
                        var ae = document.createElement("a"); ///
                        ae.href = this.Options.ServicePageURL;
                        ae.target = "_blank";
                        ae.innerText = this.Options.ServicePageURL;
                        this.appendChild(ae);
                        this.appendChild(document.createTextNode("】,请确定浏览器能通过网络或网关能访问该地址。"));
                        return;
                    }
                    //console.log(responseText);

                    //while (this.firstChild != null) {
                    //    this.removeChild(this.firstChild);
                    //}
                    var back = this.getAttribute("style");
                    if (textStatus == "success") {
                        if (typeof (successCallback) == "function") {
                            //window.setTimeout(successCallback, 10);
                            this.onloadconentonce = successCallback;
                            //successCallback();
                        }
                        //var sid = jqXHR.getResponseHeader("dcsid");
                        //if (sid != null && sid.length > 0) {
                        //    //this.setAttribute("dcsid", sid);
                        //    this.dcsid = sid;
                        //    this.getDCSID = function () {
                        //        return sid;
                        //    };
                        //}
                    }
                    var strFlag = "dcwriter-version";
                    if (responseText.indexOf(strFlag) < 0) {
                        responseText = "<b>设置错误：请检查ServerPageUrl:" + rootElement.getAttribute("servicepageurl") + "</b>" + responseText;
                    }
                    $(this).html(responseText);

                    //zhangbin20220316 添加用户图片自定义接口
                    var img = $(rootElement).find('img[id$="_Processing"]')[0]
                    if(img){
                        img.style.opacity = 0;
                        var imgFlag = false
                        var src = this.getAttribute('WaitingImg') || this.Options.WaitingImg
                        if(src){
                            imgFlag = true
                            img.src = src
                        }
                        img.onload = function(){
                            img.style.top = ($(img.parentElement).height() - img.height) / 2 + 'px'; 
                            img.style.opacity = 1;
                            if(imgFlag){
                                var div = document.createElement('div')
                                div.setAttribute('id',img.getAttribute('id'))
                                div.setAttribute('style',img.getAttribute('style')+ ';width:'+img.width+'px;height:'+img.height+'px;')            
                                var imgBg = new Image() 
                                imgBg.src = img.src
                                div.appendChild(imgBg)
                            
                                var imgLogo = new Image()
                                imgLogo.src = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAALgAAAA2CAYAAAB0iVNvAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAhdEVYdENyZWF0aW9uIFRpbWUAMjAyMjowMzoxNSAxNDoyMjozNcu5b4YAAAjFSURBVHhe7Zr5V5TXGcefO+87C8OwM+xLAiKgbUOIUdMYXGrRpI1GE2OMMTlJ+0v/n572tCc2TXqOdW3SqFFjSxIjoiwqorKDCDMyrIPAwKy397nzMswIKC5tGc7zOeee927vvBfme5/7vfcdIAiCIAiCIAiCIAiCIAiCIAiCIAiCIAiCIAiCIAiCIAiCIAiCIAiCIIhnA9OuUcEf717kWjYChTEwKwbIMSZCqSUD0o1xEX9XwDfA/e424D4b8IBL1jGdGZiaBYqhGHT6tKj6PxCLZ1kI/EGKzFZ4LakQDCwAPtclCHg6tZb50RkKQTX/XIjeQEJfZixLgSMZBhNsj+kDnX9Yq3k4TLWC3rJ90SK/3dzCPR6PVkLEKmKOgazMDLBYLPN+hsfj5R2dndDvcIDb7YFYsxny83IhPz8v1P9G002elJQEuTnZEZ8xNDTM+2w2WFm0QjzHHNHW0trG9Xo9FBY8v+DYW9vaeUZ6GiQkJCzYZzmi067LDoO7Hnom2rTSo+G+QfBN1WilR1NX3wCNN5qgq/uOTO0dHXDhx2r4/G+H4LvvL3Cf3x8xGfv7HfzQ4SNQfakGvB4vxMSYYHTUCafPnIOTp77hXq9X9u/o7ILLV2rlPeHgsy5W10B3d49WEwTvE8+DwcFBrWYu9nv3+Pl/VcGlmitaDcDwyAg/cuzEogNGtBLVAt+fuQY+yV4HH2a9DFuSi8CiGGW9GcYgkffDsNcFk4FglGU6C6ixFaBP2Af6xPdFfqOsCyfg7oCAd2DRX3pJcTHsfmsHw7Tn7V3st598xLZXbpUirar6XusF4HJN8VPfnMXoCQf274NtlVvZporX2M4dv2LvvrNbiHNIihfBiD4wMCgivDtiHL19NjAYDOLap9UEudffD36/H/Jyc7WauaRZrbB+3VooLy/TagCczjH53OVOVAvcqFPAqOhZrGpkxZZ0tjv9Z1Lkydyu9RCRyjMZFHfcm6AYi5hOMTOdLoYpxhVMjd8xR+R+T6uWezIKCwvYls0boa29Q9oKrLve2CjbXt9WCSaTKcIipKamsHVrX4aWllYZjfPz8oBzDnb7Pa0HwMjoKJ+YmIDyF8ugTwg9HCwbjQbIyspkOCk6u7rlatDdfYfX1tXz3t4+rqoqS01JFqtGjLwH+zgcA6E8JjFJQhOq+04PFysUb7h6jQs7FTHRxLi4mIBiPJP86rVG2Qetl9a85FhWFgWFvjYhD+L4rO92eqdBiSkHFLZWFUIKXbSFw32zwnpS0AsLIUPP3buy3NV1R3pnk8k4r/8tLlkJ+9/fK8ajk4KPjY0F9NszCJGK6B8Pq1eVwLTbDY6B2VUGI3tuTo7M378/DmfOfgtV3/0AZ8+dh9bWdrmaIN8Ki2LrC078uroGYamCG2/MY8JVwOfz8X9+fVramZGRUbCJSfblVyfhx4vVoeddrq2D6prLcPjocWGbbojJ2wTT01Na69Jj2XnwHFMiqODWSgA+7hMbyGytNBedoQAMiR+Ekj5up9bydCQlJcLY2H2Zd46NoXBlfj5URWHx8fFMEVcs5+fnSuHOgALPzc3BCMysqamyjExNT3OxSuAmVZZnQItz4IN9mNjmTRVzJtV7e99hG159JZTHJOwPq2+4BkPDQ7Bv7x5po3b8+g329q6d0HTzNj4zJHKbzQ4vlr0AH390gP3m4w/l2LWmJcey3WQuFsYUxnTGiKQ1PRXCFkAgEAh5aaPwz4sFbQpG0EmXS25WbXZ7yGOj0Ht7g+K3C6GhnUHfHs769WsXPMl5GLebW6C0pATi4mbvTUuzspzsLGm5ZkhOToaXysuWrKjDWXYCt7nHwAfBzSaiMlW+4FkIv7udu0cOhpJn7Ogz8ZMul0t4YyMmpgjrIcSqtTya3JxsaVfQXzv6HcI+BABFJtuEwHFjiT67Twjcak2dc2yYmrLwarEQHo+H45hRyIePHOfhaUBsRnHCzYB+PlqIaoGrTNFyQVw+D7/i7IFxNvsFJ+pN4J+6CgG/a45wA4Epjm3hMDVTyz054+MTHAWRnp4my1arVfrZhRgeHuZ//vQztBtyjGgXMjMzpMDRquD5NdZhW2ZGuhQ/RnVsw2j/IIry+F8rY8E5UrSiADZseCUibd+2FSoqXpXtCD4/WohqgYvIKL+VSZ+bt044+AlHI0z43TDCgtEOSTHEAg9MgG/8pIzWKHQpbHcH993/WraFg6/unwY8jRCbMoyqUPD8c7KutLRYbDS7weGY/wgSvW9srFluMLUqKVzcaN7t7ZVRewY8EcnOyoTmljZwOp3Srz8L9Ho9S0xMxJdRYrXIFrZkNuEmeWhwcS/MlhpRb1Hcfi//wl4HVSPtUtyICxLAyTIgRW+GWF3Q+0qRT14A79jfwes8JPI/zBG3zrjisX6Xgkv64OAQx4Qvcm7daubH//GVjKyVW7dIMWK/1atKUShw8vQZuHW7mU9NTUmhiyjPz53/N8cXRZs2VmBViPy8HFwJ5IYxXOAInpp0dnaBSVigzIyMRY83HLGxlVecdHjkh/myF34qJk4rTkZZxsnacPU6b7p566Gb5KVM1At8ITzGNZBvWamVHg2+qldjgicLi0WIFY4cOyETCru2vgFShD99d89uyM7OihDeG69XQknxSrhYfQkOfvYF/P4Pf+KHDh+VUXjXzjdBROWI/ikpKSzOYpEizkhPj2ibEXzuA5vLxyFLrALic+HYiS/hlJh4yE9Wr2JrXioHMeng04N/RdsE1643wi9/sRnt1hNNpP83UTXoB3+L8ru8DQwj+F9ss6+gkf/Fj63wdISHjWahM+4HwbPmUSFqj9sDcfFxEB8X+cvHcHDjh9cZ/x3O9LSbq6oSWiVmwPr5xoLjVVRVHklqVRIcD572hD8D+46MjgoLqECymLDh9+CY0K+jpdGqljRRMcgZFhL45/Za+rksQRAEQRAEQRAEQRAEQRAEQRAEQRAEQRAEQRAEQRAEQRAEQRAEQRAEQRD/PQD+A3a4+8oyX0u8AAAAAElFTkSuQmCC'
                                imgLogo.setAttribute('style','position:absolute;bottom:-15px;right:-32px')
                                div.appendChild(imgLogo)

                                //wyc20220323:调整写法避免报错
                                // img.parentElement.replaceChild(div, img);
                                img.parentElement.insertBefore(div,img)
                                img.parentElement.removeChild(img)
                            }
                            /////////////////////////////////
                        }
                    }
                    

                    if (back != null) {
                        this.setAttribute("style", back);
                    }
                    if (textStatus == "success") {
                        this.style.overflow = "";
                        if (this.InsertImageByJsonText == null) {
                            StartDCWriterClientControl(this);
                        }
                    }
                    else {
                        this.insertBefore(document.createTextNode(svcurl), this.firstChild);
                        this.style.overflow = "auto";
                    }
                });
    };
};


// 确认是否加载了JQuery库
function DCWriterEnsureJQuery(loadCallback) {
    if (typeof (jQuery) == "undefined") {
        var input = document.getElementById("dcwriter_jquery_url");
        if (input != null) {
            var url = input.value;
            if (url != null && url.length > 0) {
                var winWait = null;
                var oScript = document.createElement("script");
                //oScript.onload = function () { alert("zzzzzzzzzzzzzzzz"); if (winWait != null) { winWait.close(); winWait = null; } };
                oScript.type = "text\/javascript";
                oScript.setAttribute("language", "javascript");
                oScript.setAttribute("async", "false");
                oScript.setAttribute("defer", "false");
                if (typeof (loadCallback) == "function") {
                    oScript.onreadystatechange = function () {
                        if (this.readyState == "complete") {
                            loadCallback();
                        }
                    };
                }
                oScript.src = url;
                if (document.head) {
                    document.head.appendChild(oScript);
                }
                else if (document.body.firstChild != null) {
                    document.body.insertBefore(oScript, document.body.firstChild);
                }
                else {
                    document.body.appendChild(oScript);
                }
                //winWait = window.open("", "_blank","dialog2=yes");
                if (typeof (jQuery) == "undefined") {
                    // 还是没加载成功
                    //alert("DCWriter运行需要jQuery。")
                }
                // 此处存在潜在的隐患，如果用户也是动态加载JQuery，则可能造成冲突。
            }
        }
        return true;
    }
    return false;
};

// 初始化编辑器客户端控件  袁永福2018-6-9
function StartDCWriterClientControl(rootElement) {

    //alert("开始初始化控件");

    if (rootElement == null) {
        return false;
    }

    // 获得浏览器指纹
    rootElement.__globalClientID = Fingerprint2.GetMyFingerprint();
    if(!rootElement.getAttribute("servicepageurl")){
        if (typeof (__ServicePageUrl2022) == "string" && __ServicePageUrl2022.length > 0) {
            rootElement.setAttribute("servicepageurl", __ServicePageUrl2022);
        }
    }
     
    document.WriterControl = rootElement;
    rootElement.setAttribute("dcasmversion", "20230214170251");
    var optNode = document.getElementById(rootElement.id + "_WebWriterControlOptions");
    if (optNode != null) {
        rootElement.WebWriterControlOptions = optNode.value;
    }

    if (rootElement.BuildFrame == null) {
        BindingDCWriterClientControl(rootElement);
    }
    if (DCIsResourceFromServicePage() == true) {
        // 从服务页面加载资源内容，则ReferencePathForDebug属性无效。
        if (rootElement.Options != null) {
            rootElement.Options.ReferencePathForDebug = null;
        }
        rootElement.setAttribute("referencepathfordebug", "");
    }
    if (rootElement.Options != null
        && typeof (rootElement.Options.UseDCWriterMiniJS) == "string"
        && rootElement.Options.UseDCWriterMiniJS.toLowerCase() == "true") {
        // 使用加密JS代码，则ReferencePathForDebug属性也无效。
        if (rootElement.Options != null) {
            rootElement.Options.ReferencePathForDebug = null;
        }
        rootElement.setAttribute("referencepathfordebug", "");
    }
    // rootElement.delayFun = window.setInterval(function () {
    //     var frame = document.getElementById(rootElement.id + "_PreviewFrame");
    //     if (frame != null
    //         && frame.offsetHeight > 0
    //         && rootElement.ToolbarForPrintPreview != null
    //         && rootElement.ToolbarForPrintPreview.offsetHeight > 0) {
    //         var h = rootElement.clientHeight - rootElement.ToolbarForPrintPreview.offsetHeight - 1;
    //         if (frame.lastHeight != h) {
    //             frame.lastHeight = h;
    //             frame.style.height = h + "px";
    //             clearInterval(rootElement.delayFun);
    //         }
    //     }
    // }, 100);


    //是否记录用户痕迹
    rootElement.IsLogUserEditTrack = function () {
        var v = this.getAttribute("LogUserEditTrack");
        if (v == "true" || v == "TRUE" || v == "True") {
            return true;
        }
        else {
            return false;
        }
    };

    // dom dom节点
    // xuyiming 20200103 添加重新加载处理dom下面的子节点,参数默认是整个html
    rootElement.InitFileContentDom = function (dom) {
        var win = rootElement.GetContentWindow();
        var DOM = dom ? dom : rootElement.GetContentDocument();
        if (win != null && DOM != null && win.WriterCommandModuleFormat) {
            $(DOM).find("[dctype='XTextInputFieldElement']").each(function () {
                if ($(this).find("[dctype='XTextInputFieldElement'], input, img, br").length == 0) {
                    $(this).attr("empty", rootElement.GetElementTextByID(this) != "" ? "" : 1);
                } else {
                    $(this).removeAttr("empty");
                }
            })
            return win.DCWriterControllerEditor.InitFileContentDom(DOM, true);
        }
    }

    // 开始执行格式刷功能
    rootElement.BeginFormatBrush = function (usePstyle) {
        var win = rootElement.GetContentWindow();
        if (win != null && win.WriterCommandModuleFormat) {
            return win.WriterCommandModuleFormat.BeginFormatBrush(usePstyle);
        }
    };

    // 插入文档批注
    rootElement.NewComment = function (options) {
        var win = rootElement.GetContentWindow();
        if (win != null && win.DCDocumentCommentManager) {
            return win.DCDocumentCommentManager.NewComment(options);
        }
    };

    // 获取文档所有批注列表
    rootElement.getCommentList = function() {
        return win.DCDocumentCommentManager.spanArr
    }

    //修改批注接口
    rootElement.setCommentContent = function(index,content) {
        return win.DCDocumentCommentManager.setCommentContent(index,content)
    }

    //删除批注接口
    rootElement.deleteComment = function(index) {
        return win.DCDocumentCommentManager.DeleteComment2(index)
    }

   /**
    * @method 获取批注内容或者修改批注内容
    * @param element 批注元素
    * @param changeOption 第二个参数，为空时获取批注内容，有值时修改批注内容；
    * @returns changeOption为空 返回值和第二个参数格式都为以下所示：
    * {
        "Element": "",//元素本身
        "CommentText": "",//批注内容
        "CommentAuthor": "",//批注作者
        "CommentCreationTime": "",//批注创建时间
    * }
    * @returns changeOption有值 true:完成了操作。false:没有完成操作。
    */
    rootElement.CommentOption = function (element, changeOption) {
        var isComment = element && element.nodeName && element.nodeName == "SPAN" && element.getAttribute("dctype") == "DocumentComment";
        if (isComment == false) {
            return false;
        }
        var win = rootElement.GetContentWindow();
        if(!win){
            return false;
        }
        win.DCDropdownControlManager && win.DCDropdownControlManager.CloseDropdownControl();
        var option = {
            "Element": element,//元素本身
            "CommentText": element.getAttribute("commenttext"),//批注内容
            "CommentAuthor": element.getAttribute("commentauthor"),//批注作者
            "CommentCreationTime": element.getAttribute("commentcreationtime"),//批注创建时间
        };
        if (!changeOption) {
            return option;
        }
        var isChange = false;//是否修改
        for (var key in option) {
            if (Object.hasOwnProperty.call(changeOption, key)) {
                if (key != "Element" && changeOption[key] != option[key]) {
                    element.setAttribute(key.toLowerCase(), changeOption[key]);
                    isChange = true;
                }
            }
        }
        if(isChange){
            element.title = element.getAttribute("commentauthor") + "," + element.getAttribute("commentcreationtime") + "\r\n" + element.getAttribute("commenttext");
            win.DCUndoRedo && win.DCUndoRedo.RaiseChanged();
        }
        return isChange;
    }

    /**
    * @method 删除批注
    * @param element 批注元素
    * @returns true:完成了操作。false:没有完成操作。
    */
    rootElement.DeleteComment = function (element) {
        var isComment = element && element.nodeName && element.nodeName == "SPAN" && element.getAttribute("dctype") == "DocumentComment";
        if (isComment == false) {
            return false;
        }
        var win = rootElement.GetContentWindow();
        if (win) {
            element.removeAttribute("dctype");
            element.removeAttribute("refcommentindex");
            element.removeAttribute("title");
            element.removeAttribute("commenttext");
            element.removeAttribute("commentauthor");
            element.removeAttribute("commentcreationtime");
            element.removeAttribute("combk");
            element.removeAttribute("combk2");
            element.removeAttribute("style");
            win.DCDropdownControlManager && win.DCDropdownControlManager.CloseDropdownControl();
            win.DCUndoRedo && win.DCUndoRedo.RaiseChanged();
            return true;
        }
        return false;
    }

    //@method 设置边框打印可见性和颜色
    //@param where 修改什么 tableID,或者[tableID,row数,cell数]
    //@param option 对象 包括bordersetting,color
    /* bordersetting: {
        BorderLeft: true,
        BorderTop: true,
        BorderBottom: true,
        BorderRight: true,
     },
    */
    //@returns true:完成了操作。false:没有完成操作。
    rootElement.setTableBorderVisible = function (where, option) {
        var win = rootElement.GetContentWindow();
        if (win != null && win.WriterCommandModuleTable) {
            return win.WriterCommandModuleTable.setTableBorderVisible(where, option);
        }
        return false;
    };

    //@method 执行黏贴操作。
    rootElement.DCPaste = function () {
        var doc = rootElement.GetContentDocument();
        if (doc != null) {
            window.setTimeout(function () {
                DCDomTools.FoucsDocument(doc);
                //doc.designMode = "on";
                var result = doc.execCommand("paste", true);
                if (result == false) {
                    //window.alert(rootElement.GetDCWriterString("JS_WarringPaste"));
                    var eventObject = new Object();
                    eventObject.Message = rootElement.GetDCWriterString("JS_WarringPaste");
                    eventObject.State = rootElement.ErrorInfo.NotSupportedError;
                    rootElement.MessageHandler(eventObject);
                }
                //doc.designMode = "false";
            }, 50);
        }
    };

    rootElement.AboutControl = function () {
        var frame = rootElement.ShowMaskDialog();
        if (frame != null) {
            var rnd = new Date().valueOf();
            //var txt = DCDomTools.GetContentByUrlNotAsync(rootElement.getAttribute("servicepageurl") + "?dcaboutcontrol=" + rnd);
            frame.src = rootElement.getAttribute("servicepageurl") + "?dcaboutcontrol=" + rnd;
        }
    };



    //rootElement.Undo = function(){
    //    rootElement.DCExecuteCommand("undo");
    //};
    //rootElement.Redo = function () {
    //    rootElement.DCExecuteCommand("redo");
    //};
    //@method 下载当前编辑的文档为本地文件
    //@param strFormat 文件格式。目前支持的文件格式有 xml , pdf , rtf , txt ,html , longimage(长图片文件)
    rootElement.DownLoadFile = function (strFormat, filename) {
        var frm = document.createElement("form");
        if (strFormat == null || strFormat.length == 0) {
            strFormat = "xml";
        }
        frm.action = rootElement.getAttribute("servicepageurl") + "?getfilecontentspecifyformat=" + strFormat;
        //wyc20210903:添加文件名
        if (filename != null) {
            frm.action = frm.action + "&filename=" + filename.toString();
        }
        frm.action = DCDomTools.appendSessionIDToUrl(frm.action);
        frm.method = "POST";
        frm.target = "_blank";
        var field = document.createElement("input");
        field.type = "hidden";
        field.name = "filecontent";
        var postData = this.GetContentHtml(true, true, true);
        field.value = postData;
        frm.appendChild(field);
        document.body.appendChild(frm);
        frm.submit();
        document.body.removeChild(frm);
    };

    //wyc20221024:新增设置控件只读的接口
    rootElement.SetControlReadonly = function (isrd) {
        var isreadonly = null;
        if (isrd === "true" || isrd === true) {
            isreadonly = true;
        } else if (isrd === "false" || isrd === false) {
            isreadonly = false;
        }
        var doc = rootElement.contentWindow != null ? rootElement.contentWindow.document : rootElement.contentDocument;
        if (doc == null || isreadonly == null || rootElement.Options == null) {
            return;
        }
        if (isreadonly === true) {
            doc.body.style.pointerEvents = "none";
            rootElement.Options.Readonly = true;
        } else {
            doc.body.style.pointerEvents = "auto";
            rootElement.Options.Readonly = false;
        }
    }

    //@method 设置快捷菜单
    //@param jsonContextMenu json格式的快捷菜单定义对象
    rootElement.SetContextMenu = function (jsonContextMenu) {
        rootElement.contextMenu = jsonContextMenu;
        var win = this.GetContentWindow();
        if (win != null && win.UEOptions) {
            if (win.document.runtimeEnableContextMenu == false) {
                //alert(rootElement.GetDCWriterString("JS_FailSetContextMenu"));// "无法设置快捷菜单，必须在页面HTML中静态的设置容器DIV元素的ClientContextMenuType属性值。");
                var eventObject = new Object();
                eventObject.Message = rootElement.GetDCWriterString("JS_FailSetContextMenu");
                eventObject.State = rootElement.ErrorInfo.NotSupportedError;
                rootElement.MessageHandler(eventObject);
                return false;
            }
            var menu = win.UEOptions.contextMenu;
            if (menu != null) {
                while (menu.length > 0) {
                    menu.pop();
                }
                if (jsonContextMenu instanceof Array) {
                    for (var iCount = 0; iCount < jsonContextMenu.length; iCount++) {
                        menu.push(jsonContextMenu[iCount]);
                    }
                }
                return true;
            }
        }
        return false;
    };

    //设置预览页面中的自定义快捷菜单
    rootElement.SetPreviewContextMenu = function (jsonContextMenu) {
        rootElement.previewContextMenu = jsonContextMenu;
        var win = this.GetPreviewContentDocument();
        if (win != null && win.UEOptions) {
            if (win.document.runtimeEnableContextMenu == false) {
                //alert(rootElement.GetDCWriterString("JS_FailSetContextMenu"));// "无法设置快捷菜单，必须在页面HTML中静态的设置容器DIV元素的ClientContextMenuType属性值。");
                var eventObject = new Object();
                eventObject.Message = rootElement.GetDCWriterString("JS_FailSetContextMenu");
                eventObject.State = rootElement.ErrorInfo.NotSupportedError;
                rootElement.MessageHandler(eventObject);
                return false;
            }
            var menu = win.UEOptions.contextMenu;
            if (menu != null) {
                while (menu.length > 0) {
                    menu.pop();
                }
                if (jsonContextMenu instanceof Array) {
                    for (var iCount = 0; iCount < jsonContextMenu.length; iCount++) {
                        menu.push(jsonContextMenu[iCount]);
                    }
                }
                return true;
            }
        }
        return false;
    };

    //@method 设置注册码序号。
    //@param index 序号
    //@returns 一个布尔值，表示操作是否成功
    rootElement.SetRegisterCodeIndex = function (index) {
        var url = rootElement.getAttribute("servicepageurl");
        if (url != null && url.length > 0) {
            url = url + "?setregistercodeindex=" + index;
            var result = DCDomTools.PostContentByUrlNotAsync(url, false, "");
            if (result == true && rootElement.Options != null) {
                rootElement.Options.RegisterCodeIndex = index;
            }
            return result;
        }
        return false;
    };

    //@method 设置注册码
    //@param strCode 新注册码。
    //@returns 一个布尔值，表示操作是否成功
    rootElement.SetRegisterCode = function (strCode) {
        var win = this.GetContentWindow();
        if (win != null && win.DCWriterControllerEditor != null) {
            win.DCWriterControllerEditor.SetRegisterCode(strCode);
            return true;
        }
        return false;
    };

    //@method 判断编辑器是否运行在移动设备中。
    rootElement.IsMobileDevice = function () {
        var doc = this.GetContentDocument();
        if (doc != null && doc.body != null && doc.body.getAttribute("ismobiledevice") == "true") {
            return true;
        }
        else {
            return false;
        }
    };

    // 返回字体大小和字体名称
    // 返回一个对象，fontSize 字体大小 单位为px，fontFamily 字体名称
    // {fontSize: "16px", fontFamily: "楷体"} 
    rootElement.getFontObject = function () {
        var win = rootElement.GetContentWindow();
        if (win != null && win.WriterCommandModuleFormat) {
            var fontSize = win.WriterCommandModuleFormat.getFontSize();
            var fontFamily = win.WriterCommandModuleFormat.getFontFamily();
            return {
                "fontSize": fontSize,
                "fontFamily": fontFamily
            }
        }
    };

    rootElement.GetChartElementDataByID = function (id) {
        var doc = rootElement.GetContentDocument();
        var element = doc.getElementById(id);
        if (element === null ||
            element.getAttribute == undefined ||
            (element.getAttribute("dctype") !== "XTextChartElement" &&
            element.getAttribute("dctype") !== "XTextPieElement")) {
            console.log("SetChartElementDataByID：元素没有找到");
            return false;
        }
        
        var obj = JSON.parse(element.getAttribute("chartdatas"));
        return obj;
    };

    rootElement.SetChartElementDataByID = function (id, obj) {
        var doc = rootElement.GetContentDocument();
        var element = doc.getElementById(id);
        if (element === null ||
            element.getAttribute == undefined ||
            (element.getAttribute("dctype") !== "XTextChartElement" &&
            element.getAttribute("dctype") !== "XTextPieElement")) {
            console.log("SetChartElementDataByID：元素没有找到");
            return false;
        }
        var win = rootElement.GetContentWindow();
        if (win != null && win.DCChartManager) {
            element.setAttribute("chartdatas", JSON.stringify(obj));
            win.DCChartManager.UpdateChartElement(element);
            return true;
        }
        return false;
    };

    //@method 获得指定编号的元素的文本内容
    //@param id 元素编号
    rootElement.GetElementTextByID = function (id, specifySubDocID, func) {
        var win = this.GetContentWindow();
        var returnStr,isHasTag;
        if (func && typeof (func) == "function") {
            isHasTag = false;
        }
        if (win != null && win.DCWriterControllerEditor) {
            //20191108 xuyiming 解决DCWRITER-2931 当报告处于预览状态的时候,调用GetElementTextByID接口获取到的文字只有一行
            if (rootElement.IsPrintPreview() == true) {
                if (typeof (id) == 'string') {
                    var str = "";
                    var iframePreview = document.getElementById(this.id + "_PreviewFrame");
                    var doc = iframePreview.contentWindow.document || iframePreview.contentDocument;
                    var $nodes = $(doc).find("[id='" + id + "']");
                    var lastNode = $nodes[$nodes.length - 1];
                    $nodes.each(function () {
                        str += win.DCWriterControllerEditor.GetElementText(this, specifySubDocID, func);
                        if (isHasTag && this != lastNode) {
                            str += "\n";
                        }
                    });
                    returnStr = str;
                } else {
                    returnStr = win.DCWriterControllerEditor.GetElementText(id, specifySubDocID, func);
                }
            } else {
                returnStr = win.DCWriterControllerEditor.GetElementText(id, specifySubDocID, func);
            }
        }
        // 添加GetElementTextByID获取不到元素,返回null
        if (returnStr == null) {
            return null;
        }
        return returnStr;
    };

    //@method 获得文档中所有的输入域元素
    //@param excludeReadonly :是否排除只读输入域
    //@param excludeHiddenElement :是否排除隐藏输入域
    //@param specifyRootElement :指定父节点下查找所有输入域wyc20210708
    //@param nestNode :获取到的节点是否嵌套显示 zhangbin20220602
    rootElement.GetAllInputFields = function (excludeReadonly, excludeHiddenElement, specifyRootElement, nestNode) {
        var win = rootElement.GetContentWindow();
        if (win != null && win.DCInputFieldManager) {
            if (specifyRootElement) {
                return win.DCInputFieldManager.getAllInputFieldsSpecifyRoot(specifyRootElement, excludeReadonly, excludeHiddenElement, nestNode);
            } else {
                return win.DCInputFieldManager.getAllInputFields(excludeReadonly, excludeHiddenElement, nestNode);
            }
        }
        return null;
    };

    //@method 获取文档元素的自定义属性 wyc20210708
    //@param element :要设置的元素或元素ID
    rootElement.GetElementCustomAttributes = function (element) {
        var ele = null;
        if (typeof (element) == "string") {
            var doc = rootElement.GetContentDocument();
            ele = doc.getElementById(element);
        } else if (element.nodeName) {
            ele = element;
        }
        var win = this.GetContentWindow();
        if (win != null && win.DCDomTools && win.DCDomTools.IsDCWriterElement(ele) === true) {
            return win.DCDomTools.GetElementCustomAttributes(ele);
        } else {
            return null;
        }
    };

    //@method 设置文档元素的自定义属性 wyc20210708
    //@param element :要设置的元素或元素ID
    //@param obj :JSON对象
    rootElement.SetElementCustomAttributes = function (element, obj) {
        var ele = null;
        if (typeof (element) == "string") {
            var doc = rootElement.GetContentDocument();
            ele = doc.getElementById(element);
        } else if (element.nodeName) {
            ele = element;
        }
        var win = this.GetContentWindow();
        if (win != null && win.DCDomTools && win.DCDomTools.IsDCWriterElement(ele) === true) {
            return win.DCDomTools.SetElementCustomAttributes(ele, obj);
        } else {
            return false;
        }
    };


    //@method 获取文档的自定义属性 wyc20221017
    rootElement.GetDocumentCustomAttributes = function () {
        var doc = this.GetContentDocument();
        var win = this.GetContentWindow();
        if (doc != null && doc.body != null && doc.body.getAttribute && win && win.DCDomTools) {
            var str = doc.body.getAttribute("dcdocinfo");
            str = str.replace(/&quot;/g, "\"");
            var obj = JSON.parse(str);
            if (obj != null) {
                var attrstring = obj.DocumentAttributes;
                return win.DCDomTools.ParseAttributeToObject(attrstring);
            } else {
                return null;
            }
            //return JSON.parse(str);
        } else {
            return null;
        }
    };

    //@method 设置文档的自定义属性 wyc20221017
    //@param obj :JSON对象
    rootElement.SetDocumentCustomAttributes = function (obj) {
        if (typeof (obj) !== "object") {
            return false;
        }
        var resultstr = "";
        for (var i in obj) {
            var name = i.length == 0 || i == null ? "" : i.toString();
            var value = obj[i].length == 0 || obj[i] == null ? "" : obj[i].toString();
            resultstr = resultstr + name + ":" + value + ";";
        }
        if (resultstr.length > 0) {
            resultstr = resultstr.substr(0, resultstr.length - 1);
        }
        var settings = this.GetDocumentPageSettings();
        settings.DocumentAttributes = resultstr;
        this.ChangeDocumentSettings(settings);
    };

    // 20220812 xym 添加修改和获取BS自定义表格边框样式的接口
    rootElement.CustomBorderOnlyInWeb = function (element, dcCustomBorder){
        if(!element){
            return;
        }
        var node;
        if (typeof (element) == "string") {
            var doc = rootElement.GetContentDocument();
            node = doc.getElementById(element);
        } else if (element.nodeName) {
            node = element;
        }
        var win = this.GetContentWindow();
        if (win != null && win.WriterCommandModuleTable && win.WriterCommandModuleTable.CustomBorderOnlyInWeb) {
            return win.WriterCommandModuleTable.CustomBorderOnlyInWeb(node, dcCustomBorder);
        } else {
            return {};
        }
    }

    //@method 获取标签元素的连接模式设置 wyc20210818
    //@param element :要设置的元素或元素ID
    rootElement.GetLabelElementContactSettings = function (element) {
        var ele = null;
        if (typeof (element) == "string") {
            var doc = rootElement.GetContentDocument();
            ele = doc.getElementById(element);
        } else if (element.nodeName) {
            ele = element;
        }
        var win = this.GetContentWindow();
        if (win != null && win.DCDomTools && ele.getAttribute && ele.getAttribute("dctype") == "XTextLabelElement") {
            return win.DCDomTools.GetLabelElementContactSettings(ele);
        } else {
            return null;
        }
    };

    //@method 设置标签元素的连接模式 wyc20210818
    //@param element :要设置的元素或元素ID
    //@param obj :JSON对象
    rootElement.SetLabelElementContactSettings = function (element, obj) {
        var ele = null;
        if (typeof (element) == "string") {
            var doc = rootElement.GetContentDocument();
            ele = doc.getElementById(element);
        } else if (element.nodeName) {
            ele = element;
        }
        var win = this.GetContentWindow();
        if (win != null && win.DCDomTools && ele.getAttribute && ele.getAttribute("dctype") == "XTextLabelElement") {
            return win.DCDomTools.SetLabelElementContactSettings(ele, obj);
        } else {
            return false;
        }
    };

    //@method 设置文档元素的内容锁定 wyc20210713
    //@param element :要设置的元素或元素ID
    //@param obj :JSON对象
    rootElement.SetElementContentLock = function (element, obj) {
        var win = rootElement.GetContentWindow();
        if (win && win.DCDomTools) {
            win.DCDomTools.SetElementContentLock(element, obj);
        }
    };

    //@method 设置文档元素的可见性 wyc20210708
    //@param element :要设置的元素或元素ID
    //@param visible :元素的可见性
    rootElement.SetElementVisibility = function (element, visible) {
        if (visible !== true && visible !== false && visible !== "true" && visible !== "false") {
            return;
        }
        var isshow = visible === true || visible === "true" ? true : false;
        var ele = null;
        if (typeof (element) == "string") {
            var doc = rootElement.GetContentDocument();
            ele = doc.getElementById(element);
        } else if (element.nodeName) {
            ele = element;
        }
        if (ele != null && ele.hasAttribute && ele.hasAttribute("dctype") == true) {
            //wyc20211222:区别处理多选框或单选框 
            var dctype = ele.getAttribute("dctype");
            var isradioorcheckbox = dctype === "XTextCheckBoxElement" || dctype === "XTextRadioBoxElement";
            if (isradioorcheckbox == true) {
                ele = ele.parentElement;//取上级的span元素来隐藏显示
            }
            //////////////////////////////////////
            if (isshow === false) {
                $(ele).hide();
                ele.setAttribute("dc_visible", "false");
            } else {
                $(ele).show();
                ele.setAttribute("dc_visible", "true");
            }
        }
    };

    //@method 设置文档元素的打印可见性 wyc20210820
    //@param element :要设置的元素或元素ID
    //@param visible :元素的打印可见性，合法参数只有三个"Visible","Hidden","None"
    rootElement.SetElementPrintVisibility = function (element, visible) {
        var parameters = ["Visible", "Hidden", "None"];
        if (parameters.indexOf(visible) == -1) {
            return;
        }
        var ele = null;
        if (typeof (element) == "string") {
            var doc = rootElement.GetContentDocument();
            ele = doc.getElementById(element);
        } else if (element.nodeName) {
            ele = element;
        }
        if (ele != null && ele.hasAttribute && ele.hasAttribute("dctype") == true) {
            ele.setAttribute("dc_printvisibility", visible);
        }
    };

    //@method 获取文档元素的打印可见性 wyc20210820
    //@param element :要设置的元素或元素ID
    rootElement.GetElementPrintVisibility = function (element) {
        var ele = null;
        if (typeof (element) == "string") {
            var doc = rootElement.GetContentDocument();
            ele = doc.getElementById(element);
        } else if (element.nodeName) {
            ele = element;
        }
        var result = null;
        if (ele != null && ele.hasAttribute && ele.hasAttribute("dctype") == true) {
            result = ele.getAttribute("dc_printvisibility");
            if (result == null || result.length == 0) {
                result = "Visible";
            }
        }
        return result;
    };

    //@method 获得指定编号的元素的文本内容
    //@param id 元素编号，或者文档元素本身。
    //@param newText 新的文本值
    //@returns 返回一个布尔值，表示操作是否成功。
    rootElement.SetElementTextByID = function (id, newText, subdoc) {
        var win = this.GetContentWindow();
        if (win != null && win.DCWriterControllerEditor && newText != null) {
            newText = newText.toString();
            //BSDCWRIT-186
            var arr = newText.split('\n');
            var str = ""; 
            for (var i = 0; i < arr.length; i++) {
                str += arr[i] + "<br>";
            }
            str = str.substring(0, str.length - 4);
            return win.DCWriterControllerEditor.SetElementText(id, str, subdoc);
        }
        return "";
    };

    rootElement.SetElementCheckedByID = function (id, checked) {
        var win = this.GetContentWindow();
        if (win != null && win.DCWriterControllerEditor) {
            return win.DCWriterControllerEditor.SetElementCheckedByID(id, checked);
        }
        return false;
    };

    /**
    * @method 修改单复选框的Text和Value值
    * @param input id值或者元素本身
    * @param opt 数据为{"text":"","value":""}
    */
    rootElement.SetCheckboxOrRadioTextAndValue = function (input, opt) {
        var win = this.GetContentWindow();
        if (win != null && win.DCInputFieldManager) {
            return win.DCInputFieldManager.SetCheckboxOrRadioTextAndValue(input, opt);
        }
    }

    //@method 通过给定下拉项的值选中下拉列表中的项 wyc20210812
    //@param id 元素编号，可以是元素本身
    //@param valuestring 下拉项中的值
    rootElement.SetFieldDropListItemByValue = function (id, valuestring) {
        var win = this.GetContentWindow();
        var doc = this.GetContentDocument();
        var element = null;
        if (typeof (id) == "string") {
            element = doc.getElementById(id);
        } else {
            element = id;
        }
        if (win != null && win.DCSelectionManager && element == null) {
            element = win.DCSelectionManager.GetCurrentInputField();
        }
        var isfield = win != null && win.DCInputFieldManager.IsInputFieldElement(element) === true ? true : false;
        if (win != null &&
            win.DCWriterControllerEditor &&
            isfield === true &&
            typeof (valuestring) == "string") {
            return win.DCWriterControllerEditor.SetFieldDropListItemByValue(element, valuestring);
        }
    };

    //wyc20220701:获取表格单元格的单元格网格线
    rootElement.GetTableCellGridLineInfo = function (cell) {
        var win = this.GetContentWindow();
        if(win != null && !cell){
            cell = $(win.getSelection().baseNode).parents('td:first')[0]
        }
        if(cell){
            if (win == null || win.DCDomTools == null || win.WriterCommandModuleTable == null) {
                return null;
            }
            var ownerTable = DCDomTools.getParentSpecifyNodeName(cell, "TABLE");
            if (ownerTable.getAttribute("dctype") === "XTextTableElement") {
                return win.WriterCommandModuleTable.GetTableCellGridLineInfo(cell);
            } else {
                return null;
            }
        }else{
            return false
        }
        
    };
    //wyc20220701:设置表格单元格的单元格网格线
    rootElement.SetTableCellGridLineInfo = function (cell, options, isSelected) {
        var win = this.GetContentWindow();
        if(win != null && !cell){
            cell = $(win.getSelection().baseNode).parents('td:first')[0]
        }
        if(cell){
            if(isSelected == 'isSelected'){
            }else{
                if(win.WriterCommandModuleTable.newUETable && $.inArray(cell,win.WriterCommandModuleTable.newUETable.selectedTds) >= 0){
                    for(var i=0;i<win.WriterCommandModuleTable.newUETable.selectedTds.length;i++){
                        rootElement.SetTableCellGridLineInfo(win.WriterCommandModuleTable.newUETable.selectedTds[i],options,'isSelected')
                    }
                    return
                }
            }
            if (win == null || win.DCDomTools == null || win.WriterCommandModuleTable == null) {
                return false;
            }
            var ownerTable = DCDomTools.getParentSpecifyNodeName(cell, "TABLE");
            if (ownerTable.getAttribute("dctype") === "XTextTableElement") {
                return win.WriterCommandModuleTable.setTableCellGridLineInfo(cell, options);
            } else {
                return false;
            }
        }else{
            return false
        }
        
    };

    //zhangbin20220704  获取表格单元格的数据表达式
    rootElement.GetTableCellExpression = function (cell) {
        var win = this.GetContentWindow();
        if(win != null){
            return win.WriterCommandModuleTable.TableConfig('GetTableCellExpression',cell)
        }
    };

    //zhangbin20220704  设置表格单元格的数据表达式
    rootElement.SetTableCellExpression = function (cell,expression,isSelected) {
        var win = this.GetContentWindow();
        if(win != null){
            return win.WriterCommandModuleTable.TableConfig('SetTableCellExpression',cell,expression,isSelected)
        }
    };

    //zhangbin20220721 获取单元格展示位置
    rootElement.GetTableCellDirection = function(cell){
        var win = this.GetContentWindow();
        if(win != null){
            return win.WriterCommandModuleTable.TableConfig('GetTableCellDirection',cell)
        }
    }

    // zhangbin20220720 设置单元格展示位置
    rootElement.SetTableCellDirection = function(cell,direction,isSelected){
        var win = this.GetContentWindow();
        if(win != null){
            return win.WriterCommandModuleTable.TableConfig('SetTableCellDirection',cell,direction,isSelected)
        }
    }

    //@method 获得指定编号的元素的innervalue内容(dc_innervalue)
    //@param id 元素编号
    rootElement.GetElementInnerValueStringByID = function (id) {
        var win = this.GetContentWindow();
        if (win != null && win.DCWriterControllerEditor) {
            return win.DCWriterControllerEditor.GetElementInnerValueStringByID(id);
        }
        return "";
    };

    //@method 设置指定编号的元素的innervalue内容(dc_innervalue)
    //@param id 元素编号，或者文档元素本身。
    //@param newText 新的文本值
    //@param newValue 新的value值
    //输入两个值只修改value，输入三个值修改value和text
    //@returns 返回一个布尔值，表示操作是否成功。
    rootElement.SetElementInnerValueStringByID = function (id, newValue, newText) {
        var win = this.GetContentWindow();
        if (win != null && win.DCWriterControllerEditor) {
            return win.DCWriterControllerEditor.SetElementInnerValueString(id, newValue, newText);
        }
        return "";
    };

    //@method 获得被选中的文本内容.
    rootElement.SelectionText = function (clearBorder) {
        var win = this.GetContentWindow();
        if (win != null && win.WriterCommandModuleFile) {
            return win.WriterCommandModuleFile.getSelectionText(clearBorder);
        }
        return null;
    };

    //@method 获得被选中的XML代码.
    rootElement.SelectionXml = function (containHeaderFooter) {
        var win = this.GetContentWindow();
        if (win != null && win.WriterCommandModuleFile) {
            return win.WriterCommandModuleFile.getSelectionXml(containHeaderFooter);
        }
        return null;
    };

    //@method 获得被选中的文本内容.
    // nativeHtml 是否清理HTML DOM （true:不清理；false或者为空:清理）
    rootElement.SelectionHtml = function (nativeHtml) {//SelectionHtml方法添加参数来判断是否清理HTML的DOM（true:不清理；false或者为空:清理）
        var win = this.GetContentWindow();
        if (win != null && win.WriterCommandModuleFile) {
            return win.WriterCommandModuleFile.getSelectionHtml(false, nativeHtml);
        }
        return null;
    };

    rootElement.GetHtml = function () {
        var win = this.GetContentWindow();
        if (win != null && win.WriterCommandModuleFile) {
            return win.WriterCommandModuleFile.GetFileContentHtml();
        }
        return null;
    };

    // @method 获得当前元素
    // @param funcFilter 过滤器，可以为一个函数，一个指定的NodeName字符串，或者为空。
    rootElement.CurrnetElement = function (funcFilter) {
        var win = this.GetContentWindow();
        if (win != null && win.DCWriterControllerEditor) {
            return win.DCWriterControllerEditor.CurrentElement(funcFilter);
        }
        return null;
    };

    // 返回字体大小
    //徐逸铭 20190522
    rootElement.getFontSize = function () {
        var win = rootElement.GetContentWindow();
        if (win != null && win.WriterCommandModuleFormat) {
            return win.WriterCommandModuleFormat.getFontSize();
        }
    };

    // 获得当前输入域对象
    rootElement.CurrentInputField = function () {
        return this.CurrnetElement(function (element) {
            if (element.getAttribute && element.getAttribute("dctype") == "XTextInputFieldElement") {
                return true;
            }
            return false;
        });
        ////wyc20210126：修正为更准确的判断
        //var win = this.GetContentWindow();
        //if (win !== null && win.DCSelectionManager) {
        //    return win.DCSelectionManager.GetCurrentInputField();
        //} else {
        //    return null;
        //}
    };

    // 获得当前表格行对象
    rootElement.CurrentTableRow = function () {
        var win = this.GetContentWindow();
        if (win !== null && win.WriterCommandModuleTable) {
            return win.WriterCommandModuleTable.getCurrentRow();
        } else {
            return null;
        }
    };

    // 获得当前单复选框(CurrentCheckboxOrRadio)
    rootElement.CurrentCheckboxOrRadio = function () {
        var XTextCheckBoxElementBaseLabel = rootElement.CurrnetElement(function (el) {
            return (el && el.getAttribute && el.getAttribute("dctype") == "XTextCheckBoxElementBaseLabel");
        });
        var checkboxOrRadio;
        if (XTextCheckBoxElementBaseLabel) {
            // 20220824 xym 修改获取单复选框的代码【ctl.CurrentCheckboxOrRadio】
            var span = XTextCheckBoxElementBaseLabel.parentNode || XTextCheckBoxElementBaseLabel.parentElement;
            if(span){
                checkboxOrRadio = span.querySelector("input");
            }
        }
        return checkboxOrRadio;
    };

    // 添加获得当前光标处的病程记录接口[CurrentSubDoc()]
    rootElement.CurrentSubDoc = function () {
        return rootElement.CurrnetElement(function (node) {
            return (node.nodeName == "DIV" && node.getAttribute("dctype") == "XTextSubDocumentElement");
        });
    };

    /**
     * 删除病程记录
     * @param subdoc 病程记录的ID或者元素本身
     * @return 返回是否删除成功
     */
    rootElement.deleteSubDoc = function(subdoc){
        var doc = rootElement.GetContentDocument();
        if (!subdoc || !doc || !doc.getElementById) {
            return false;
        }
        var subDocNode;
        if (typeof (subdoc) == "string") {
            // 是字符串，当成ID处理
            subDocNode = doc.getElementById(subdoc);
        } else if (subdoc.nodeType == 1) {
            // 是元素
            subDocNode = subdoc;
        } else {
            // 不确定是什么数据，不处理
            return false;
        }
        if (subDocNode.nodeName == "DIV" && subDocNode.getAttribute("dctype") == "XTextSubDocumentElement") {
            // 拿到父节点:
            var parent = subDocNode.parentElement;
            // 删除:
            parent.removeChild(subDocNode);
            return true;
        }
        return false;
    }

    /**
     * 设置病程记录是否分页
     * @param subdoc 病程记录的ID或者元素本身
     * @param isCross Boolean 是否分页
     * @return 返回是否设置成功
     */
    rootElement.subDocCrossPage = function (subdoc, isCross) {
        var win = rootElement.GetContentWindow();
        if (win && win.DCDomTools) {
            isCross = win.DCDomTools.toBoolean(isCross, null);
        }
        if (isCross == null || isCross == undefined) {
            return false
        }
        var doc = rootElement.GetContentDocument();
        if (!subdoc || !doc || !doc.getElementById) {
            return false;
        }
        var subDocNode;
        if (typeof (subdoc) == "string") {
            // 是字符串，当成ID处理
            subDocNode = doc.getElementById(subdoc);
        } else if (subdoc.nodeType == 1) {
            // 是元素
            subDocNode = subdoc;
        } else {
            // 不确定是什么数据，不处理
            return false;
        }
        if (subDocNode.nodeName == "DIV" && subDocNode.getAttribute("dctype") == "XTextSubDocumentElement") {
            subDocNode.setAttribute("dc_newpage", isCross);
            return true;
        }
        return false;
    };

    rootElement.SetTableRowCrossPage = function (tableRow, isCross) {
        if ((isCross === true || isCross === false) && tableRow !== null && tableRow.nodeName.toLowerCase() === "tr") {
            tableRow.setAttribute("dc_newpage", isCross);
            return true;
        } else {
            return false;
        }
    };

    //设置表格的行高
    rootElement.SetTableHeight = function (tableID, newHeight) {
        var doc = rootElement.GetContentDocument();
        var table = doc.getElementById(tableID);
        newHeight = Math.max(newHeight, 15);

        if (table.childNodes[1] && table.childNodes[1].localName == "tbody") {
            var list = table.childNodes[1].childNodes;
            for (var i = 0; i < list.length; i++) {
                list[i].height = parseInt(newHeight);
                list[i].style.height = newHeight + "px";
                list[i].setAttribute("dc_specifyheight", parseInt(newHeight) * 3.125);
                list[i].removeAttribute("height");
            }
        }
        
        //var funcCheckHeight = function () {
        //    if (Math.abs(newHeight - rowElement.offsetHeight) > 3) {
        //        // 高度设置失败，说明高度设置不合理，则删除相关属性。
        //        rowElement.removeAttribute("dc_specifyheight");
        //        rowElement.style.height = "";
        //    }
        //    else {
        //        DCDomTools.BubbleRaiseChanged();
        //    }
        //};
        //window.setTimeout(funcCheckHeight, 10);
    }
    // 获得当前表格单元格对象
    rootElement.CurrentTableCell = function () {
        var win = this.GetContentWindow();
        if (win !== null && win.WriterCommandModuleTable) {
            return win.WriterCommandModuleTable.getCurrentCell();
        } else {
            return null;
        }
    };

    // 获取通用的文档元素的属性 zhangbin20220124
    rootElement.GetElementProperties = function(element,group){
        if(!element){
            element = rootElement.CurrentInputField()
            if(element == null){
                element = rootElement.CurrentCheckboxOrRadio()
            }
            if(element == null){
                element = ctl.CurrnetElement(function (node) {
                    if(node != null && node.getAttribute != null){
                        return (node.getAttribute('dctype') == 'XTextButtonElement')
                    }
                })
            }
            if(element == null){
                return
            }
        }
        var ele = null;
        if (typeof (element) == "string") {
            var doc = rootElement.GetContentDocument();
            ele = doc.getElementById(element);
        } else if (element.nodeName) {
            ele = element;
        }
        var win = this.GetContentWindow();
        if (win == null || ele == null || ele.getAttribute == null || ele.hasAttribute("dctype") === false) {
            return;
        }
        var dctype = ele.getAttribute("dctype");
        //当group为true时转为获取整个输入域组的元素
        if((dctype == 'XTextRadioBoxElement' || dctype == 'XTextCheckBoxElement') && group == true){
            ele = win.DCInputFieldManager.GetCheckBoxOrRadioGroup(ele)
        }
        var result = null;
        switch(dctype){
            case "XTextInputFieldElement": 
                result = win.DCInputFieldManager.GetInputAttribute(ele);
                break;
            case "XTextLabelElement":
                result = win.DCInputFieldManager.GetLabelElementProperties(ele);
                break;
            case "XTextRadioBoxElement":
                result = win.DCInputFieldManager.GetCheckboxOrRadioAttribute(ele,rootElement);
                break;
            case "XTextCheckBoxElement":
                result = win.DCInputFieldManager.GetCheckboxOrRadioAttribute(ele,rootElement);
                break;
            case "XTextButtonElement":
                result = win.DCInputFieldManager.GetButtonAttribute(ele, rootElement)
            case "XTextHorizontalLineElement":
                result = win.WriterCommandModuleFile.GetHorizontalLineProperties(ele, rootElement)
            default:
                break;
        }
        return result;
    }

    // 设置通用的文档元素的属性 wyc20210823
    rootElement.SetElementProperties = function (element, options) {
        //当元素和options都为数组时
        if($.isArray(element) && $.isArray(options)){
            for(var i=0;i<element.length;i++){
                var currentEle = element[i]
                if(currentEle.nodeName && currentEle.hasAttribute('dctype') == true){
                    currentOpt = options[i] ? options[i] : {}
                    rootElement.SetElementProperties(currentEle,currentOpt)
                }
            }
            return
        }
        if(!element){
            return
        }    
        var ele = null;
        if (typeof (element) == "string") {
            var doc = rootElement.GetContentDocument();
            ele = doc.getElementById(element);
        } else if (element.nodeName) {
            ele = element;
        }
        var win = this.GetContentWindow();
        if (win == null || ele == null || ele.getAttribute == null || ele.hasAttribute("dctype") === false) {
            return;
        }
        var dctype = ele.getAttribute("dctype");
        var result = null
        switch (dctype) {
            case "XTextPageInfoElement": //wyc20210823:初版只支持页码元素
                win.DCPageInfoManager.setPageInfoProperties(ele, options);
                break;
            case "XTextLabelElement": //wyc20220824:补充设置标签元素
                result = win.DCInputFieldManager.SetLabelElementProperties(ele, options);
                break;
            case "XTextInputFieldElement": 
                result = win.DCInputFieldManager.SetInputAttribute(ele, options);
                break;
            case "XTextRadioBoxElement":
                win.DCInputFieldManager.SetCheckboxOrRadioAttribute(ele, options,rootElement);
                break;
            case "XTextCheckBoxElement":
                win.DCInputFieldManager.SetCheckboxOrRadioAttribute(ele, options,rootElement);
                break;
            case "XTextButtonElement":
                result = win.DCInputFieldManager.SetButtonAttribute(ele,options)
                break;
            case "XTextHorizontalLineElement":
                result = win.WriterCommandModuleFile.SetHorizontalLineProperties(ele, options)
                break;
            default:
                break;
        }
        return result
    };

    // zhangbin 20221024 通用方法设置元素属性对话框
    rootElement.PopUpWindow = function(ele,type){
        var win = rootElement.GetContentWindow()
        if(win == null){
            return
        }
        //允许使用id或元素本身
        if(typeof ele == 'string'){
            ele = rootElement.GetContentDocument().getElementById(ele)
        }
        //判断需要弹框的元素
        if(ele == null && type != null){
            //判断需要获取的元素
            switch(type){
                case 'HorizontalLine':  //分割线
                    ele = rootElement.CurrnetElement(function(node){
                        return (node.getAttribute != null && node.getAttribute('dctype') == 'XTextHorizontalLineElement' && node.nodeName == 'HR')
                    })
                    break
            }
        }
        //如果元素存在
        if(ele != null){
            //获取元素的dctype
            var dctype = ele.getAttribute('dctype')
            switch(dctype){
                case 'XTextHorizontalLineElement':
                    win.WriterCommandModuleFile.HorizontalLinePopUp(ele)
                    break
            }
        }
        return ele
    }

    // 设置通用的文档元素的边框 wyc20210714
    rootElement.SetElementBorder = function (element, options) {
        var ele = null;
        if (typeof (element) == "string") {
            var doc = rootElement.GetContentDocument();
            ele = doc.getElementById(element);
        } else if (element.nodeName) {
            ele = element;
        }
        var win = this.GetContentWindow();
        if (win != null && win.DCDomTools && ele != null && ele.hasAttribute && ele.hasAttribute("dctype") == true) {
            return win.DCDomTools.SetElementBorder(ele, options);
        } else {
            return false;
        }
    };

    rootElement.SetTableCellBorder = function (cellElement,options) {
        var win = this.GetContentWindow();
        if (win !== null && win.WriterCommandModuleTable) {
            win.WriterCommandModuleTable.setTableCellBorder(cellElement, options);
        }
    };

    //BSDCWRIT-229
    rootElement.setTableCellSettings = function (cellElement, options) {
        var win = this.GetContentWindow();
        if (win !== null && win.WriterCommandModuleTable) {
            win.WriterCommandModuleTable.setTableCellSettings(cellElement, options);
        }
    };
    
    rootElement.SetTableBorder = function (tableElement, options) {
        var win = this.GetContentWindow();
        if (win !== null && win.WriterCommandModuleTable) {
            win.WriterCommandModuleTable.setTableBorder(tableElement, options);
        }
    };

    rootElement.SetFieldDropListFont = function (fontname, fontsize) {
        var win = this.GetContentWindow();
        if (win !== null && win.WriterCommandModuleTable) {
            win.DCInputFieldManager.FontName = fontname;
            win.DCInputFieldManager.FontSize = fontsize;
        }
    };

    // 获得当前表格对象
    rootElement.CurrentTable = function () {
        var win = this.GetContentWindow();
        if (win !== null && win.WriterCommandModuleTable) {
            return win.WriterCommandModuleTable.getCurrentTable();
        } else {
            return null;
        }
    };



    // 获得当前选中区域
    rootElement.DocumentSelection = function () {
        var win = this.GetContentWindow();
        if (win != null && win.DCSelectionManager) {
            return win.DCSelectionManager.getSelection();
        }
    };

    rootElement.getServerInfoJson = function (advanced) {
        //wyc20221228:高级模式下后台返回更多服务器性能参数但会有卡顿
        var advMode = advanced === true ? "true" : "false";
        var url = this.getAttribute("servicepageurl") + "?getserverinfo=" + advMode + "&tick=" + Math.random().toString();
        var txt = DCDomTools.GetContentByUrlNotAsync(url);
        // 20210928 xym 修复getServerInfoJson报错问题
        var result = JSON.parse(txt.replaceAll("\r\n","").replaceAll(",}","}"));
        return result;
    };

    //@method 设置状态栏文本
    rootElement.SetStatusText = function (text) {
        var ctl = rootElement.StatusUIElement;
        if (ctl == null) {
            ctl = rootElement.getAttribute("statusuielement");
        }
        if (typeof (ctl) == "string") {
            ctl = document.getElementById(ctl);
        }
        if (ctl != null) {
            if (typeof (ctl) == "function") {
                ctl.call(this, text);
            }
            else if (ctl.nodeName) {
                ctl.innerText = text;
            }
        }
        //console.log("DCWriter:" + text);
    };

    //@methdod 根据BASE64字符串插入图片
    //@param jsonText Base64字符串
    rootElement.InsertImageByJsonText = function (jsonText) {
        var win = this.GetContentWindow();
        if (win != null && win.DCFileUploadManager != null) {
            // 20210726 xym InsertImageByJsonText方法添加返回值，返回值为img元素组成的数组
            return win.DCFileUploadManager.InsertImageByJsonText(jsonText);
        }
    };

    if (rootElement.getAttribute("contentrendermode") == "ActiveXControl") {
        // 采用ActiveX控件的形式
        document.addEventListener("onkeydown", function (eventObj) {
            if (eventObj.keyCode == 8) {
                // 首先让编辑器试图处理back键
                if (rootElement.HandleBackspace() == true) {
                    // 若编辑器成功的处理了back键，则浏览器无需处理
                    return false;
                }
            }
            return true;
        });
    }

    // 获得显示文档内容的容器窗体对象
    rootElement.GetContentWindow = function (forceUseEditFrame) {
        var iframe = document.getElementById(this.id + "_Frame");
        var previewframe = document.getElementById(this.id + "_PreviewFrame");
        if (forceUseEditFrame === true) {
            if (iframe != null) {
                return iframe.contentWindow;
            } else {
                return null;
            }           
        } else if (forceUseEditFrame === false) {
            if (previewframe != null) {
                return previewframe.contentWindow;
            } else {
                return null;
            }
        }
        //wyc20210224：不强制指定时编辑预览窗体哪个显示就返回哪个
        else if (iframe != null && iframe.style.display != "none") {
            return iframe.contentWindow;
        } else if (previewframe != null && previewframe.style.display != "none") {
            return previewframe.contentWindow;
        } else {
            return null;
        }
   
        //if (iframe == null) {
        //    return null;
        //}
        //else if (iframe.style.display != "none") {
        //    return iframe.contentWindow;
        //}
        ////wyc20200605:预览模式下新增机制强制使用编辑模式下的frame和相关功能
        //if (forceUseEditFrame === true) {
        //    iframe = document.getElementById(this.id + "_Frame");
        //} else if (forceUseEditFrame === false){
        //    iframe = document.getElementById(this.id + "_PreviewFrame");
        //}
        //if (iframe != null) {
        //    return iframe.contentWindow;
        //}
        //return null;
    };

    // 获得显示文档内容的容器窗体对象
    rootElement.GetContentDocument = function () {
        var iframe = document.getElementById(this.id + "_Frame");
        if (iframe == null) {
            return null;
        }
        else {
            return iframe.contentWindow.document;
        }
    };
    //获取预览页面的document
    rootElement.GetPreviewContentDocument = function () {
        var iframe = document.getElementById(this.id + "_PreviewFrame");
        if (iframe == null) {
            return null;
        }
        else {
            return iframe.contentWindow.document;
        }
    };

    // 获得显示文档内容的容器窗体对象
    rootElement.GetContentContainer = function () {
        var iframe = document.getElementById(this.id + "_Frame");
        if (iframe == null) {
            return null;
        }
        else {
            return iframe.contentWindow.document.getElementById("divDocumentBody_0");
        }
    };

    //清除正文内容
    rootElement.ClearDocumentBody = function () {
        var iframe = document.getElementById(this.id + "_Frame");
        if (iframe == null) {
            return false;
        }
        else {
            var result = iframe.contentWindow.document.getElementById("divDocumentBody_0").innerHTML = "<p><br></p>";
            if (result == "<p><br></p>") {
                return true;
            }
        }
        return false;
    };

    //拖放对象,触发对象
    this.id = rootElement.id;
    rootElement.frameElement = document.getElementById(rootElement.id + "_Frame");

    //DCDomTools.SetFrameInnerHTML(rootElement.frameElement, htmlSourceElement.value);
    //htmlSourceElement.parentNode.removeChild(htmlSourceElement);

    // 检查HTML内容是否正确
    rootElement.checkResponseContent = function (responseText) {
        if (responseText != null && typeof (responseText) == "string") {
            if (responseText.indexOf("dcwritersessiontimeoutflag") >= 0) {
                // 出现了会话超时标记
                var doc = this.GetContentDocument();
                if (doc != null) {
                    var lbl = doc.getElementById("divAlertSessionTimeout");
                    if (lbl != null) {
                        lbl.style.display = "";
                    }
                }
                //alert(rootElement.GetDCWriterString("JS_SessionTimeout"));
                var eventObject = new Object();
                eventObject.Message = rootElement.GetDCWriterString("JS_SessionTimeout");
                eventObject.State = rootElement.ErrorInfo.Error;
                rootElement.MessageHandler(eventObject);
                rootElement.HiddenAppProcessing();
                return false;
            }
        }
        return true;
    };

    // 初始化控件属性值
    var funcInitPropertyInstance = function () {
        // 获得承载内容的window对象 
        //rootElement.contentWindow = rootElement.frameElement.contentWindow;
        //if (rootElement.contentWindow != null) {
        //    // 获得内容文档对象
        //    rootElement.contentDocument = rootElement.contentWindow.document;
        //}
        //if (rootElement.contentDocument != null) {
        //    // 获得文档内容正文对象
        //    rootElement.contentBody = rootElement.contentDocument.body;
        //    rootElement.contentDocument.WriterControl = rootElement;
        //}
        // 服务器页面地址
        rootElement.servicePageURL = rootElement.getAttribute("ServicePageURL");
        //rootElement.frameElement.RootWriterControl = rootElement;
        // 设置文档配置
        var doc = rootElement.GetContentDocument();
        if (rootElement.DocumentOptions != null && doc != null) {
            doc.Options = rootElement.DocumentOptions;
        }
    }

    funcInitPropertyInstance();

    var contentRenderMode = rootElement.getAttribute("contentrendermode");
    if (contentRenderMode == "") {

    }
    // 设置控件处于鼠标拖拽滚动模式
    // @setValue 布尔值，是否设置拖拽滚动模式
    rootElement.setMouseDragScrollMode = function (setValue, specifyIFrame) {
        //rootElement.MouseDragScrollMode = setValue;

        var funcSetCursor = function (doc) {
            var tab = doc.getElementById("dctable_AllContent");
            if (tab != null) {
                if (setValue) {
                    tab.style.cursor = "";
                }
                else {
                    tab.style.cursor = "auto";
                }
            }

        }
        //if (this.contentBody == null) {
        //    // 文档还未加载完成
        //    this.dcondocumentload_SetDragScrollMode = function () {
        //        DCDomTools.setMouseDragScrollMode(
        //            this.body,
        //            setValue);
        //        funcSetCursor();
        //    }
        //    return;
        //}
        //else {
        //    this.dcondocumentload_SetDragScrollMode = null;
        //}

        var iframe = document.getElementById(this.id + "_PreviewFrame");
        if (iframe == null || iframe.offsetHeight == 0) {
            iframe = document.getElementById(this.id + "_Frame");
        }
        if (specifyIFrame != null) {
            iframe = specifyIFrame;
        }
        if (iframe != null
            && iframe.contentWindow != null
            && iframe.contentWindow.document != null
            && iframe.contentWindow.document.body != null) {
            iframe.contentWindow.MouseDragScrollMode = setValue;
            var doc = iframe.contentWindow.document;
            var result = DCDomTools.setMouseDragScrollMode(doc.body, setValue);
            funcSetCursor(doc);
            return result;
        }

        //if (rootElement.contentWindow != null
        //    && rootElement.contentWindow.document.body != null) {
        //    var result = this.contentWindow.DCDomTools.setMouseDragScrollMode(
        //        this.contentWindow.document.body,
        //        setValue);
        //    funcSetCursor();
        //    return result;
        //}
        return false;
    };

    //    rootElement.IsMouseDragScrollMode = function () {
    //        if (this.frameWindow != null
    //            && this.frameWindow.DCDomTools != null
    //            && this.frameWindow.document.body != null) {
    //            return this.frameWindow.DCDomTools.IsMouseDragScrollMode(
    //                this.frameWindow.document.body);
    //        }
    //        return false;
    //    };


    // 触发编辑器客户端事件
    rootElement.raiseDCClientEvent = function (eventObject, eventName) {
        if (eventObject != null && eventObject.cancelBubble == true) {
            // 参数指明不再触发后续事件
            return;
        }
        var func = this[eventName];
        if (typeof (func) == "function") {
            return func.call(rootElement, eventObject);
        }
        var script = this.getAttribute(eventName);
        if (script != null && script.length > 0) {
            var func = window[script];
            if (typeof (func) == "function") {
                func.call(rootElement, eventObject);
            }
            else {
                //eval.apply(rootElement);
                //return eval.call(rootElement, script);
            }
        }
    };

    //@method 移动输入焦点到指定地点
    //@param sWhere 要移动的目标,可选值有beforeBegin,afterBegin,beforeEnd,afterEnd
    //@param element 要移动的元素对象
    //@returns true:完成了操作，移动了插入点位置。false:没有完成操作。
    rootElement.FocusAdjacent = function (sWhere, element) {
        var win = rootElement.GetContentWindow();
        if (win != null && win.DCInputFieldManager) {
            var result = win.DCInputFieldManager.FocusAdjacent(sWhere, element);
            // 20211012 xym FocusAdjacent支持屏幕自动滚动到光标所在位置(AutoScrollToCaretWhenGotFocus = true)
            try {
                if (win.DCDomTools && win.DCDomTools.toBoolean) {
                    var AutoScrollToCaretWhenGotFocus = rootElement.DocumentOptions.BehaviorOptions.AutoScrollToCaretWhenGotFocus;
                    AutoScrollToCaretWhenGotFocus = win.DCDomTools.toBoolean(AutoScrollToCaretWhenGotFocus, false);
                }
                if (result && AutoScrollToCaretWhenGotFocus === true) {
                    // element.scrollIntoView && element.scrollIntoView();
                    // var doc = rootElement.GetContentDocument();
                    // var _zoom = $(doc.body).css("zoom");
                    // var _top = element.offsetTop;
                    // $(doc).scrollTop(_top * _zoom);
                    win.DCDomTools.ScrollIntoView(element);
                }
                return result;
            } catch (error) {

            }
        }
        return false;
    };

    // 获得对象在文档视图中的绝对左边边界。
    rootElement.GetAbsBoundsInDocument = function () {
        return DCDomTools.GetAbsBoundsInDocument(rootElement);
    };

    //@method 对文档根元素列表进行排序
    //@param sortFunction 两个节点内容比较函数
    //@root 根节点对象，如果为空，则获得文档中正文容器元素。
    //@returns true:操作修改了文档内容;false:没有修改文档内容。
    rootElement.SortRootContent = function (sortFunction, root) {
        if (root != null) {
            var result = DCDomTools.sortChildNodes(root, sortFunction);
            return result;
        } 
        var doc = this.GetContentDocument();
        if (doc != null) {
            var root = doc.getElementById("divDocumentBody_0");
            if (root != null) {
                var result = DCDomTools.sortChildNodes(root, sortFunction);
                return result;
            }
        }
        return false;
    };

    //@method 设置区域选择打印模式
    //@param setValue 布尔值，是否启用区域选择打印模式。
    rootElement.SetBoundsSelectionViewMode = function (setValue) {
        var iframePreview = document.getElementById(this.id + "_PreviewFrame");
        if (iframePreview != null && iframePreview.offsetHeight == 0) {
            // 有打印预览区域，但没有显示出来，则处理失败。
            return false;
        }
        if (iframePreview == null) {
            iframePreview = document.getElementById(this.id + "_Frame");
        }
        if (iframePreview != null) {
            iframePreview.contentWindow.WriterCommandModuleFile.SetBoundsSelectionViewMode(setValue);
            return true;
        }
        return false;
    };

    //@method 设置续打模式
    //@param setValue 布尔值，是否启用续打模式。
    rootElement.SetJumpPrintMode = function (setValue) {
        var iframePreview = document.getElementById(this.id + "_PreviewFrame");
        if (iframePreview != null && iframePreview.offsetHeight == 0) {
            // 有打印预览区域，但没有显示出来，则处理失败。
            return false;
        }
        if (iframePreview == null) {
            iframePreview = document.getElementById(this.id + "_Frame");
        }
        if (iframePreview != null) {
            iframePreview.contentWindow.WriterCommandModuleFile.JumpPrintModeCommand(setValue);
            return true;
        }
        return false;
    };
    //获取最后一次续打位置
    rootElement.GetLastJumpPrintPosition = function () {
        var iframePreview = document.getElementById(this.id + "_PreviewFrame");
        if (iframePreview == null) {
            iframePreview = document.getElementById(this.id + "_Frame");
        }
        if (iframePreview != null) {
            var p = iframePreview.contentWindow.WriterCommandModuleFile.LastJumpPrintPosition;
            return p;
        }
        return null;
    };
    //设置续打位置
    rootElement.SetLastJumpPrintPosition = function (position,endposition) {
        var iframePreview = document.getElementById(this.id + "_PreviewFrame");
        if (iframePreview == null) {
            iframePreview = document.getElementById(this.id + "_Frame");
        }
        if (iframePreview != null) {
            var p = iframePreview.contentWindow.WriterCommandModuleFile.SetJumpPrintPosition(position, endposition);
            return p;
        }
        return false;
    };

    //@method 获取文档页数
    rootElement.GetDocumentPageNum = function () {
        rootElement.allTimeAfterCreate()
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?dcgetdocumentpagenum=xml" +
            "&tick=" + new Date().valueOf();
        // 创建服务URL地址
        url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”
        var postData = this.GetContentHtml(true, true);
        var result = null;

        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (textStatus == "success") {
                //var responseData = JSON.parse(responseText);
                //以下代替json处理
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var message = temparray2[0];
                var resultstring = temparray2[1];
                if (success == "true") {
                    resultstring = DCDomTools.HTMLDecode(resultstring);
                    result = parseInt(resultstring);
                } else {
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
            rootElement.allTimeBeforeReturn('GetDocumentPageNum')
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: false,
            data: postData,
            error: function (responseText, textStatus, jqXHR) {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(postData)
        $.ajax(settings);
        return result;
    };

    //@method 打印文档
    rootElement.PrintDocument = function (settings) {
        rootElement.allTimeAfterCreate()
        var iframeContent = document.getElementById(this.id + "_Frame");
        if (iframeContent.style.display != "none") {
            // 正在编辑文档内容，则直接打印文档
            var doc = rootElement.GetContentDocument();
            // 20200622 xuyiming 修复分页预览模式下无法打印文档问题
            if (doc.body.getAttribute("contentrendermode") == "PagePreviewHtml") {
                iframeContent.contentWindow.WriterCommandModuleFile.FilePrint(settings);
            } else {
                var postData = this.GetContentHtml(true, true, null, null , true);
                return this.InnerLoadPrintPreview("getprintpreview", postData, true, settings);
            }
        }
        else {
            var iframePreview = document.getElementById(this.id + "_PreviewFrame");
            if (iframePreview != null && iframePreview.offsetHeight == 0) {
                // 有打印预览区域，但没有显示出来，则处理失败。
                return false;
            }
            if (iframePreview == null) {
                iframePreview = document.getElementById(this.id + "_Frame");
            }
            if (iframePreview != null) {
                // 处于打印预览，则打印正在预览的内容。
                iframePreview.contentWindow.WriterCommandModuleFile.FilePrint(settings);
            }
        }
    };

    // 获得本地WEB服务器页面地址
    rootElement.GetLocalServicePageUrl = function () {
        var port = rootElement.getAttribute("localserverport");
        if (port == "99999") {
            // 特殊的标记，直接返回远程服务器页面地址，进入调试状态。
            return rootElement.getAttribute("servicepageurl");
        }
        if (port == null || port.length == 0) {
            port = "2020";// 默认端口号定义在C#代码RunClientConfig.DefaultLocalServerPort中。
        }
        if (port != null && port.length > 0 && isNumber(port)) {
            var dcasmversion = rootElement.getAttribute("dcasmversion");
            var url = "http://localhost:" + port + "/";
            // 执行本地服务器测试
            var testResult = DCDomTools.GetContentByUrlNotAsync(url + "?runclient=test" + dcasmversion);
            if (testResult != null) {
                if (testResult.indexOf("ok") >= 0) {
                    // 测试通过
                    return url;
                }
            }
            // 执行远程服务器测试。
            var startUrl = rootElement.getAttribute("servicepageurl");
            testResult = DCDomTools.GetContentByUrlNotAsync(startUrl + "?runclient=test" + dcasmversion);
            if (testResult != null && testResult.indexOf("ERROR:") >= 0)
            {
                window.alert(testResult);
                return null;
            }
            window.open(
                startUrl + "?runclient=getexe" + dcasmversion + "&port=" + port,
                "_blank");
            alert("将运行本地WEB服务器，发布地址为【" + url + "】,正在下载EXE程序文件，请下载后点击运行EXE程序文件。然后在浏览器中重复本次操作。");
        }
        return null;
    };

    //20200805:后端WC_DCWriterFrontEndPrint测试
    rootElement.LocalPrintDocument = function (options) {
        rootElement.allTimeAfterCreate()
        var showuistring = options != null && options.ShowUI != null && (options.ShowUI === true || options.ShowUI === "true") ? "true" : "false";
        var printername = options != null && options.PrinterName != null ? options.PrinterName.toString() : "";
        var printpagerange = options != null && options.PrintPageRange != null ? options.PrintPageRange.toString() : "";
        var asyncvar = options != null && options.Async != null && (options.Async == true || options.Async == "true") ? true : false;

        var offsetx = options != null && options.OffsetX != null ? options.OffsetX.toString() : "";
        var offsety = options != null && options.OffsetY != null ? options.OffsetY.toString() : "";
        var autofitpagesize = options != null && options.AutoFitPageSize != null && (options.AutoFitPageSize === true || options.AutoFitPageSize === "true") ? "true" : "false";
        var jumpprintposition = options != null && options.JumpPrintPosition != null ? options.JumpPrintPosition.toString() : "";
        var jumpprintendposition = options != null && options.JumpPrintEndPosition != null ? options.JumpPrintEndPosition.toString() : "";

        // 获得服务器页面地址
        var servicePageUrl = rootElement.GetLocalServicePageUrl();// rootElement.getAttribute("servicepageurl");
        if (servicePageUrl == null || servicePageUrl.length == 0) {
            // 操作失败
            return false;
        }
        var url = servicePageUrl + "?dcwriterfrontendprint=1"
            + "&showui=" + showuistring
            + "&printername=" + printername
            + "&printpagerange=" + printpagerange
            + "&offsetx=" + offsetx
            + "&offsety=" + offsety
            + "&autofitpagesize=" + autofitpagesize
            + "&jumpprintposition=" + jumpprintposition
            + "&jumpprintendposition=" + jumpprintendposition
            + "&tick=" + new Date().valueOf();
        // 创建服务URL地址b
        url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”

        var htmlContent = this.GetContentHtml(true, true, true, null, true);// "";
        //var win = this.GetContentWindow(true);
        //if (win != null && win.WriterCommandModuleFile) {
        //    htmlContent = win.WriterCommandModuleFile.GetFileContentHtml();
        //    htmlContent = win.WriterCommandModuleFile.EncodeContentHtmlForPost(htmlContent);
        //}
        var result = false;
        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (rootElement.CheckForWattingMessage(responseText) == true) {
                // 检测到延时等待信息，则退出本操作。
                return;
            }
            if (textStatus == "success") {
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var responsehtml = temparray2[1];
                var message = temparray2[0];
                if (success == "true") {
                    result = true
                } else {
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
            rootElement.allTimeBeforeReturn('LocalPrintDocument')
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: asyncvar,
            data: htmlContent,
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(htmlContent)
        $.ajax(settings);
        return result;
    }

    //wyc20200917:利用前端服务打印批量文档
    rootElement.LocalPrintDocuments = function (options) {
        rootElement.allTimeAfterCreate()
        if (options == undefined || options == null || Array.isArray(options.Files) == false) {
            console.log("需要从options.Files中接收要打印的XML文件数组");
            return;
        }
        var showuistring = options != null && options.ShowUI != null && (options.ShowUI === true || options.ShowUI === "true") ? "true" : "false";
        var printername = options != null && options.PrinterName != null ? options.PrinterName.toString() : "";
        var printpagerange = options != null && options.PrintPageRange != null ? options.PrintPageRange.toString() : "";
        var asyncvar = options != null && options.Async != null && (options.Async == true || options.Async == "true") ? true : false;

        // 获得服务器页面地址
        var servicePageUrl = rootElement.GetLocalServicePageUrl();// rootElement.getAttribute("servicepageurl");
        if (servicePageUrl == null || servicePageUrl.length == 0) {
            // 操作失败
            return false;
        }
        var url = servicePageUrl + "?dcwriterfrontendprintspecifyxml=1"
            + "&showui=" + showuistring
            + "&printername=" + printername
            + "&printpagerange=" + printpagerange
            + "&tick=" + new Date().valueOf();

        // 创建服务URL地址b
        url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”

        var result = false;
        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (rootElement.CheckForWattingMessage(responseText) == true) {
                // 检测到延时等待信息，则退出本操作。
                return;
            }
            if (textStatus == "success") {
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var responsehtml = temparray2[1];
                var message = temparray2[0];
                if (success == "true") {
                    result = true
                } else {
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
            rootElement.allTimeBeforeReturn('LocalPrintDocuments')
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: asyncvar,
            data: JSON.stringify(options.Files),
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(JSON.stringify(options.Files))
        $.ajax(settings);
        return result;
    }


    //20200110 xuyiming 添加传入打印预览的HTML获取打印时的HTML接口GetPrintNowHTML
    //wyc20200624：将代码合并进getprintpreviewhtml内
    rootElement.GetPrintNowHTML = function (xmlstring) {
        if (xmlstring == null) {
            return false;
        }
        var isLandscape = xmlstring.indexOf("landscape=\"True\"") == -1 ? false : true;
        var div = document.createElement("div");
        div.innerHTML = xmlstring;
        var dcRootCenter = div.querySelector("#dcRootCenter");
        // 20220413 xym GetPrintNowHTML添加判断避免传入字符串不是正常html
        if (!dcRootCenter) {
            return xmlstring;
        }
        $(dcRootCenter).find("div[name = dcHiddenElementForPrint]").hide();
        var tables = dcRootCenter.querySelectorAll("table");
        for (var iCount = 0; iCount < tables.length; iCount++) {
            var table = tables[iCount];
            if (table.id == "dctable_AllContent") {
                if (table.getAttribute("haspageborder") != "true") {
                    // 没有页面边框，则设置表格边框为空。
                    table.style.border = "0px none white";
                }
                if (table.style.removeAttribute) {
                    table.style.removeAttribute("border-radius");
                }
                table.style.borderRadius = "";
                var contentRow = table.rows[0];
                if (table.rows) {
                    contentRow = table.rows[0];
                }
                else if (table.firstChild.nodeName == "TD") {
                    contentRow = table.firstChild;
                }
                else if (table.firstChild.nodeName == "TBODY") {
                    contentRow = table.firstChild.firstChild;
                }
                if (contentRow != null) {
                    contentRow.removeAttribute("height");
                }
                for (var iCount2 = 0; iCount2 < contentRow.childNodes.length; iCount2++) {
                    var tdNode = contentRow.childNodes[iCount2];
                    if (tdNode.nodeName == "TD") {
                        if (tdNode.id == "dcGlobalRootElement") {
                            tdNode.width = "";
                        }
                    }
                }
            } //if
        } //for
        var ps = "@page{margin-left:0cm;margin-top:0cm;margin-right:0cm;margin-bottom:0cm;";
        if (isLandscape) {
            ps += "size: landscape;";
        }
        ps += "}";
        var html = "<html><head><style> P{margin:0px}  " + ps + " </style>";
        var styleString = "";
        var styleElement = div.querySelector("#dccontentstyle");
        if (styleElement != null) {
            styleString = styleElement.innerHTML;
        }
        styleElement = div.querySelector("#dccustomcontentstyle");
        if (styleElement != null) {
            styleString = styleString + "\r\n" + styleElement.innerHTML;
        }
        if (styleString.length > 0) {
            html = html + "<style>" + styleString + "</style>";
        }
        html = html + "<style>.hiddenforprint{display:none;}</style>";
        html = html + "</head><body style='padding-left:1px;padding-top:0px;padding-right:0px;padding-bottom:0px;margin:0px"
        html = html + "'>" + dcRootCenter.innerHTML;
        html = html + "</body></html>";
        return html;
    }

    //@method 传入html打印文档
    rootElement.PrintByHtml = function (string, settings) {
        var PrintWindowWidth = "450";
        var PrintWindowHeight = "470";
        var PrintWindowTop = "100";
        var PrintWindowLeft = "400";
        var autoClose = true;
        if (settings != null && settings.AutoClose && (settings.AutoClose == false || settings.AutoClose == "false")) {
            autoClose = false;
        }
        if (settings != null && settings.PrintWindowWidth) {
            PrintWindowWidth = settings.PrintWindowWidth.toString();//parseInt(settings.PrintWindowWidth);
        }
        if (settings != null && settings.PrintWindowHeight) {
            PrintWindowHeight = settings.PrintWindowHeight.toString();//parseInt(settings.PrintWindowWidth);
        }
        if (settings != null && settings.PrintWindowTop) {
            PrintWindowTop = settings.PrintWindowTop.toString();//parseInt(settings.PrintWindowWidth);
        }
        if (settings != null && settings.PrintWindowLeft) {
            PrintWindowLeft = settings.PrintWindowLeft.toString();//parseInt(settings.PrintWindowWidth);
        }
        var oPrntWin = window.open("", "_blank", "width=" + PrintWindowWidth + ",height=" + PrintWindowHeight + ",left=" + PrintWindowLeft + ",top=" + PrintWindowTop + ",menubar=yes,toolbar=no,location=no,scrollbars=yes,resizable=yes");

        var html = string; //rootElement.GetPrintNowHTML(string); //wyc20220111:getprintpreviewhtml集成了getprintnowhtml
        //wyc20221014:打印时临时删除授权信息
        var divv = document.createElement("div");
        divv.innerHTML = html;
        var hiddenitems = divv.getElementsByClassName("hiddenforprint");
        for (var i = 0; i < hiddenitems.length; i++) {
            var item = hiddenitems[i];
            $(item).hide();
        }
        html = divv.innerHTML;
        /////////////////////////////////////
        oPrntWin.document.write(html);
        // xym 修复打印问题【rootElement.PrintByHtml】
        oPrntWin.document.close();
        $(oPrntWin.document).ready(function () {
            if (oPrntWin && oPrntWin.WriterCommandModuleTable && oPrntWin.WriterCommandModuleTable.setTdCustomBorder) {
                oPrntWin.WriterCommandModuleTable.setTdCustomBorder(oPrntWin.document);
            }
            if (document.WriterControl != null
                && document.WriterControl.EventBeforePrint != null
                && typeof (document.WriterControl.EventBeforePrint) == "function") {
                if (document.WriterControl.EventBeforePrint.call(
                    document.WriterControl,
                    oPrntWin.document) === false) {
                    //wyc20200709：若返回false直接关闭窗体不打印了
                    oPrntWin.close();
                    return;
                }
            }
            setTimeout(function () {
                oPrntWin.print();
                if (autoClose === true) {
                    oPrntWin.close(); // 关闭弹出式窗体
                }
            }, 100);
        });

    }

    /**
    * 单元格套打
    * @param {*} doc 打印的document
    * @param {*} hiddenCellData 需要隐藏的单元格数据 []
    * @param {boolean} isStart 是否启动隐藏
    * @returns {*} CellData 打印过的单元格数据 []
    */
    rootElement.TemplatePrintingWithCells = function (doc, hiddenCellData, isStart) {
        var win = rootElement.GetContentWindow();
        if (win != null && win.WriterCommandModuleFile != null) {
            var result = win.WriterCommandModuleFile.TemplatePrintingWithCells(doc, hiddenCellData, isStart);
            return result;
        }
    }

    /**
    * 打印预览时双页展示
    * @param {*} isTwoPage 是否双页展示
    * @returns {*} 
    */
    rootElement.changePreviewTwoPageDisplay = function (isTwoPage) {
        // 不是打印预览模式
        if (rootElement.IsPrintPreview() == false) {
            return;
        }
        isTwoPage = DCDomTools.toBoolean(isTwoPage, false);
        var printPreviewDoc = rootElement.GetPreviewContentDocument();
        var pageindex = $(printPreviewDoc).find("#dcRootCenter > .dcpageforprint > #dctable_AllContent");
        if (pageindex.length <= 1) {
            // 少于一页时，不需要进行下面的操作
            return;
        }
        var styleNode = printPreviewDoc.getElementById("twoPageCss");
        if (!styleNode) {
            styleNode = document.createElement("style");
            styleNode.id = "twoPageCss";
            printPreviewDoc.head.appendChild(styleNode);
        }
        var styleStr = "";
        if (isTwoPage) {
            var paddingNum = 6;
            styleStr += "#dcRootCenter > [name='dcHiddenElementForPrint']{display:none;}";
            styleStr += "#dcRootCenter > .dcpageforprint{float: left;padding: " + paddingNum + "px;}";
            styleStr += "#dcRootCenter::after{display:block;content:'';clear:both;}";
            var widthStr = (pageindex.outerWidth() + paddingNum * 2 + 4) * 2;
            styleStr += "#dcRootCenter{margin: 0 auto;width:" + widthStr + "px;}";
        }
        styleNode.innerHTML = styleStr;
    }

    // 获得打印工具条对象
    rootElement.GetToolbarForPrintPreview = function () {
        var tbr = this.ToolbarForPrintPreview;
        if (tbr == null) {
            tbr = this.getAttribute("toolbarforprintpreview");
        }
        if (tbr != null && typeof (tbr) == "string") {
            tbr = document.getElementById(tbr);
        }
        return tbr;
    };

    //@method 关闭打印预览
    rootElement.ClosePrintPreview = function () {
        var iframeContent = document.getElementById(this.id + "_Frame");
        var iframePreview = document.getElementById(this.id + "_PreviewFrame");
        if (iframePreview == null) {
            return false;
        }
        iframeContent.style.display = "";
        iframePreview.style.display = "none";
        // 获得打印用的工具条
        var tbr = this.GetToolbarForPrintPreview();
        if (tbr != null) {
            tbr.style.display = "none";
        }
        iframePreview.contentWindow.document.write("");
        iframePreview.contentWindow.document.close();
        return true;
    };

    //获取 条形码/二位码
    rootElement.CreateBarcodeElement = function (options, flag) {
        var win = rootElement.GetContentWindow();
        if (win != null && win.DCBarcodeManager != null) {
            var result = win.DCBarcodeManager.createBarcodeElement(options, flag);
            return result;
        }
        
    }
    //@method 留痕合并文档，并以打印预览的方式展示。
    //@param args.OldUserID 旧用户名
    //@param args.OldUerName 旧用户姓名
    //@param args.OldSaveTime 旧的文件保存时间
    //@param args.OldPermissionLevel 旧的文件保存时间
    //@param args.OldFileName 旧文件名
    //@param args.NewUserID  新用户编号
    //@param args.NewUserName 新用户名
    //@param args.NewSaveTime 新文件保存时间
    //@param args.NewPermissionLevel 新文件保存时间
    //@param args.NewFileName 新文件名
    //@param args.FileFormat 文件格式
    rootElement.MergeDocumentByFileName = function (args) {
        if (args == null) {
            // 参数为空
            return false;
        }
        rootElement.allTimeAfterCreate()
        args.FileNameMode = true;
        args.ClientOptions = this.GetWebWriterControlOptionString();
        var postData = JSON.stringify(args);
        return this.InnerLoadPrintPreview("dcmergedocument", postData);
    };

    //@method 留痕合并文档，并以打印预览的方式展示。
    //@param args.OldUserID 旧用户名
    //@param args.OldUerName 旧用户姓名
    //@param args.OldSaveTime 旧的文件保存时间
    //@param args.OldPermissionLevel 旧的文件保存时间
    //@param args.OldFileContent 旧文件内容
    //@param args.NewUserID  新用户编号
    //@param args.NewUserName 新用户名
    //@param args.NewSaveTime 新文件保存时间
    //@param args.NewPermissionLevel 新文件保存时间
    //@param args.NewFileContent 新文件内容
    //@param args.FileFormat 文件格式
    rootElement.MergeDocumentByFileContent = function (args) {
        if (args == null) {
            // 参数为空
            return false;
        }
        rootElement.allTimeAfterCreate()
        args.FileNameMode = false;
        args.ClientOptions = this.GetWebWriterControlOptionString();
        var postData = JSON.stringify(args);
        return this.InnerLoadPrintPreview("dcmergedocument", postData, false);//20200807 xym 修复留痕合并文档显示工具条
    };



    //@method 判断当前控件是否处于打印预览模式。
    rootElement.IsPrintPreview = function () {
        var iframePreview = document.getElementById(this.id + "_PreviewFrame");
        if (iframePreview == null) {
            return false;
        }
        if (iframePreview.style.display == "none") {
            return false;
        }
        return true;
    };

    /**
    * BS分页 ctl.autoPaging({isauto: true,intervalTime:5000})
    * @param {object} obj {isauto:'是否实时运行',intervalTime:'间隔时间,单位为毫秒'}
    * @returns {*} 
    */
    rootElement.autoPaging = function (obj) {
        var win = rootElement.GetContentWindow(true);
        if (!win || !win.WriterCommandModuleFile || typeof (win.WriterCommandModuleFile.insertAnchorNodes) != "function") {
            return;
        }
        var data = {
            isauto: true,//是否实时运行
            intervalTime: 5000,//间隔时间,单位为毫秒
        };
        if (obj && $.isPlainObject(obj)) {
            for (var i in obj) {
                if (Object.hasOwnProperty.call(data, i)) {
                    data[i] = obj[i];
                }
            }
        }
        clearInterval(rootElement.autoPagingFunc);
        rootElement.autoPagingFunc = null;
        if (data.isauto == true) {
            var pagingFunc = function () {
                var old_len = rootElement.autoPagingLength || 0;
                win.WriterCommandModuleFile.insertAnchorNodes();
                var new_len = rootElement.autoPagingLength || 0;
                if (new_len != old_len && (new_len * 10) > data.intervalTime) {
                    var _time = new_len * 10;
                    clearInterval(rootElement.autoPagingFunc);
                    rootElement.autoPagingFunc = setInterval(pagingFunc, _time);
                }
            }
            rootElement.autoPagingFunc = setInterval(pagingFunc, data.intervalTime);
        }
        win.WriterCommandModuleFile.insertAnchorNodes();
    }

    //@method 加载打印预览
    //@param container 承载打印预览内容的HTML容器元素
    rootElement.LoadPrintPreview = function (settings) {
        rootElement.allTimeAfterCreate()
        var postData = this.GetContentHtml(true, true , true, false, true);
        return this.InnerLoadPrintPreview("getprintpreview", postData, false, settings);
    };

    //@method 加载打印预览
    //@param container 承载打印预览内容的HTML容器元素
    rootElement.LoadPrintPreviewWithPermissionMark = function (settings) {
        rootElement.allTimeAfterCreate()
        var postData = this.GetContentHtml(true, true, null, null, true);
        if (settings === undefined || settings === null) {
            settings = {
                "ShowPermissionMark": true,
                "ShowPermissionTip": true,
                "ShowLogicDeletedContent": true
            }
        } else {
            settings.ShowPermissionMark = true;
            settings.ShowPermissionTip = true;
            settings.ShowLogicDeletedContent = true;
        }
        return this.InnerLoadPrintPreview("getprintpreview", postData, false, settings);
    };

    //@method 获取当前文档打印时HTML
    rootElement.GetPrintPreviewHTML = function (settings) {
        rootElement.allTimeAfterCreate()
        var resultHTML = "";
        var iframeContent = document.getElementById(this.id + "_Frame");
        if (iframeContent.style.display != "none") {
            // 正在编辑文档内容，则直接打印文档
            var doc = rootElement.GetContentDocument();
            // 20200622 xuyiming 修复分页预览模式下无法打印文档问题
            if (doc.body.getAttribute("contentrendermode") == "PagePreviewHtml") {
                resultHTML = doc.documentElement.outerHTML;
            } else {
                var postData = this.GetContentHtml(true, true, null, null ,true);
                var servicePageUrl = this.getAttribute("servicepageurl");
                // 创建服务URL地址
                var url = servicePageUrl + "?getprintpreview=1&tick=" + new Date().valueOf();
                if (settings != null && settings.PageIndexs != null && settings.PageIndexs.length > 0) {
                    // 传递页码序号。
                    url = url + "&pageindexs=" + settings.PageIndexs;
                }
                //WYC20191220：新增根据参数让后台输出留痕预览
                if (settings != null && (settings.ShowPermissionMark === true
                    || settings.ShowPermissionMark === "true")) {
                    url = url + "&showpermissionmark=true";
                }
                if (settings != null && (settings.ShowLogicDeletedContent === true
                    || settings.ShowLogicDeletedContent === "true")) {
                    url = url + "&showlogicdeletedcontent=true";
                }
                if (settings != null && (settings.ShowPermissionTip === true
                    || settings.ShowPermissionTip === "true")) {
                    url = url + "&showpermissiontip=true";
                }
                //wyc20210903:新增扩展表格行选项
                if (settings != null && (settings.InsertLastTableRowToPageBottom === true
                    || settings.InsertLastTableRowToPageBottom === "true")) {
                    url = url + "&expandtablerow=true";
                }

                //wyc20230112:处理水印参数
                if (settings != null && typeof (settings.WaterMark) === "object") {
                    var watermarkstr = encodeURIComponent(JSON.stringify(settings.WaterMark));
                    postData = watermarkstr + "$dcwatermarksplitter$" + postData;
                }

                var settings2 = {
                    url: url,
                    method: "POST",
                    type: "POST",
                    data: postData,
                    processData: false,
                    async: false
                };
                if (typeof (contextObj) != "undefined") {
                    settings2.context = contextObj;
                }
                var tickNetwork = new Date().valueOf();
                DCDomTools.fixAjaxSettings(settings2, rootElement);
                rootElement.allTimeBeforeAjax(postData)
                $.ajax(settings2).always(function (responseText, textStatus, jqXHR) {

                    if (textStatus == "success") {
                        var temparray = responseText.split("$dcsuccesssplit$");
                        var success = temparray[0];
                        var temparray2 = temparray[1].split("$dcmessageplit$");
                        var message = temparray2[0];
                        var temparray3 = temparray2[1].split("$dcviewstatesplit$");
                        var responseText = temparray3[1];
                        var newdocumentviewstate = temparray3[0];

                        if (success == "true") {
                            if (typeof (responseText) == "object") {
                                responseText = responseText.responseText;
                            }
                            if (responseText == null || responseText.length == 0) {
                                // 遇到过responseText参数类型为#document类型，因此额外再尝试读取数据。YYF2020-3-20
                                if (typeof (jqXHR) == "object") {
                                    responseText = jqXHR.responseText;
                                }
                            }
                            resultHTML = responseText;
                        } else {
                            rootElement.RuntimeError(message);
                        }
                    } else {
                        rootElement.ConnectionError(responseText, textStatus, jqXHR);
                    }
                    rootElement.allTimeAfterAjax(responseText, jqXHR);
                });
            }
        } else {
            var iframePreview = document.getElementById(this.id + "_PreviewFrame");
            if (iframePreview != null && iframePreview.offsetHeight == 0) {
                // 有打印预览区域，但没有显示出来，则处理失败。
                return null;
            }
            if (iframePreview == null) {
                iframePreview = document.getElementById(this.id + "_Frame");
            }
            if (iframePreview != null) {
                // 处于打印预览，则打印正在预览的内容。
                var previewDoc = rootElement.GetPreviewContentDocument();
                if (previewDoc) {
                    resultHTML = previewDoc.documentElement.outerHTML;
                }
            }
        }
        if(resultHTML){
            // 根据打印预览html生成打印html
            resultHTML = rootElement.GetPrintNowHTML(resultHTML);
        }
        return resultHTML;
    }

    //@method 加载打印预览
    //@param container 承载打印预览内容的HTML容器元素
    rootElement.InnerLoadPrintPreview = function (webMethodName, postData, printDirect, settings) {
        //在打印预览时,对初始时间进行初始化
        rootElement.startTimeForLoadDocumentFormFile = new Date();
        var startTick = new Date().valueOf();
        var iframeContent = document.getElementById(this.id + "_Frame");
        var iframePreview = document.getElementById(this.id + "_PreviewFrame");
        if (iframePreview == null) {
            return false;
        }
        if (printDirect == false) {
            if (iframePreview.offsetHeight > 0) {
                // 已经处于打印预览模式，不再执行后续操作。
                //iframePreview.contentWindow.WriterCommandModuleFile.FilePrint();
                return true;
            }
        }
        var servicePageUrl = this.getAttribute("servicepageurl");
        // 创建服务URL地址
        var url = servicePageUrl + "?" + webMethodName + "=1&tick=" + new Date().valueOf();
        if (settings != null && settings.PageIndexs != null && settings.PageIndexs.length > 0) {
            // 传递页码序号。
            url = url + "&pageindexs=" + settings.PageIndexs;
        }

        //WYC20191220：新增根据参数让后台输出留痕预览
        if (settings != null && (settings.ShowPermissionMark === true
            || settings.ShowPermissionMark === "true")) {
            url = url + "&showpermissionmark=true";
        }
        if (settings != null && (settings.ShowLogicDeletedContent === true
            || settings.ShowLogicDeletedContent === "true")) {
            url = url + "&showlogicdeletedcontent=true";
        }
        if (settings != null && (settings.ShowPermissionTip === true
            || settings.ShowPermissionTip === "true")) {
            url = url + "&showpermissiontip=true";
        }

        //wyc20210903:新增扩展表格行选项
        if (settings != null && (settings.InsertLastTableRowToPageBottom === true
            || settings.InsertLastTableRowToPageBottom === "true")) {
            url = url + "&expandtablerow=true";
        }
        //wyc20220915:新增扩展表格行到当前页底但尽量不扩充到下一页的功能
        if (settings != null && (settings.InsertLastTableRowToPageBottom2 === true
            || settings.InsertLastTableRowToPageBottom2 === "true")) {
            url = url + "&expandtablerow2=true";
        }
        //wyc20220525:新增是否展示碎片化的痕迹效果
        if (settings != null && (settings.MorePermissionMarkPieces === true
            || settings.MorePermissionMarkPieces === "true")) {
            url = url + "&moremarkpieces=true";
        }

        var win = iframePreview.contentWindow;
        win.document.write(this.GetDCWriterString("JS_LoadingPrintPreview"));
        win.document.close();
        iframeContent.style.display = "none";
        iframePreview.style.display = "";

        if (printDirect == false) {

            // 获得打印用的工具条
            var tbr = this.GetToolbarForPrintPreview();
            if (tbr != null) {
                // 显示打印预览用的工具条
                if (tbr.parentNode != this) {
                    this.insertBefore(tbr, iframePreview);
                }
                tbr.style.display = "";
                tbr.disabled = true;
                $("*", tbr).each(function () { this.disabled = true; });
                // 20210510 xym 解决打印预览工具条导致打印预览高度问题
                if (iframePreview != null
                    && iframePreview.offsetHeight > 0
                    && tbr.offsetHeight > 0) {
                    var h = rootElement.clientHeight - tbr.offsetHeight - 1;
                    if (iframePreview.lastHeight != h) {
                        iframePreview.lastHeight = h;
                        iframePreview.style.height = h + "px";
                    }
                }
            }
        }

        //wyc20230112:处理水印参数
        if (settings != null && typeof (settings.WaterMark) === "object") {
            var watermarkstr = encodeURIComponent(JSON.stringify(settings.WaterMark));
            postData = watermarkstr + "$dcwatermarksplitter$" + postData;
        }

        var settings2 = {
            url: url,
            method: "POST",
            type: "POST",
            data: postData,
            processData: false,
            async: true
        };
        if (typeof (contextObj) != "undefined") {
            settings2.context = contextObj;
        }
        DCDomTools.fixAjaxSettings(settings2, rootElement);
        rootElement.allTimeBeforeAjax(postData)
        $.ajax(settings2).always(function (responseText, textStatus, jqXHR) {
            if(jqXHR && jqXHR.getResponseHeader){
                rootElement.totalServerTicks = jqXHR.getResponseHeader("dcservertick");
            }
            if (textStatus == "success") {
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var message = temparray2[0];
                var temparray3 = temparray2[1].split("$dcviewstatesplit$");
                var responseText = temparray3[1];
                var newdocumentviewstate = temparray3[0];

                if (success !== "true") {                   
                    rootElement.RuntimeError(message);
                    iframePreview.style.display = "none";
                    iframeContent.style.display = "";
                    return;
                } 
            } else {
                iframePreview.style.display = "none";
                iframeContent.style.display = "";
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
                return;
            }


            if (rootElement.CheckForWattingMessage(responseText) == true) {
                // 检测到延时等待信息，则退出本操作。
                if (tbr != null) {
                    tbr.disabled = false;
                    $("*", tbr).each(function () { this.disabled = false; });
                }
                return;
            }
            if (printDirect == true) {
                iframePreview.style.display = "none";
                iframeContent.style.display = "";
            }
            if (typeof (responseText) == "object") {
                responseText = responseText.responseText;
            }
            if (responseText == null || responseText.length == 0) {
                // 遇到过responseText参数类型为#document类型，因此额外再尝试读取数据。YYF2020-3-20
                if (typeof (jqXHR) == "object") {
                    responseText = jqXHR.responseText;
                }
            }
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (tbr != null) {
                tbr.disabled = false;
                $("*", tbr).each(function () { this.disabled = false; });
            }
            win.document.write(responseText);
            win.for_LoadPrintPreview = true;
            if (textStatus == "success") {
                win.document.onreadystatechange = function (e) {
                    if (win.document.readyState === "complete") {
                        // 20210311 xym 解决打印预览时缩小字体填充超出问题
                        // var zoomisok = true;
                        $(win.document).find("[nowfontsize]").each(function () {
                            var span = $(this);
                            var isWhiteSpace = span.css("white-space") == "nowrap";
                            var table = span.parents("table[dctype='XTextTableElement']");
                            if (span.length == 1 && table.length > 0 && isWhiteSpace) {
                                // 20220902 xym 修复chrome49下字体缩小打印(BSDCWRIT-983)
                                span.css("zoom", 1);
                                var nowFontSize = parseFloat(span.css("font-size")) * 3 / 4;
                                var autofixfontsize = parseFloat(span.attr("nowfontsize"));
                                if (autofixfontsize != nowFontSize) {
                                    // 缩小字体仅保留小数点后两位
                                    var zoomStr = parseFloat(autofixfontsize / nowFontSize).toFixed(2);
                                    if (zoomStr < 1) {
                                        // 20221019 xym 暂时使用zoom缩放【InnerLoadPrintPreview】
                                        span.css({ "zoom": zoomStr });
                                        // if(span.index($(win.document).find("[nowfontsize]")) == 0){
                                        //     if(span.css("zoom") != zoomStr){
                                        //         zoomisok = false;
                                        //     }
                                        // }
                                        // if(zoomisok == false || true){
                                        //     var cssObj = {
                                        //         "display": "inline-block",
                                        //         "transform": "perspective(1px) scale(" + zoomStr + ")",
                                        //         "transform-origin": "top left",
                                        //         "zoom": 1
                                        //     };
                                        //     span.css(cssObj);
                                        // }
                                    }
                                }
                            }
                        });
                        // zoomisok = null;
                        if (win && win.WriterCommandModuleTable && win.WriterCommandModuleTable.setTdCustomBorder) {
                            win.WriterCommandModuleTable.setTdCustomBorder(win.document);
                        }
                        // 触发控件的EventAfterPrintPreview事件。
                        // 绑定该事件的形式为 ctl.EventAfterPrintPreview = function( doc ){};
                        // 在事件函数中， this表示编辑器控件，doc表示打印预览中的文档对象。
                        if (rootElement.EventAfterPrintPreview != null
                            && typeof (rootElement.EventAfterPrintPreview) == "function") {
                            rootElement.EventAfterPrintPreview.call(rootElement, win.document);
                        }
                        if (printDirect == true) {
                            // 立即打印
                            win.WriterCommandModuleFile.FilePrint(settings);
                        }
                    };
                }
            }
            else {
                if (responseText == null || responseText.length < 10) {
                    win.document.write("<span style='color:red;font-weight:bold'>错误:<br/>" + url + "</span>");
                }
            }
            win.document.close();
            rootElement.allTimeBeforeReturn('PrintPreview')
        });
    };

    //打印预览 （根据指定Html） 解决3104多文档合并续打 2020-2-25 hulijun 
    rootElement.PrintPreviewByHtml = function (html) {
        rootElement.CheckPerformanceCounter("PrintPreviewByHtml");
        var iframeContent = document.getElementById(this.id + "_Frame");
        var iframePreview = document.getElementById(this.id + "_PreviewFrame");
        if (iframePreview == null) {
            return false;
        }
        if (iframePreview.offsetHeight > 0) {
            // 已经处于打印预览模式，不再执行后续操作。
            return true;
        }
        var tickRender = new Date().valueOf();

        var win = iframePreview.contentWindow;
        win.document.write(this.GetDCWriterString("JS_LoadingPrintPreview"));
        win.document.close();
        iframeContent.style.display = "none";
        iframePreview.style.display = "";

        // 获得打印用的工具条
        var tbr = this.GetToolbarForPrintPreview();
        if (tbr != null) {
            // 显示打印预览用的工具条
            if (tbr.parentNode != this) {
                this.insertBefore(tbr, iframePreview);
            }
            tbr.style.display = "";
            tbr.disabled = true;
            $("*", tbr).each(function () { this.disabled = true; });
        }
        
        //---------------------------
        if (tbr != null) {
            tbr.disabled = false;
            $("*", tbr).each(function () { this.disabled = false; });
        }
        
        //-----------------------
        win.document.write(html);
        win.for_LoadPrintPreview = true;
        win.document.onreadystatechange = function (e) {
            if(rootElement.PerformanceCounter != null){
                rootElement.PerformanceCounter.BrowserRenderTickSpan = new Date().valueOf() - tickRender;
            }
            if (win.document.readyState === "complete") {
                if (win && win.WriterCommandModuleTable && win.WriterCommandModuleTable.setTdCustomBorder) {
                    win.WriterCommandModuleTable.setTdCustomBorder(win.document);
                }
                // 触发控件的EventAfterPrintPreview事件。
                // 绑定该事件的形式为 ctl.EventAfterPrintPreview = function( doc ){};
                // 在事件函数中， this表示编辑器控件，doc表示打印预览中的文档对象。
                if (rootElement.EventAfterPrintPreview != null
                    && typeof (rootElement.EventAfterPrintPreview) == "function") {
                    rootElement.EventAfterPrintPreview.call(rootElement, win.document);
                }
                rootElement.OnEventAfterWorkCompleted("PrintPreviewByHtml");
            };
        }
        win.document.close();
        //----------------------
    }

    rootElement.GetWebWriterControlOptionString = function () {
        var input = document.getElementById(rootElement.id + "_WebWriterControlOptions");
        if (input == null) {
            return null;
        }
        else {
            return input.value;
        }
    };

    // 从HTML字符串加载文档内容
    rootElement.LoadDocumentFromHtmlText = function (htmlContent, specifyLoadPart) {
        if (rootElement.checkResponseContent(htmlContent) == false) {
            return false;
        }
        var startTick = new Date().valueOf();
        var tick = 0;
        if (typeof (DCDomTools) != "undefined") {
            DCDomTools.GetDateMillisecondsTick(new Date());
        }
        var doc = this.GetContentDocument();
        if (specifyLoadPart !== "Header" &&
            specifyLoadPart !== "Footer" &&
            specifyLoadPart !== "Body" &&
            specifyLoadPart !== "HeaderFooter") {
            doc.write(htmlContent);     //wyc20210712：只要参数不满足指定条件就全部加载
        } else {
            DCDomTools.LoadDocumentFromHtmlTextSpecifyPart(doc, htmlContent, specifyLoadPart);
        }       
        doc.close();

        //$(rootElement.contentDocument).html(htmlContent);

        //DCDomTools.SetFrameInnerHTML(rootElement.frameElement, htmlContent);
        funcInitPropertyInstance();
        if (typeof (DCDomTools) != "undefined") {
            tick = DCDomTools.GetDateMillisecondsTick(new Date()) - tick;
        }
        if (doc.body != null) {
            doc.body.setAttribute("loadtick", tick);
        }

        window.setTimeout(DCWriterEnsureJQuery, 500); //////////////////////////////////////////////
        if (rootElement.PerformanceCounter != null) {
            rootElement.PerformanceCounter.BrowserRenderTickSpan = new Date().valueOf() - startTick;
        }
        return true;
        //}
        //window.setTimeout(func, 1000);
    };
    rootElement.AutosSaveUrl = function (url, html) {
        DCWriterEnsureJQuery();

        // 显示等待界面
        //rootElement.ShowAppProcessing();
        // 创建服务URL地址
        url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”
        rootElement.startTimeForLoadDocumentFormFile = new Date();

        // 采用Ajax技术来加载文档内容
        var postData = "";

        postData = "html=" + html;

        //var input = document.getElementById(rootElement.id + "_WebWriterControlOptions");
        //if (input != null) {
        //    postData = "webwritercontroloptions=" + input.value;
        //}
        var settings = {
            url: url,
            method: "POST",
            type: "POST",
            data: html,
            processData: false
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        if (typeof (contextObj) != "undefined") {
            settings.context = contextObj;
        }
        $.ajax(settings);
        return true;
    };

    rootElement.InnerLoadDocumentContentFromUrl = function (url, callBack, contextObj,fileName) {
        DCWriterEnsureJQuery();

        // 显示等待界面
        //rootElement.ShowAppProcessing();
        // 创建服务URL地址
        url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”
        rootElement.startTimeForLoadDocumentFormFile = new Date();

        // 采用Ajax技术来加载文档内容
        var postData = "";
        var input = document.getElementById(rootElement.id + "_WebWriterControlOptions");
        if (input != null) {
            postData = "webwritercontroloptions=" + input.value;
        }
        if (fileName != null) {
            postData += "&filename=" + encodeURIComponent(fileName);
        }
        var settings = {
            url: url,
            method: "POST",
            type: "POST",
            contentType: "text/plain",
            data: postData,
            processData: false
        };

        DCDomTools.fixAjaxSettings(settings, rootElement);

        if (typeof (contextObj) != "undefined") {
            settings.context = contextObj;
        }
        rootElement.allTimeBeforeAjax(postData)
        $.ajax(settings).always(callBack);
        //var result = DCDomTools.GetContentByUrl(url, false, func);
        //var tick999 = DCDomTools.GetDateMillisecondsTick(new Date()) - startTick;
        //return DCWriterControllerClass.LoadDocumentFormFile(this, fileName, fileFormat);
        return true;
    };
    //判断字符串是否为数字
    function isNumber(val) {
        var regPos = /^\d+(\.\d+)?$/; //非负浮点数
        var regNeg = /^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$/; //负浮点数
        if (regPos.test(val) || regNeg.test(val)) {
            return true;
        } else {
            return false;
        }
    }

    // 设置文档参数
    rootElement.SetDocumentParameterValue = function (name, pValue) {
        if (rootElement.ParameterValues == null) {
            rootElement.ParameterValues = new Object();
        }
        if (typeof (pValue) == "string" || Array.isArray(pValue) == true) {
            var win = rootElement.GetContentWindow()
            if(win && win.WriterCommandModuleTable){
                if(win.WriterCommandModuleTable.tabelDcAttributes.direction == 'vertical'){
                    if(rootElement.ParameterValues[name]){
                        rootElement.ParameterValues[name]  = rootElement.ParameterValues[name].concat(pValue)
                    }else{
                        rootElement.ParameterValues[name] = pValue;
                    }
                }else{
                    rootElement.ParameterValues[name] = pValue;
                }
            }
        } else {
            var pValue2 = {};
            for (var i in pValue) {
                pValue2[i.toLowerCase()] = pValue[i];
            }
            rootElement.ParameterValues[name] = pValue2;
        }
    };

    // 获得文档参数值
    rootElement.GetDocumentParameterValue = function (name , autoCreate ) {
        if (rootElement.ParameterValues == null) {
            if (autoCreate == true) {
                rootElement.ParameterValues = new Object();
            }
            else {
                return null;
            }
        }
        var result = rootElement.ParameterValues[name];
        if (result == null && autoCreate == true) {
            result = new Object();
            rootElement.ParameterValues[name] = result;
        }
        return result;
    };

    // 将数据从数据源写入到文档中。
    rootElement.GetDataSourceBindingDescriptionsJSON = function () {
        var win = rootElement.GetContentWindow();
        if (win != null && win.DCDataSource != null) {
            var result = win.DCDataSource.GetDataSourceBindingDescriptionsJSON();
            return result;
        }
        return 0;
    };

    //清空所有输入域
    rootElement.ClearAllFields = function () {
        var win = rootElement.GetContentWindow();
        if (win != null && win.DCInputFieldManager != null) {
            win.DCInputFieldManager.clearAllFields();
        }
    }

    //清空数据源
    rootElement.ClearAllParameterValue = function () {
        var win = rootElement.GetContentWindow();
        if (win != null && win.DCDataSource != null) {
            win.DCDataSource.ClearDataSource();
            rootElement.ParameterValues = null;
        }
    }

    // 将数据从数据源写入到文档中。
    rootElement.WriteDataFromDataSourceToDocument = function (specifySubDoc) {
        var win = rootElement.GetContentWindow();
        if (win != null && win.DCDataSource != null) {
            var result = win.DCDataSource.WriteDataFromDataSourceToDocument(specifySubDoc);
            return result;
        }
        return 0;
    };

    // 将数据从文档写入到数据源。
    rootElement.WriteDataFromDocumentToDataSource = function (specifySubDoc) {
        var win = rootElement.GetContentWindow();
        if (win != null && win.DCDataSource != null) {
            var result = win.DCDataSource.WriteDataFromDocumentToDataSource(specifySubDoc);
            return result;
        }
        return 0;
    };

    // WriteDataToTr向表格行中写入数据
    // tr 传入表格行节点
    // data 可传入{} or []
    // {}: 根据数据源绑定路径来赋值
    // []: 根据顺序来赋值
    rootElement.WriteDataToTr = function (tr, data) {
        var win = rootElement.GetContentWindow();
        if (win != null && win.WriterCommandModuleTable != null) {
            var result = win.WriterCommandModuleTable.WriteDataToTr(tr, data);
            return result;
        }
    }

    //对前端单个病程单独加载XML字符串 //wyc20210728
    rootElement.LoadSubDocumentFromString = function (options) {
        rootElement.allTimeAfterCreate()
        //强制要求指定某些属性
        if (options == undefined ||
            options.ID == undefined ||
            options.FileContentXML == undefined) {
            console.log("缺少必要的属性");
            return;
        }
        var doc = rootElement.GetContentDocument();
        var subdoc = doc.getElementById(options.ID);
        if (subdoc == null || subdoc.getAttribute == undefined || subdoc.getAttribute("dctype") !== "XTextSubDocumentElement") {
            console.log("没有找到子文档元素");
            return;
        }
        var loadedfilearray = new Array();
        loadedfilearray.push(options.FileContentXML);
        var opts = new Array();
        opts.push(new Object({ "ID": options.ID }));//xym 保证LoadSubDocumentFromString病程ID不变

        var base64 = "false";
        if (options.Usebase64 == true || options.Usebase64 == "true") {
            base64 = "true";
        }
        var showmaskui = options.ShowMaskUI == true || options.ShowMaskUI == "true" ? true : false;
        var obj = {
            "file": loadedfilearray,//xml文档
            "format": "xml",//xml文档格式
            "base64": base64,//是否是base64字符串
            "type": "two",//{one:整个html，two:body内的html}
            "usesubdoc": "true",
            "forprint": "false",
            "subdocopts": opts
        }
        if (showmaskui === true) {
            rootElement.ShowAppProcessing();
        }

        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?dcwritergethtmlbyfile=xml&tick=" + new Date().valueOf();
        var postData = "";
        var input = document.getElementById(rootElement.id + "_WebWriterControlOptions");
        if (input != null) {
            postData = input.value;
        };
        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (rootElement.CheckForWattingMessage(responseText) == true) {
                // 检测到延时等待信息，则退出本操作。
                return;
            }
            if (textStatus == "success") {
                //var responseData = JSON.parse(responseText);
                //以下代替json处理
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                //var message = temparray2[0];
                var resultstring = temparray2[1];        
                if (success == "true") {
                    // 20220415 xym 暂时不处理HTML字符串
                    var resposeHTML = resultstring;//DCDomTools.HTMLDecode(resultstring);
                    var win = rootElement.GetContentWindow();
                    if (resposeHTML != null && resposeHTML.length > 0 && win != null && win.DCInputFieldManager != null) {
                        var div = document.createElement("DIV");
                        $(div).html(resposeHTML);
                        var returnsubdoc = div.querySelector("[dctype='XTextSubDocumentElement']");
                        if (returnsubdoc != null && win != null) {
                            var inputfields = returnsubdoc.querySelectorAll("[dctype='XTextInputFieldElement']");
                            for (var i = 0; i < inputfields.length; i++) {
                                win.DCInputFieldManager.FixInputElementDom(inputfields[i]);
                            }
                            subdoc.parentElement.replaceChild(returnsubdoc, subdoc);
                            //subdoc.innerHTML = returnsubdoc.innerHTML;
                        }
                    }
                }
            }
            if (showmaskui === true) {
                rootElement.HiddenAppProcessing();
            }
            rootElement.allTimeBeforeReturn('LoadSubDocumentFromString')
        };

        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            data: {
                "param": JSON.stringify(obj),
                "webwritercontroloptions": postData
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(JSON.stringify(settings.data))
        $.ajax(settings);
    };

    //获取病程文档字符串
    rootElement.SaveSubDocumentToString = function (options) {
        rootElement.allTimeAfterCreate()
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");

        var usebase64 = options != undefined && options.UseBase64 != undefined && (options.UseBase64 == true || options.UseBase64 == "true") ? "true" : "false";
        var subdocid = options != undefined && options.SubDocID != undefined ? options.SubDocID : "";
        var subdocids = options != undefined && options.SubDocIDs != undefined ? options.SubDocIDs : "";
        var fileformat = options != undefined && options.FileFormat != undefined ? options.FileFormat : "xml";
        var commitusertraces = options != undefined && options.CommitUserTrace != undefined && (options.CommitUserTrace == true || options.CommitUserTrace == "true") ? "true" : "false";
        var outputformatxml = options != undefined && options.OutputFormatXML != undefined && (options.OutputFormatXML == false || options.OutputFormatXML == "false") ? "false" : "true";
        var loguseredittrack = document.WriterControl != null && document.WriterControl.Options != null && document.WriterControl.Options.LogUserEditTrack === true ? "true" : "false";
        var saveheaderfooter = options != undefined && (options.SaveHeaderFooter === true || options.SaveHeaderFooter === "true") ? "true" : "false";
        var saveoriginheaderfooter = options != undefined && (options.SaveOriginHeaderFooter === true || options.SaveOriginHeaderFooter === "true") ? "true" : "false";
        var smartmode = options != undefined && options.SmartMode != undefined && (options.SmartMode === true || options.SmartMode === "true") ? "true" : "false";

        var url = servicePageUrl + "?savesubdocumenttostring=" + encodeURI(fileformat)
            + "&controlinstanceid=" + rootElement.getAttribute("controlinstanceid")
            + "&contentrendermode=" + rootElement.getAttribute("contentrendermode")
            + "&subdocumentid=" + subdocid
            + "&subdocumentids=" + subdocids
            + "&commitusertraces=" + commitusertraces
            + "&outputformatxml=" + outputformatxml
            + "&usebase64=" + usebase64
            + "&loguseredittrack=" + loguseredittrack
            + "&saveheaderfooter=" + saveheaderfooter
            + "&smartmode=" + smartmode
            + "&saveoriginheaderfooter=" + saveoriginheaderfooter
            + "&tick=" + new Date().valueOf();
        // 创建服务URL地址
        url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”

        var htmlContent = "";
        var win = this.GetContentWindow();
        if (win != null && win.WriterCommandModuleFile) {
            if (smartmode === "false") {
                htmlContent = win.WriterCommandModuleFile.GetFileContentHtml();
                htmlContent = win.WriterCommandModuleFile.EncodeContentHtmlForPost(htmlContent);
            //wyc20221025:新机制传入不同的html内容
            } else {
                var obj2 = new Object();
                var subarr = new Array();
                if (subdocid.length > 0) {
                    subarr.push(subdocid);
                } else if (subdocids.length > 0) {
                    subarr = JSON.parse(subdocids);
                }
                if (Array.isArray(subarr) === true && subarr.length > 0) {
                    var doc = this.GetContentDocument();
                    for (var i = 0; i < subarr.length; i++) {
                        var tid = subarr[i];
                        var subdocdom = doc.getElementById(tid);
                        if (subdocdom != null && subdocdom.getAttribute("dctype") === "XTextSubDocumentElement") {
                            var subdochtml = win.DCDomTools.GetOuterHTML(subdocdom);
                            obj2[tid] = subdochtml;
                        }
                    }
                }
                //wyc20221026:需要夹带上前端options
                var input = $(document.WriterControl).find("#" + document.WriterControl.id + "_WebWriterControlOptions")[0];
                if (input != null) {
                    obj2.optstring = input.value;
                }
                htmlContent = encodeURIComponent(JSON.stringify(obj2));
            }
        }
        var result = "";
        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (rootElement.CheckForWattingMessage(responseText) == true) {
                // 检测到延时等待信息，则退出本操作。
                return;
            }
            if (textStatus == "success") {
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var responsehtml = temparray2[1];
                var message = temparray2[0];
                if (success == "true") {
                    //wyc20220711：
                    result = JSON.parse(responsehtml);
                    if (subdocids.indexOf("[") == -1) {
                        //表示取单个病程，只返回字符串本身
                        result = result[subdocid];
                    }
                } else {
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
            rootElement.allTimeBeforeReturn('SaveSubDocumentToString')
        };

        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: false,
            data: htmlContent,
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(htmlContent)
        $.ajax(settings);
        return result;
    };

    //获取病程文档字符串
    //@param fileFormat 字符串格式
    //@param id 病程ID
    rootElement.SaveSubDocumentToBase64String = function (fileFormat, id) {
        rootElement.allTimeAfterCreate()
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?savesubdocumenttostring=" + encodeURI(fileFormat)
            + "&controlinstanceid=" + rootElement.getAttribute("controlinstanceid")
            + "&contentrendermode=" + rootElement.getAttribute("contentrendermode")
            + "&subdocumentid=" + id
            + "&usebase64=true"
            + "&tick=" + new Date().valueOf();
        // 创建服务URL地址
        url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”

        var htmlContent = "";
        var win = this.GetContentWindow();
        if (win != null && win.WriterCommandModuleFile) {
            htmlContent = win.WriterCommandModuleFile.GetFileContentHtml();
            htmlContent = win.WriterCommandModuleFile.EncodeContentHtmlForPost(htmlContent);
        }
        var result = "";
        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (rootElement.CheckForWattingMessage(responseText) == true) {
                // 检测到延时等待信息，则退出本操作。
                return;
            }
            if (textStatus == "success") {
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var responsehtml = temparray2[1];
                var message = temparray2[0];
                if (success == "true") {
                    result = responsehtml;//.replace(" ", " ");;//wyc20200618：这里有可能会生成一个特殊的空格字符
                } else {
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
            rootElement.allTimeBeforeReturn('SaveSubDocumentToBase64String')
        };

        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: false,
            data: htmlContent,
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(htmlContent)
        $.ajax(settings);
        return result;
    };

    //获取文档正文xml字符串
    rootElement.SaveBodyDocumentToString = function(fileFormat){
        return rootElement.GetElementInnerXmlById("divAllContainer");
    }

    //wyc20220402:异步保存
    rootElement.SaveDocumentToStringAsync = function (options) {
        var back = rootElement.Options.AJAXAsync;
        rootElement.Options.AJAXAsync = true;
        rootElement.SaveDocumentToString(options);
        rootElement.Options.AJAXAsync = back;
    }

    //wyc20220914:保存成特定的XML结构，只暴露其中的输入域与选框元素中的重要属性
    rootElement.SaveDocumentToSpecificXMLString = function () {
        rootElement.allTimeAfterCreate()
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?savedocumenttospecificxmlstring=1" + "&tick=" + new Date().valueOf();
        // 创建服务URL地址b
        url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”

        var htmlContent = this.GetContentHtml(true, true);
        
        var result = "";
        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax(responseText, jqXHR)
            if (rootElement.CheckForWattingMessage(responseText) == true) {
                // 检测到延时等待信息，则退出本操作。
                return;
            }
            //var resultobj = JSON.parse(responseText);
            if (textStatus == "success") {
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var message = temparray2[0];
                var responsehtml = temparray2[1];

                if (success == "true") {
                    result = responsehtml;//.replace(" ", " ");//wyc20200618：这里有可能会生成一个特殊的空格字符
                    if (message != null && message.length > 0) {
                        console.log(message);
                    }
                } else {
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
            rootElement.allTimeBeforeReturn("SaveDocumentToSpecificXMLString")
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: false,
            data: htmlContent,
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(htmlContent)
        $.ajax(settings);
        return result;
    };

    //获取文档字符串
    //@param fileFormat 字符串格式
    rootElement.SaveDocumentToString = function (options) {
        rootElement.allTimeAfterCreate()
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var fileformat = options != undefined && options.FileFormat != undefined ? options.FileFormat : "xml";
        var commitusertraces = options != undefined && (options.CommitUserTrace === true || options.CommitUserTrace === "true") ? "true" : "false";
        var outputformatxml = options != undefined && (options.OutputFormatXML === false || options.OutputFormatXML === "false") ? "false" : "true";
        var encodingname = options != undefined && options.EncodingName != undefined ? options.EncodingName.toString() : "";
        var savetobase64 = options != undefined && (options.SaveBase64String === true || options.SaveBase64String === "true") ? "true" : "false";
        var specifysavepart = options != undefined && options.SpecifySavePart != undefined ? options.SpecifySavePart.toString() : "ALL";
        var cleardatabindingcontent = options != undefined && (options.ClearDataBindingContent === true || options.ClearDataBindingContent === "true") ? "true" : "false";
        var url = servicePageUrl + "?savedocumenttostring=" + encodeURI(fileformat)
            + "&controlinstanceid=" + rootElement.getAttribute("controlinstanceid")
            + "&commitusertraces=" + commitusertraces
            + "&outputformatxml=" + outputformatxml
            + "&encodingname=" + encodingname
            + "&savetobase64=" + savetobase64
            + "&specifysavepart=" + specifysavepart
            + "&cleardatabindingcontent=" + cleardatabindingcontent
            + "&contentrendermode=" + rootElement.getAttribute("contentrendermode") + "&tick=" + new Date().valueOf();
        // 创建服务URL地址b
        url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”

        //wyc20211028:新增扩展表格行选项
        if (options != null && (options.InsertLastTableRowToPageBottom === true
            || options.InsertLastTableRowToPageBottom === "true")) {
            url = url + "&expandtablerow=true";
        }

        //wyc20221110:夹带attributes
        if (options != null && typeof (options.AttachedCustomAttributes) === "object") {
            var attrstring = "";
            for (var i in options.AttachedCustomAttributes) {
                var name = i.toString();
                var value = options.AttachedCustomAttributes[i].toString();
                attrstring = attrstring + name + ":" + value + ";";
            }
            if (attrstring.length > 0) {
                attrstring = attrstring.slice(0, attrstring.length - 1);
                url = url + "&addattrs=" + encodeURIComponent(attrstring);
            }
        }

        var htmlContent = this.GetContentHtml(true, true);
        //var win = this.GetContentWindow();
        //if (win != null && win.WriterCommandModuleFile) {
        //    htmlContent = win.WriterCommandModuleFile.GetFileContentHtml(includeweboptions);
        //    htmlContent = win.WriterCommandModuleFile.EncodeContentHtmlForPost(htmlContent);
        //}
        var result = "";
        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (rootElement.CheckForWattingMessage(responseText) == true) {
                // 检测到延时等待信息，则退出本操作。
                return;
            }
            //var resultobj = JSON.parse(responseText);
            if (textStatus == "success") {
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var message = temparray2[0];
                var temparray3 = temparray2[1].split("$dcviewstatesplit$");
                var responsehtml = temparray3[1];               
                var newdocumentviewstate = temparray3[0];

                if (success == "true") {
                    result = responsehtml;//.replace(" ", " ");//wyc20200618：这里有可能会生成一个特殊的空格字符
                    if (message != null && message.length > 0) {
                        console.log(message);
                    }
                    //wyc20220210：留痕模式下保存时追加更新前端文档状态数据
                    if (newdocumentviewstate.length > 0 && rootElement.Options != null &&
                        rootElement.Options.LogUserEditTrack === true) {
                        var doc = rootElement.GetContentDocument();
                        var body = doc.getElementById("divDocumentBody_0");
                        body.setAttribute("dcdocumentviewstate", newdocumentviewstate);
                    }
                    //wyc20220402：为异步提供事件
                    if (typeof (rootElement.EventAfterSaveDocumentToString) == "function") {
                        rootElement.EventAfterSaveDocumentToString.call(rootElement, result);
                    }
                } else {
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
            rootElement.allTimeBeforeReturn("SaveDocumentToString")
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: false,
            data: htmlContent,
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(htmlContent)
        $.ajax( settings );
        return result;
    };




    //获取文档字符串
    //@param fileFormat 字符串格式
    rootElement.GetXMLsByHTMLs = function (options) {
        rootElement.allTimeAfterCreate()
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?dcgetxmlsbyhtmls=1" + "&tick=" + new Date().valueOf();

        var datas = JSON.stringify(options);
        var result = null;
        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax(responseText, jqXHR)
            if (rootElement.CheckForWattingMessage(responseText) == true) {
                // 检测到延时等待信息，则退出本操作。
                return;
            }
            //var resultobj = JSON.parse(responseText);
            if (textStatus == "success") {
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var message = temparray2[0];
                var temparray3 = temparray2[1].split("$dcviewstatesplit$");
                var responsehtml = temparray3[1];
                var newdocumentviewstate = temparray3[0];

                if (success == "true") {
                    var str = decodeURIComponent(responsehtml);
                    result = JSON.parse(str);
                    
                } else {
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
            rootElement.allTimeBeforeReturn("GetXMLsByHTMLs");
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: false,
            data: encodeURIComponent( datas ),
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(datas)
        $.ajax(settings);
        return result;
    };






    //将文档末尾表格新增行至当页的页尾 //wyc20220315
    rootElement.InsertTableRowToPageBottom = function (expand,clonetype) {
        rootElement.allTimeAfterCreate()
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var exppandpage = expand === true ? "true" : "false";
        var clonetypestring = clonetype === "Complete" || clonetype === "ContentWithClearField" ? clonetype : "Default";
        var url = servicePageUrl + "?dcwriterinserttablerowtopagebottom=1"
            + "&exppandpage=" + exppandpage
            + "&controlinstanceid=" + rootElement.getAttribute("controlinstanceid")
            + "&contentrendermode=" + rootElement.getAttribute("contentrendermode") + "&tick=" + new Date().valueOf();
        // 创建服务URL地址b
        url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”

        var htmlContent = this.GetContentHtml(true, true);
        var result = false;
        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (textStatus == "success") {
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var responsehtml = temparray2[1];
                var message = temparray2[0];

                //var responseData = JSON.parse(responseText);
                if (success == "true") {
                    rootElement.ServerMessage = message;//responseData.message;
                    ////wyc20220318:由后端传回整个文档改为只传回行数由前端来添加行
                    //var i = parseInt(responsehtml);
                    //var win = rootElement.GetContentWindow();
                    //if (i > 0 && win && win.WriterCommandModuleTable) {
                    //    var doc = rootElement.GetContentDocument();
                    //    var tables = doc.querySelectorAll("[dctype='XTextTableElement']");
                    //    var table = tables[tables.length - 1];
                    //    var trs = table.querySelectorAll("tr");
                    //    var tr = trs[trs.length - 1];
                    //    var parent = tr.parentElement;
                    //    for (var j = 0; j < i - 1; j++) {
                    //        var newrow = win.WriterCommandModuleTable.editorCloneTableRow(tr, clonetypestring);
                    //        parent.appendChild(newrow);
                    //    }
                    //}
                    var resposeHTML = responsehtml;//DCDomTools.HTMLDecode(responseData.result);
                    if (rootElement.CallbackForLoadDocumentHtml(resposeHTML, textStatus, jqXHR) == true) {
                        rootElement.FileFormat = "xml";
                        result = true;
                    }
                } else {
                    rootElement.HiddenAppProcessing();
                    rootElement.RuntimeError(message);
                    result = false;
                }
            } else {
                rootElement.HiddenAppProcessing();
                rootElement.RuntimeError(responseText, textStatus, jqXHR);
                result = false;
            }
            rootElement.allTimeBeforeReturn('InsertTableRowToPageBottom')
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: false,
            data: htmlContent,
            error: function (responseText, textStatus, jqXHR) {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(htmlContent)
        $.ajax(settings);
        return result;
    };

    //提交文档中的痕迹
    rootElement.CommitDocumentUserTrace = function () {
        rootElement.allTimeAfterCreate()
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?dccommitdocumentusertrace=1"
            + "&controlinstanceid=" + rootElement.getAttribute("controlinstanceid")
            + "&contentrendermode=" + rootElement.getAttribute("contentrendermode") + "&tick=" + new Date().valueOf();
        // 创建服务URL地址b
        url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”

        var htmlContent = this.GetContentHtml(true, true);
        var result = false;
        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (textStatus == "success") {
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var responsehtml = temparray2[1];
                var message = temparray2[0];

                //var responseData = JSON.parse(responseText);
                if (success == "true") {
                    rootElement.ServerMessage = message;//responseData.message;
                    var resposeHTML = responsehtml;//DCDomTools.HTMLDecode(responseData.result);
                    if (rootElement.CallbackForLoadDocumentHtml(resposeHTML, textStatus, jqXHR) == true) {
                        rootElement.FileFormat = "xml";
                        result = true;
                    }
                } else {
                    rootElement.HiddenAppProcessing();
                    rootElement.RuntimeError(message);
                    result = false;
                }
            } else {
                rootElement.HiddenAppProcessing();
                rootElement.RuntimeError(responseText, textStatus, jqXHR);
                result = false;
            }
            rootElement.allTimeBeforeReturn('CommitDocumentUserTrace')
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: false,
            data: htmlContent,
            error: function (responseText, textStatus, jqXHR) {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(htmlContent)
        $.ajax(settings);
        return result;
    };

    //wyc20210826:获取文档指定页面的图片
    rootElement.GetDocumentSpecifyPageImages = function (options) {
        rootElement.allTimeAfterCreate()
        var resultImage = new Array();
        var showMarginLine = options != null && (options.ShowMarginLine === true || options.ShowMarginLine === "true");
        var specifypageindex = options != null && options.SpecifyPageIndexes != null ? options.SpecifyPageIndexes : "1";
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?getdcwriterpageimage=" + specifypageindex
            + "&showmarginline=" + showMarginLine.toString() + "&tick=" + new Date().valueOf();
        // 创建服务URL地址b
        url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”

        var htmlContent = this.GetContentHtml(true, true);
        var result = false;
        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (textStatus == "success") {
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var responsehtml = temparray2[1];
                var message = temparray2[0];

                //var responseData = JSON.parse(responseText);
                if (success == "true") {
                    rootElement.ServerMessage = message;//responseData.message;
                    var imagebasestrings = JSON.parse(responsehtml);
                    if (Array.isArray(imagebasestrings) == true) {
                        for (var i = 0; i < imagebasestrings.length; i++) {
                            var imgstr = imagebasestrings[i];
                            var img = document.createElement("img");
                            img.src = imgstr;
                            resultImage.push(img);
                        }
                    }
                } else {
                    rootElement.HiddenAppProcessing();
                    rootElement.RuntimeError(message);
                    result = false;
                }
            } else {
                rootElement.HiddenAppProcessing();
                rootElement.RuntimeError(responseText, textStatus, jqXHR);
                result = false;
            }
            rootElement.allTimeBeforeReturn('GetDocumentSpecifyPageImages')
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: false,
            data: htmlContent,
            error: function (responseText, textStatus, jqXHR) {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(htmlContent)
        $.ajax(settings);
        return resultImage;
    };

    //wyc20201124:获取文档中的痕迹列表
    rootElement.GetDocumentUserTrackInfos = function (IsUpdate) {
        rootElement.allTimeAfterCreate()
        //debugger;
        var resultjsonstring = null;
        if (IsUpdate == true) {//请求后台获取新的痕迹列表数据
            // 获得服务器页面地址
            var servicePageUrl = rootElement.getAttribute("servicepageurl");
            var url = servicePageUrl + "?dcgetusertrackinfos=1"
                + "&controlinstanceid=" + rootElement.getAttribute("controlinstanceid")
                + "&contentrendermode=" + rootElement.getAttribute("contentrendermode") + "&tick=" + new Date().valueOf();
            // 创建服务URL地址b
            url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”

            var htmlContent = this.GetContentHtml(true, true, true);
            var result = false;
            var funcCallback = function (responseText, textStatus, jqXHR) {
                rootElement.allTimeAfterAjax (responseText, jqXHR)
                if (textStatus == "success") {
                    var temparray = responseText.split("$dcsuccesssplit$");
                    var success = temparray[0];
                    var temparray2 = temparray[1].split("$dcmessageplit$");
                    var responsehtml = temparray2[1];
                    var message = temparray2[0];

                    //var responseData = JSON.parse(responseText);
                    if (success == "true") {
                        rootElement.ServerMessage = message;//responseData.message;
                        var resposeHTML = responsehtml;//DCDomTools.HTMLDecode(responseData.result);
                        var div = document.createElement("div");
                        div.innerHTML = resposeHTML;
                        var tempid = "[id='" + rootElement.id + "_usertrackinfoobjstring" + "']" ;
                        var input = div.querySelector(tempid);
                        if (input != null && input.value) {
                            resultjsonstring = input.value;
                        }
                        if (rootElement.CallbackForLoadDocumentHtml(resposeHTML, textStatus, jqXHR) == true) {
                            rootElement.FileFormat = "xml";
                            //var doc = rootElement.GetContentDocument();
                            //var input = $(doc).find("#" + rootElement.id + "_usertrackinfoobjstring")[0];
                            //if (input != null && input.value) {
                            //    resultjsonstring = input.value;
                            //}
                        }
                    } else {
                        rootElement.HiddenAppProcessing();
                        rootElement.RuntimeError(message);
                        result = false;
                    }
                } else {
                    rootElement.HiddenAppProcessing();
                    rootElement.RuntimeError(responseText, textStatus, jqXHR);
                    result = false;
                }
                rootElement.allTimeBeforeReturn('GetDocumentUserTrackInfos')
            };
            var settings = {
                method: "POST",
                type: "POST",
                url: url,
                async: false,
                data: htmlContent,
                error: function (responseText, textStatus, jqXHR) {
                    rootElement.ConnectionError(responseText, textStatus, jqXHR);
                },
                success: funcCallback
            };
            DCDomTools.fixAjaxSettings(settings, rootElement);
            rootElement.allTimeBeforeAjax(htmlContent)
            $.ajax(settings);
        }
        if (resultjsonstring == null) {
            var isprintpreview = rootElement.IsPrintPreview();
            var after = isprintpreview === true ? "_usertrackinfoobjstringforprint" : "_usertrackinfoobjstring";
            var doc = isprintpreview === true ? rootElement.GetPreviewContentDocument() : rootElement.GetContentDocument();
            var input = $(doc).find("#" + rootElement.id + after)[0];
            if (input != null && input.value) {
                resultjsonstring = input.value;
            }
        }
        return JSON.parse(resultjsonstring);
    };


    //wyc20220421:
    rootElement.GetAPPUpdateRecording = function () {
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?dcwriterresourcename=1"
            + "&newmode=1" + "&tick=" + new Date().valueOf();
        // 创建服务URL地址b
        var result = false;
        var funcCallback = function (responseText, textStatus, jqXHR) {
            if (textStatus == "success") {
                var wind = window.open("", "_blank", "menubar=no,toolbar=no,location=no,scrollbars=no,resizable=yes");
                wind.document.write(responseText);
                return true;
            } else {
                rootElement.HiddenAppProcessing();
                rootElement.RuntimeError(responseText, textStatus, jqXHR);
                result = false;
            }
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: false,
            data: null,
            error: function (responseText, textStatus, jqXHR) {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        $.ajax(settings);
    }

    //wyc20211018:获取给定ID的元素的坐标
    rootElement.GetElementCoordinateByID = function (ids, ispreviewmode, specifyxml) {
        rootElement.allTimeAfterCreate()
        //debugger;
        var resultjsonstring = null;
        var arrid = new Array();
        if (typeof (ids) == "string") {
            arrid.push(ids);
        } else if (Array.isArray(ids) === true) {
            arrid = ids;
        }
        var paraid = JSON.stringify(arrid);
        var ispreview = ispreviewmode === true || ispreviewmode === "true" ? "true" : "false";
        var usespecifyxml = typeof (specifyxml) === "string" && specifyxml.length > 0 ? "true" : "false";

        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?dcgetelementcoordinate=1"
            + "&paraid=" + paraid
            + "&usespecifyxml=" + usespecifyxml
            + "&ispreview=" + ispreview + "&tick=" + new Date().valueOf();
        // 创建服务URL地址b
        url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”

        var htmlContent = usespecifyxml === "true" ? specifyxml : this.GetContentHtml(true, true);
        var result = false;
        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (textStatus == "success") {
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var responsehtml = temparray2[1];
                var message = temparray2[0];

                //var responseData = JSON.parse(responseText);
                if (success == "true") {
                    rootElement.ServerMessage = message;//responseData.message;
                    resultjsonstring = responsehtml;
                } else {
                    rootElement.HiddenAppProcessing();
                    rootElement.RuntimeError(message);
                    result = false;
                }
            } else {
                rootElement.HiddenAppProcessing();
                rootElement.RuntimeError(responseText, textStatus, jqXHR);
                result = false;
            }
            rootElement.allTimeBeforeReturn('GetElementCoordinateByID')
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: false,
            data: htmlContent,
            error: function (responseText, textStatus, jqXHR) {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(htmlContent)
        $.ajax(settings);
        return JSON.parse(resultjsonstring);
    };


    //获取文档Base64字符串
    //@param fileFormat 字符串格式 //wyc2022078 归并到savedocumenttostring
    rootElement.SaveDocumentToBase64String = function (options) {
        var opt = {
            SaveBase64String: "true"
        };
        if (typeof (options) === "object") {
            opt = options;
            opt.SaveBase64String = "true";
        }
        else if (typeof (options) === "string") {
            opt.FileFormat = options;
        }
        return rootElement.SaveDocumentToString(opt);
    };

    //调用后台的ReleaseSession命令
    rootElement.ReleaseSession = function () {
        var result = "";
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?releasesession=" + encodeURI("1")
            + "&controlinstanceid=" + rootElement.getAttribute("controlinstanceid")
            + "&contentrendermode=" + rootElement.getAttribute("contentrendermode") + "&tick=" + new Date().valueOf();
        // 创建服务URL地址
        url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”

        var htmlContent = "";
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: false,
            data: htmlContent,
            //processData: false,
            error: function (request) {
            },
            success: function (data) {
                result = data;
            }
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        $.ajax( settings );
        return result;
    };

    //WYC20200220：重置文档修改状态
    rootElement.ResetDocumentModified = function () {
        var win = rootElement.GetContentWindow();
        if (win != null && win.DCMultiDocumentManager) {
            win.DCMultiDocumentManager.reset();
            win.DCUndoRedo.Clear();
            win.DCMultiDocumentManager.Start();
            win.DCMultiDocumentManager.setModify = false
        } else {
            console.log("ResetDocumentModified：失败")
        }
        
    };

    /*rootElement.getServerInfo = function () {
        var doc = this.GetContentDocument();
        if (doc != null && doc.body != null) {
            var result = new Object();
            // 总的输出字符数
            result.characters = parseInt(doc.body.getAttribute("serverchars"));
            // 总字符数
            ctl.serverPerformances.characters = parseInt(document.body.getAttribute("serverchars"));
            // 服务器端耗时毫秒数
            ctl.serverPerformances.serverTicks = parseInt(document.body.getAttribute("serverticks"));
            ctl.serverPerformances.totalServerTicks = ctl.totalServerTicks;
            ctl.serverPerformances.maxOnlineNumber = parseInt(document.body.getAttribute("maxonlinenumber"));
            ctl.serverPerformances.currentOnlineNumber = parseInt(document.body.getAttribute("currentonlinenumber"));

        }
        else {
            return new Object();
        }
    };*/

    //获取所有病程
    rootElement.GetCourseRecords = function (id) {
        var arr;
        var win = rootElement.GetContentWindow();
        if (win != null && win.DCWriterControllerEditor) {
            arr = win.DCWriterControllerEditor.GetCourseRecords(id);
        }
        return arr;
    }

    //根据XML获取HTML
    //@param strXML XML字符串
    //@param type {one:整个html，two:body内的html}
    rootElement.getHtmlByXMLString = function (strXML, type) {
        rootElement.allTimeAfterCreate()
        var obj = {
            "file": strXML,//xml文档 
            "format": "xml",//xml文档格式
            "base64": "false",//是否是base64字符串
            "type": type//end     
        };
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?dcwritergethtmlbyfile=xml&tick=" + new Date().valueOf();

        var postData = "";
        var input = document.getElementById(rootElement.id + "_WebWriterControlOptions");
        if (input != null) {
            postData = input.value;
        };

        var result="";
        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (textStatus == "success") {
                //var responseData = JSON.parse(responseText);
                //以下代替json处理
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var message = temparray2[0];
                var resultstring = temparray2[1];        

                if (success == "true") {
                    result = DCDomTools.HTMLDecode(resultstring);
                } else {
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
            rootElement.allTimeBeforeReturn('getHtmlByXMLString')
        };
        var dataobj = {
            "params": obj,
            "webopts": postData
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: false,
            data: encodeURIComponent(JSON.stringify(dataobj)),
            error: function (responseText, textStatus, jqXHR) {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(JSON.stringify(settings.data))
        $.ajax(settings);
        return result;
    };

    rootElement.getNavigator = function () {
        var win = rootElement.GetContentWindow();
        if (win != null && win.WriterCommandModuleTools) {
            return win.WriterCommandModuleTools.GetNavigator();
        }

    }

    //签名
    rootElement.CASignature = function (obj, callback) {
        rootElement.allTimeAfterCreate()
        var win = rootElement.GetContentWindow();
        var imgElement = null;
        if (win != null && win.DCFileUploadManager) {
            //插入签名图片
            imgElement = win.DCFileUploadManager.InsertSignImage(obj);
        }

        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?casignature=xml" +
            "&contentrendermode=" + rootElement.getAttribute("contentrendermode") +
            "&elementid=" + obj.id + 
            "&role=" + obj.role +
            "&tick=" + new Date().valueOf();
        // 创建服务URL地址
        url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”
        var postData = this.GetContentHtml(true, true);

        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (textStatus == "success") {
                //var responseData = JSON.parse(responseText);
                //以下代替json处理
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var message = temparray2[0];
                var resultstring = temparray2[1];
                if (success == "true") {                   
                    //if (callback) { //wyc20220802:取消回调改成直接加载文档HTML
                    //    var responseData = new Object();
                    //    responseData.result = resultstring;// DCDomTools.HTMLDecode(resultstring);
                    //    responseData.success = DCDomTools.HTMLDecode(success);
                    //    responseData.message = DCDomTools.HTMLDecode(message);
                    //    callback(responseData);
                    //}
                    rootElement.ServerMessage = message;
                    rootElement.CallbackForLoadDocumentHtml(resultstring, textStatus, jqXHR)
                } else {
                    imgElement.parentNode.removeChild(imgElement);
                    rootElement.RuntimeError(message);
                }
            } else {
                imgElement.parentNode.removeChild(imgElement);
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
            rootElement.allTimeBeforeReturn('CASignature')
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            //async: false,
            data: postData,
            error: function (responseText, textStatus, jqXHR) {
                imgElement.parentNode.removeChild(imgElement);
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(postData)
        $.ajax(settings);
    };
    //重新签名
    rootElement.CAReSignature = function (obj, callback) {
        rootElement.allTimeAfterCreate()
        var imgElement = rootElement.GetContentDocument().getElementById(obj.id);
        if (!imgElement) {
            Error("未获找到重签的签名图片！");
            return;
        }
        if (obj.role && obj.role == "1") {
            imgElement.setAttribute("dc_bstrpfx", obj.bstrpfx);
            imgElement.setAttribute("dc_bstrpwd", obj.bstrpwd);
        }
        
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?casignature=xml" +
            "&contentrendermode=" + rootElement.getAttribute("contentrendermode") +
            "&elementid=" + obj.id +
            "&role=" + obj.role +
            "&tick=" + new Date().valueOf();
        // 创建服务URL地址
        url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”
        var postData = this.GetContentHtml(true, true);

        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (textStatus == "success") {
                var responseData = JSON.parse(responseText);
                if (responseData.success == "true") {
                    responseData.result = responseData.result;// DCDomTools.HTMLDecode(responseData.result);
                    if (callback) {
                        callback(responseData);
                    }
                } else {
                    rootElement.RuntimeError(responseData.message);
                }
            } else {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
            rootElement.allTimeBeforeReturn('CAReSignature')
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            //async: false,
            data: postData,
            error: function (responseText, textStatus, jqXHR) {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(postData)
        $.ajax(settings);
    };

    //wyc20221110:获取多文档混合合并后的预览HTML
    //@param obj: {
    //    Files: [
    //        [],
    //        [],
    //        []
    //    ],
    //    UseBase64: "false"
    //    WaterMark: null
    //}
    //obj为文档xml多维数组，每一项数组内个数为1时则作为独立文档单独合并到主文档，独立文档使用自身的页眉页脚
    //若数组个数为2个以上则先加载到子文档并合并到独立文档后作为整体再合并到主文档，此时独立文档的页眉页脚由第一个子文档提供
    rootElement.GetPrintPreviewHTMLByMixedFiles = function (obj, callback) {
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?dcwritergetpreviewhtmlbymixfiles=xml&tick=" + new Date().valueOf();

        var postData = "";
        var input = document.getElementById(rootElement.id + "_WebWriterControlOptions");
        if (input != null) {
            postData = input.value;
        };

        var funcCallback = function (responseText, textStatus, jqXHR) {
            if (textStatus == "success") {
                //var responseData = JSON.parse(responseText);
                //以下代替json处理
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var message = temparray2[0];
                var resultstring = temparray2[1];
                if (success == "true") {
                    var resposeHTML = resultstring;// DCDomTools.HTMLDecode(resultstring);
                    if (callback) {
                        callback(resposeHTML);
                    }
                } else {
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
        };

        var dataObj = {
            "param": obj,
            "webwritercontroloptions": postData
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            data: encodeURIComponent(JSON.stringify(dataObj)),
            error: function (responseText, textStatus, jqXHR) {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        $.ajax(settings);
    };

    //多文档合并成一个预览HTML 异步
    //@param obj:{ 
    //            "files": arr,//数组对象，存的是xml文档 
    //            "format": "xml",//xml文档格式
    //            "base64": "false",//是否是base64字符串
    //            "megedoc": "false"//是否合并
    //            "modefile": "xmlstring"//病程合并模式下提供页眉页脚的主文档XML字符串
    //            "splitmode": "none"//各个文件合并时中间的分隔模式，"none"不分隔，"pagebreak"换新页，"linebreak"，用换行符分隔，"line"，用水平线分隔
    //}
    rootElement.getHtmlByFiles = function (obj, callback) {
        rootElement.allTimeAfterCreate()
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?dcwritergetpreviewhtmlbyfiles=xml&tick=" + new Date().valueOf();

        var postData = "";
        var input = document.getElementById(rootElement.id + "_WebWriterControlOptions");
        if (input != null) {
            postData = input.value;
        };

        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (textStatus == "success") {
                //var responseData = JSON.parse(responseText);
                //以下代替json处理
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var message = temparray2[0];
                var resultstring = temparray2[1];   
                if (success == "true") {
                    var resposeHTML = resultstring;// DCDomTools.HTMLDecode(resultstring);
                    if (callback) {
                        callback(resposeHTML);
                    }
                } else {
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
            rootElement.allTimeBeforeReturn('getHtmlByFiles')
        };

        var dataObj = {
            "param": obj,
            "webwritercontroloptions": postData
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            data: encodeURIComponent(JSON.stringify(dataObj)),
            error: function (responseText, textStatus, jqXHR) {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(JSON.stringify(settings.data))
        $.ajax(settings);
    };


    //@param obj:{ 
    //            "files": arr,//数组对象，存的是xml文档 
    //            "base64": "false",//是否是base64字符串
    //            "megedoc": "false"//是否合并
    //            "modefile": "xmlstring"//病程合并模式下提供页眉页脚的主文档XML字符串
    //            "splitmode": "none"//各个文件合并时中间的分隔模式，"none"不分隔，"pagebreak"换新页，"linebreak"，用换行符分隔，"line"，用水平线分隔
    //            "filename": ""//导出PDF的文件名
    //            "resulttype": ""//返回类型："DownloadFile"表示直接下载文件;"Base64String"表示返回pdf二进制的BASE64字符串
    //            "forceblacktextcolor": "false"//设置导出的PDF强制使用黑色字体
    //}
    rootElement.GetPDFByFiles = function (obj, callback) {
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?dcgetpdfbyfiles=xml&tick=" + new Date().valueOf();

        var postData = "";
        var input = document.getElementById(rootElement.id + "_WebWriterControlOptions");
        if (input != null) {
            postData = input.value;
        };
        var returnbase64 = obj != null && obj.resulttype === "Base64String" ? true : false;
        var result = null;

        if (returnbase64 == false) {
            var frm = document.createElement("form");
            frm.action = url;
            frm.method = "POST";
            frm.target = "_blank";
            var field = document.createElement("input");
            field.type = "hidden";
            field.name = "param";
            field.value = JSON.stringify(obj);
            var field2 = document.createElement("input");
            field2.type = "hidden";
            field2.name = "webwritercontroloptions";
            field2.value = postData;
            frm.appendChild(field);
            frm.appendChild(field2);
            document.body.appendChild(frm);
            frm.submit();
            document.body.removeChild(frm);

        } else {
            var dataobj = {
                "param": obj,
                "webwritercontroloptions": postData
            }
            var settings = {
                method: "POST",
                type: "POST",
                url: url + "&postbyajax=1",
                async: false,
                data: encodeURIComponent(JSON.stringify(dataobj)),
                success: function (responsedata) {
                    result = responsedata;
                },
                error: function (request) {
                    ConnectionError();
                }
            };
            DCDomTools.fixAjaxSettings(settings, rootElement);
            $.ajax(settings);
        }
        return result;
    };

    rootElement.ErrorInfo = {
        Error : "000",
        ErrorMsg : "抱歉，出错了！",
        ConnectionError : "001",//连接失败
        ConnectionErrorMsg : "连接失败！",
        RuntimeError : "002",//后台处理失败
        RuntimeErrorMsg : "抱歉，出错了！",
        NotSupportedError : "003",//不支持
        NotSupportedErrorMsg: "抱歉，出错了！"
    };
    
    rootElement.ConnectionError = function (responseText, textStatus, jqXHR) {
        responseText = DCDomTools.getResponseText(responseText);
        if (responseText == null || responseText.length == 0) {
            alert("未知错误!");
        }
        else {
            this.LoadDocumentFromHtmlText(responseText);
        }
        this.ServerMessage = this.ErrorInfo.ConnectionErrorMsg;
        var eventObject = new Object();
        eventObject.Message = this.ErrorInfo.ConnectionErrorMsg;
        eventObject.State = this.ErrorInfo.ConnectionError;
        this.MessageHandler(eventObject,textStatus,jqXHR);
    }
    rootElement.RuntimeError = function (message) {
        var eventObject = new Object();
        if (message) {
            this.ServerMessage = message;
            eventObject.Message = message;
        } else {
            this.ServerMessage = this.ErrorInfo.RuntimeErrorMsg;
            eventObject.Message = this.ErrorInfo.RuntimeErrorMsg;
        }
        eventObject.State = this.ErrorInfo.RuntimeError;
        this.MessageHandler(eventObject,'RuntimeError',message);
    }

    rootElement.Error = function(message) {
        var eventObject = new Object();
        if (message) {
            this.ServerMessage = message;
            eventObject.Message = message;
        } else {
            this.ServerMessage = this.ErrorInfo.ErrorMsg;
            eventObject.Message = this.ErrorInfo.ErrorMsg;
        }
        eventObject.State = this.ErrorInfo.Error;
        this.MessageHandler(eventObject);
    }

    //处理提示信息
    rootElement.MessageHandler = function (eventObj,textStatus,jqXHR) {
        if (!eventObj) {
            eventObj = new Object();
        }
        eventObj.cancelBubble = false;

        var func = this["EventMessageHandler"];
        if (func && typeof (func) == "function") {
            func.call(rootElement, eventObj)
        }
        if (eventObj.cancelBubble == true) {
            return;
        } else if (eventObj.Message) {
            console.log(eventObj.Message);
        }
        // var win = rootElement.GetContentWindow()
        // if(win && win.DCWriterControllerEditor){
        //     DCWriterControllerEditor.executeWriterControlEventHandler(null, "DocumentLoad");
        // }
        rootElement.raiseDCClientEvent({textStatus:textStatus,Message:jqXHR},'ErrorCallback')
    }
    //多文档合并成一个预览HTML 异步
    //@param obj:{ 
    //            "files": arr,//数组对象，存的是xml文档 
    //            "format": "xml",//xml文档格式
    //            "base64": "false",//是否是base64字符串
    //}
    rootElement.getHtmlsByFiles = function (obj, callback) {
        rootElement.allTimeAfterCreate()
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?dcwritergetpreviewhtmlsbyfiles=xml&tick=" + new Date().valueOf();

        var postData = "";
        var input = document.getElementById(rootElement.id + "_WebWriterControlOptions");
        if (input != null) {
            postData = input.value;
        };

        var win = this.GetContentWindow();
        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (textStatus == "success") {
                //var responseData = JSON.parse(responseText);
                //以下代替json处理
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var message = temparray2[0];
                var resultstring = temparray2[1];               
                if (success == "true") {
                    var resposeHTML = DCDomTools.HTMLDecode(resultstring);
                    if (callback) {
                        callback(resposeHTML);
                    }
                } else {
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
            rootElement.allTimeBeforeReturn('getHtmlsByFiles')
        };

        var dataObj = {
            "param": obj,
            "webwritercontroloptions": postData
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            data: encodeURIComponent(JSON.stringify(dataObj)),
            error: function (responseText, textStatus, jqXHR) {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(JSON.stringify(settings.data))
        $.ajax(settings);
    };

    //数据源绑定生成打印HTML
    //@param obj:{ 
    //            "modeIsBase64":"false",//模板是否是base64字符串
    //            "xmlMode": "",//模板文档 
    //            "dataSourceName": "Patient",//数据源名称
    //            "dataSource": "<a><Name>赵六</Name><Age>43</Age></a>",//数据
    //            "format": "xml",//xml字符串绑定数据源
    //}
    rootElement.getHtmlByFileWithSource = function (obj) {
        rootElement.allTimeAfterCreate()
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?dcwritergetpreviewhtmlbyfilewithsource=xml&tick=" + new Date().valueOf();

        var postData = "";
        var input = document.getElementById(rootElement.id + "_WebWriterControlOptions");
        if (input != null) {
            postData = input.value;
        };

        var result="";
        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (textStatus == "success") {
                //var responseData = JSON.parse(responseText);
                //以下代替json处理
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var message = temparray2[0];
                var resultstring = temparray2[1];          
                if (success == "true") {
                    var resposeHTML = DCDomTools.HTMLDecode(resultstring);
                    result = resposeHTML;
                } else {
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
            rootElement.allTimeBeforeReturn('getHtmlByFileWithSource')
        };

        var dataobj = {
            "param": obj,
            "webwritercontroloptions": postData
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: false,
            data: encodeURIComponent(JSON.stringify(dataobj)),
            error: function (responseText, textStatus, jqXHR) {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(JSON.stringify(settings.data))
        $.ajax(settings);
        return result;
    };

    //根据XML获取HTML
    //@param strXML XML文档的base64字符串
    //@param type {one:整个html，two:body内的html}
    rootElement.getHtmlByXMLBase64String = function (strXML, type) {
        rootElement.allTimeAfterCreate()
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?dcwritergetpreviewhtmlbyxmlbase64string=" + type + "&tick=" + new Date().valueOf();

        var postData = "";
        var input = document.getElementById(rootElement.id + "_WebWriterControlOptions");
        if (input != null) {
            postData = input.value;
        };

        var result="";
        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (textStatus == "success") {
                //var responseData = JSON.parse(responseText);
                //以下代替json处理
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var message = temparray2[0];
                var resultstring = temparray2[1];       
                if (success == "true") {
                    var resposeHTML = DCDomTools.HTMLDecode(resultstring);
                    result = resposeHTML;
                } else {
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
            rootElement.allTimeBeforeReturn('getHtmlByXMLBase64String')
        };

        var dataobj = {
            "fileContent": strXML,
            "webwritercontroloptions": postData
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: false,
            data: encodeURIComponent(JSON.stringify(dataobj)),
            error: function (responseText, textStatus, jqXHR) {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(JSON.stringify(settings.data))
        $.ajax(settings);
        return result;
    };

    //BSDCWRIT-319
    //@param xmlContent 文档字符串
    rootElement.AppendBody = function (xmlContent) {
        var doc = rootElement.GetContentDocument();
        var body = doc.getElementById("divDocumentBody_0");
        var lChild = body.lastChild;
        if (body != null && lChild) {
            var flag = false;
            if (body.childNodes.length == 1 && body.childNodes[0].textContent == " ") {
                flag = true;
            }
            this.InsertXmlString(xmlContent, false, null, lChild, false, null);
            if (flag) {
                body.removeChild(lChild);
            }
        }
    }
    //插入XML
    //@param xmlContent 文档字符串
    //@param flag 是否是病程记录
    //@param options 病程记录属性
    //@param element 指定元素后插入
    //@param async 异步，默认true：异步，false:同步
    //@param func 回调函数 function(ele){//ele插入的内容}
    rootElement.InsertXmlString = function (xmlContent, flag, option, element, async, func) {
        var win = this.GetContentWindow();
        if (!win || (!element && win.DCWriterControllerEditor.CanInsertElementAtCurentPosition() == false)) {
            // 在光标处插入时，判断是否可以插入元素【InsertXmlString,InsertXmlBase64String】
            return false;
        }
        rootElement.allTimeAfterCreate()
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?dcwriterinsertxmlstring=xml&controlinstanceid=" + rootElement.getAttribute("controlinstanceid")
            + "&contentrendermode=" + rootElement.getAttribute("contentrendermode") + "&tick=" + new Date().valueOf();
        rootElement.ShowAppProcessing();
        var postData = "";
        var input = document.getElementById(rootElement.id + "_WebWriterControlOptions");
        if (input != null) {
            postData = input.value;
        };
        var result = false;
        var flag = DCDomTools.toBoolean(flag, false);
        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (rootElement.CheckForWattingMessage(responseText) == true) {
                // 检测到延时等待信息，则退出本操作。
                return;
            }
            if (textStatus == "success") {
                //var responseData = JSON.parse(responseText);
                //以下代替json处理
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var message = temparray2[0];
                var resultstring = temparray2[1];     

                if (success == "true") {
                    var tickRender = new Date().valueOf();
                    var resposeHTML = DCDomTools.HTMLDecode(resultstring);
                    if (resposeHTML != null && resposeHTML.length > 0 && win != null && win.DCWriterControllerEditor != null) {
                        if(flag == false){
                            resposeHTML = DCDomTools.RemoveHtmlParagraphElement(resposeHTML);
                        }
                        //wyc20201218：插入元素覆盖当前选中的元素
                        var sel = win.getSelection();
                        if (sel != null && sel.isCollapsed == false) {
                            win.DCDomTools.delectNode(true);
                        }
                        //////////////////////////
                        if (element) {
                            var div = win.DCWriterControllerEditor.InsertHtmlAfterElement(resposeHTML, flag, option, element);
                        } else if (flag == true) {
                            var div = win.DCWriterControllerEditor.InsertHtmlAtCurentPositionFromCourseRecord(resposeHTML, flag, option);
                        } else {
                            var div = win.DCWriterControllerEditor.InsertHtmlAtCurentPosition(resposeHTML);
                        }
                        rootElement.HiddenAppProcessing();
                        result = true;
                        !!func && typeof (func) === "function" && !!div && func.call(rootElement, div);
                    }
                } else {
                    rootElement.HiddenAppProcessing();
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.HiddenAppProcessing();
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
            rootElement.allTimeBeforeReturn('InsertXmlString')
        };

        var dataobj = {
            "fileContent": xmlContent,
            "webwritercontroloptions": postData
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: async,
            data: encodeURIComponent(JSON.stringify(dataobj)),
            error: function (responseText, textStatus, jqXHR) {
                rootElement.HiddenAppProcessing();
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(JSON.stringify(settings.data))
        $.ajax(settings);
        //同步 返回
        if (async==false) {
            return result;
        }
    };

    //插入XML
    //@param xmlContent 文档Base64字符串
    //@param flag 是否是病程记录
    //@param options 病程记录属性
    //@param element 指定元素后插入
    //@param async 异步，默认true：异步，false:同步
    //@param func 回调函数 function(ele){//ele插入的内容}
    rootElement.InsertXmlBase64String = function (xmlContent, flag, option, element, async, func) {
        var win = this.GetContentWindow();
        if (!win || (!element && win.DCWriterControllerEditor.CanInsertElementAtCurentPosition() == false)) {
            // 在光标处插入时，判断是否可以插入元素【InsertXmlString,InsertXmlBase64String】
            return false;
        }
        rootElement.allTimeAfterCreate()
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?dcwriterinsertxmlbase64string=xml&controlinstanceid=" + rootElement.getAttribute("controlinstanceid")
            + "&contentrendermode=" + rootElement.getAttribute("contentrendermode") + "&tick=" + new Date().valueOf();
        rootElement.ShowAppProcessing();
        var postData = "";
        var input = document.getElementById(rootElement.id + "_WebWriterControlOptions");
        if (input != null) {
            postData = input.value;
        };
        var result = false;
        var flag = DCDomTools.toBoolean(flag, false);
        var funcCallback = function (responseText, textStatus, jqXHR) {
            if (rootElement.CheckForWattingMessage(responseText) == true) {
                // 检测到延时等待信息，则退出本操作。
                return;
            }
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (textStatus == "success") {
                //var responseData = JSON.parse(responseText);
                //以下代替json处理
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var message = temparray2[0];
                var resultstring = temparray2[1];   

                if (success == "true") {
                    var resposeHTML = DCDomTools.HTMLDecode(resultstring);
                    if (resposeHTML != null && resposeHTML.length > 0 && win != null && win.DCWriterControllerEditor != null) {
                        if(flag == false){
                            resposeHTML = DCDomTools.RemoveHtmlParagraphElement(resposeHTML);
                        }
                        //wyc20201218：插入元素覆盖当前选中的元素
                        var sel = win.getSelection();
                        if (sel != null && sel.isCollapsed == false) {
                            win.DCDomTools.delectNode(true);
                        }
                        if (element) {
                            var div = win.DCWriterControllerEditor.InsertHtmlAfterElement(resposeHTML, flag, option, element);
                        } else if (flag == true) {
                            var div = win.DCWriterControllerEditor.InsertHtmlAtCurentPositionFromCourseRecord(resposeHTML, flag, option);
                        } else {
                            var div = win.DCWriterControllerEditor.InsertHtmlAtCurentPosition(resposeHTML);
                        }
                        rootElement.HiddenAppProcessing();
                        result = true;
                        !!func && typeof (func) === "function" && !!div && func.call(rootElement, div);
                    }
                } else {
                    rootElement.HiddenAppProcessing();
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.HiddenAppProcessing();
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
            rootElement.allTimeBeforeReturn('InsertXmlBase64String')
        };

        var dataobj = {
            "fileContent": xmlContent,
            "webwritercontroloptions": postData
        };

        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: async,
            data: encodeURIComponent(JSON.stringify(dataobj)),
            error: function (responseText, textStatus, jqXHR) {
                rootElement.HiddenAppProcessing();
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(JSON.stringify(settings.data))
        $.ajax(settings);
        //同步 返回
        if (async == false) {
            return result;
        }
    };

    //wyc20220830:批量获取元素的innerxml
    rootElement.GetElementsInnerXmlByIDs = function (ids) {
        var win = this.GetContentWindow();
        var doc = this.GetContentDocument();
        if (Array.isArray(ids) === false || win == null || doc == null) {
            return null;
        }
        var htmlarr = new Array();
        var resultarr = null;
        for (var i = 0; i < ids.length; i++) {
            var element = null;
            var id = ids[i];
            if (typeof (id) == "string") {
                element = doc.getElementById(id);
            } else if (id.nodeName) {
                element = id;
            }
            if (element == null) {
                continue;
            }
            var html = win.WriterCommandModuleFile.GetElementHTMLContent(element);
            htmlarr.push(html);
        }
        var servicePageUrl = rootElement.getAttribute("servicepageurl");;

        var url = servicePageUrl + "?" + "dcwritergetxmlcontent=1"
            + "&batchmode=true"
            + "&innermode=true";
        var funcCallback = function (responseText, textStatus, jqXHR) {
            if (rootElement.CheckForWattingMessage(responseText) == true) {
                // 检测到延时等待信息，则退出本操作。
                return;
            }
            rootElement.allTimeAfterAjax(responseText, jqXHR)
            if (textStatus == "success") {
                resultarr = JSON.parse(responseText);
            } else {
                rootElement.HiddenAppProcessing();
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: false,
            data: encodeURIComponent(JSON.stringify(htmlarr)),
            error: function (responseText, textStatus, jqXHR) {
                rootElement.HiddenAppProcessing();
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        $.ajax(settings);

        return resultarr;

    };


    //获取元素内XML
    rootElement.GetElementInnerXmlById = function (id) {
        var win = this.GetContentWindow();
        var doc = this.GetContentDocument();
        if (win != null && doc != null && win.WriterCommandModuleFile != null && id) {
            var element = null; //win.DCWriterControllerEditor.getInnerElementById(id); //废弃原先写法 wyc20211125
            if (typeof (id) == "string") {
                element = doc.getElementById(id);
            } else if (id.nodeName) {
                element = id;
            }
            return win.WriterCommandModuleFile.getInnerXmlByElement(element);
        }
        else {
            return "";
        }
    };

    //wyc20220819:获取系统性能日志展示页面
    rootElement.GetServerLog = function (filename) {
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var parameter = filename != null && filename.toString ? filename : "1";
        var url = servicePageUrl + "?serverlog=" + parameter;
        if (parameter == "1") {
            var win = window.open(url);
            return;
        } else {
            var returnhtmlstr = null;
            var funcCallback = function (responseText, textStatus, jqXHR) {
                if (rootElement.CheckForWattingMessage(responseText) == true) {
                    // 检测到延时等待信息，则退出本操作。
                    return;
                }
                returnhtmlstr = responseText;
            };
            var settings = {
                method: "POST",
                type: "POST",
                url: url,
                async: false,
                error: function (responseText, textStatus, jqXHR) {
                    rootElement.HiddenAppProcessing();
                    rootElement.ConnectionError(responseText, textStatus, jqXHR);
                },
                success: funcCallback
            };
            DCDomTools.fixAjaxSettings(settings, rootElement);
            $.ajax(settings);
        }
        var win = window.open("");
        win.document.write(returnhtmlstr);
        //return returnhtmlstr;
    };

    //设置病程状态
    //editable 可否编辑
    //strStyle 病程样式
    rootElement.SetSubDocumentState = function (editable, strStyle) {
        var win = this.GetContentWindow(); 
        if (win != null && win.DCWriterControllerEditor != null) {
            var focusNode = win.getSelection().focusNode;
            var subDoc = getSubDocument(focusNode);
            if (subDoc) {
                win.DCWriterControllerEditor.EditorSetState(subDoc, editable, strStyle);
            }
        }
    };

    function getSubDocument(focusNode) {
        if (!focusNode) {
            return null;
        }

        if (focusNode.nodeName == "DIV" && focusNode.attributes["dctype"].value == "XTextSubDocumentElement") {
            return focusNode;
        } else if (focusNode.nodeName == "DIV" && focusNode.attributes["dctype"].value == "XTextDocumentBodyElement") {
            return null;
        }else {
            return getSubDocument(focusNode.parentNode);
        }
    }

    ///在文档末尾追加病程记录，options参数同InsertSubDocuments
    rootElement.AppendSubDocuments = function (options, func) {
        var doc = rootElement.GetContentDocument();
        var body = doc.getElementById("divDocumentBody_0");
        if (body != null && body.lastChild) {
            this.InsertSubDocuments(options, body.lastChild, func);
            //当文档结束时清空批注
            var win = rootElement.GetContentWindow()
            if(win && win.DCDocumentCommentManager != null){
                win.DCDocumentCommentManager.spanArr = []
                $(doc).find('#dctable_AllContent>tbody>tr>td:nth-child(4) .annotation div').off()
                $(doc).find('#dctable_AllContent>tbody>tr>td:nth-child(4)').remove()
            }
        }
    }

    //@param options:{ 
    //            "Files": 批量生成子文档的文档数组 
    //            "Options": 批量生成子文档的文档选项数组
    //            "afterElement": 在指定的元素后插入，若为空，则在当前位置处插入
    //}
    rootElement.InsertSubDocuments = function (options, afterElement, func) {
        rootElement.allTimeAfterCreate()
        //防御：
        if (options == null || options.Files == null || $.isArray(options.Files) == false) {
            console.log("InsertSubDocuments参数不对");
            return;
        }
        if (options == null || options.Options == null || $.isArray(options.Options) == false
            || options.Files.length != options.Options.length) {
            console.log("InsertSubDocuments参数不对");
            return;
        }
        var win = this.GetContentWindow();
        if ((win == null || win.DCWriterControllerEditor.CanInsertElementAtCurentPosition("XTextSubDocumentElement") == false)
            && afterElement === undefined) {
            console.log("检测到当前有输入域或表格单元格，不允许插入子文档");
            return;
        }
        // 20230116 xym 禁止插入到正文之外的位置【InsertSubDocuments】
        if (!afterElement) {
            var currentNode = rootElement.CurrnetElement(function (node) {
                return node.nodeName == "DIV" && node.getAttribute("dctype") == "XTextDocumentBodyElement";
            });
            if (!currentNode) {
                console.log("当前位置不在正文中，不允许插入子文档");
                return;
            }
        }
        var base64 = "false";
        if (options.Usebase64 == true || options.Usebase64 == "true") {
            base64 = "true";
        }

        var showmaskui = options.ShowMaskUI == true || options.ShowMaskUI == "true" ? true : false;
        var obj = {
            "file": options.Files,//xml文档
            "format": "xml",//xml文档格式
            "base64": base64,//是否是base64字符串
            "type": "two",//{one:整个html，two:body内的html}
            "usesubdoc": "true",
            "forprint": "false",
            "subdocopts": options.Options
        }
        if (showmaskui === true) {
            rootElement.ShowAppProcessing();
        }

        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?dcwritergethtmlbyfile=xml&tick=" + new Date().valueOf();
        var weboptstr = "";
        var input = document.getElementById(rootElement.id + "_WebWriterControlOptions");
        if (input != null) {
            weboptstr = input.value;
        };
        var funcCallback = function (responseText, textStatus, jqXHR) {
            if (rootElement.CheckForWattingMessage(responseText) == true) {
                // 检测到延时等待信息，则退出本操作。
                return;
            }
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (textStatus == "success") {
                //var responseData = JSON.parse(responseText);
                //以下代替json处理
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                //var message = temparray2[0];
                var resultstring = temparray2[1];
                if (success == "true") {
                    // 20220415 xym 暂时不处理HTML字符串
                    var resposeHTML = resultstring;//DCDomTools.HTMLDecode(resultstring);
                    if (resposeHTML != null && resposeHTML.length > 0 && win != null && win.DCWriterControllerEditor != null) {
                        var div = document.createElement("DIV");
                        $(div).html(resposeHTML);
                        var subdocs = div.querySelectorAll("[dctype='XTextSubDocumentElement']");
                        for (var i = 0; i < subdocs.length; i++) {
                            var subdoc = subdocs[i];
                            if (subdoc.hasAttribute("tagvalue")) {
                                var str = subdoc.getAttribute("tagvalue");
                                var obj = win.DCDomTools.ParseAttributeToObject(str);
                                if (obj.css) {
                                    var cssstr = atob(obj.css.toString());
                                    subdoc.setAttribute("style", cssstr);
                                }
                            }
                        }
                        if (afterElement === undefined || afterElement === null) {
                            win.DCWriterControllerEditor.InsertElementAtCurentPosition(div, true);
                        } else {
                            // win.DCWriterControllerEditor.InsertHtmlAfterElement(div.innerHTML, false, null, afterElement);
                            $(afterElement).after($(div));
                            if(win.DCWriterControllerEditor.InitFileContentDom){
                                win.DCWriterControllerEditor.InitFileContentDom(div, true);
                            }
                        }
                        !!func && typeof (func) === "function" && !!div && func.call(rootElement, div);
                        //wyc20201104：添加一个回调事件
                        if (rootElement.EventAfterInsertSubDocuments != null
                            && typeof (rootElement.EventAfterInsertSubDocuments) == "function") {
                            rootElement.EventAfterInsertSubDocuments.call(rootElement, div);
                        }
                    }
                }
            }
            if (showmaskui === true) {
                rootElement.HiddenAppProcessing();
            }
            rootElement.allTimeBeforeReturn('InsertSubDocuments')
        };
        var dataobj = {
            "params": obj,
            "webopts": weboptstr
        };

        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            data: encodeURIComponent(JSON.stringify(dataobj)),
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(JSON.stringify(settings.data))
        $.ajax(settings);
    };

    //插入XML
    //@param obj:{ 
    //            "file": arr,//xml文档 
    //            "format": "xml",//xml文档格式
    //            "base64": "false",//是否是base64字符串
    //            "type":{one:整个html，two:body内的html}
    //            "position":{"start":在目标容器的头部插入，"end":在目标容器的尾部插入}
    //}
    //async 是否是异步；默认true：异步，false:同步
    rootElement.InsertXmlById = function (obj, id, async) {
        rootElement.allTimeAfterCreate()
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?dcwritergethtmlbyfile=xml&tick=" + new Date().valueOf();
        rootElement.ShowAppProcessing();
        var postData = "";
        var input = document.getElementById(rootElement.id + "_WebWriterControlOptions");
        if (input != null) {
            postData = input.value;
        };
        if (obj.position == null) {
            obj.position = "start";
        }

        var win = this.GetContentWindow();
        // 20220225 xym 添加InsertXmlById第三个参数，插入是否是异步；默认true：异步，false:同步
        var isAsync = win.DCDomTools.toBoolean(async, true);

        // wyc20220302 添加ClearFormat参数支持清除插入元素的样式
        var isClearInsertedFormat = obj.ClearFormat === true || obj.ClearFormat === "true";

        var funcCallback = function (responseText, textStatus, jqXHR) {
            if (rootElement.CheckForWattingMessage(responseText) == true) {
                // 检测到延时等待信息，则退出本操作。
                return;
            }
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (textStatus == "success") {
                //var responseData = JSON.parse(responseText);
                //以下代替json处理
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var message = temparray2[0];
                var resultstring = temparray2[1];      
                if (success == "true") {
                    var resposeHTML = resultstring;// DCDomTools.HTMLDecode(resultstring);
                    if (resposeHTML != null && resposeHTML.length > 0 && win != null && win.DCWriterControllerEditor != null) {
                        //wyc20220308：判断只有一个空段落则取消插入操作BSDCWRIT627
                        var div = document.createElement("div");
                        div.innerHTML = resposeHTML;
                        var ps = div.querySelectorAll("p");
                        if (ps.length == 1) {
                            if (ps[0].getAttribute("blank") == "true") {
                                rootElement.HiddenAppProcessing();
                                return;
                            } else {
                                //wyc20221220:如果只有一个段落，直接取段落内的内容
                                div.innerHTML = ps[0].innerHTML;
                            }
                        }
                        /////////////////////////////////////////////////
                        // 20191212 xuyiming 解决DCWRITER-2916
                        resposeHTML = resposeHTML.replace(/\<p/g, "<span").replace(/\<\/p\>/g, "</span><br dcpf='1'/>").toString();
                        var resulthtml = resposeHTML.substring(0, resposeHTML.lastIndexOf("<br dcpf='1'/>"));  //删除最终的换行
                        var ele = win.DCWriterControllerEditor.handleElementById(id, obj.position);
                        // wyc20220302 处理ClearFormat参数
                        if (isClearInsertedFormat === true) {
                            var usestyle = null;
                            if (ele.parentElement.getAttribute("dctype") === "XTextInputFieldElement") {
                                var start = ele.parentElement.querySelector("[dctype='start']");
                                usestyle = start.nextSibling.getAttribute("style");
                            }
                            var nodes = div.querySelectorAll("*");
                            for (var i = 0; i < nodes.length; i++) {
                                var node = nodes[i];
                                if (usestyle == null) {
                                    node.removeAttribute("style");
                                } else {
                                    node.setAttribute("style", usestyle);
                                    node.style.removeProperty("color");
                                    node.style.removeProperty("background-color");
                                }
                                //node.style = ele.style;
                            }
                            resulthtml = div.innerHTML;
                        }
                        ///////////////////////////////////
                        win.DCWriterControllerEditor.InsertHtmlAfterElement(resulthtml, false, null, ele);
                        $(ele).parent("[dctype='XTextInputFieldElement']").find("style").each(function () {
                            var sty = $(win.document).find("style#dccustomcontentstyle");
                            sty.text(sty.text() + $(this).text());
                        });
                        // 20220303 xym 修复BSDCWRIT-620
                        var element = $(ele).parent("[dctype='XTextInputFieldElement']")[0];
                        if (element && element.isContentEditable == true) {
                            var oldText = rootElement.GetElementTextByID(element);
                            if (win.DCInputFieldManager.ValueValidate(element) == null && oldText != "") {
                                win.DCWriterExpressionManager.ExecuteEffectExpression(element);
                            }
                        }
                        rootElement.HiddenAppProcessing();
                    }
                } else {
                    rootElement.HiddenAppProcessing();
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.HiddenAppProcessing();
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
            rootElement.allTimeBeforeReturn('InsertXmlById')
        };
        var data = {
            "params": obj,
            "webopts": postData
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: isAsync,
            data: encodeURIComponent(JSON.stringify(data)),
            error: function (responseText, textStatus, jqXHR) {
                rootElement.HiddenAppProcessing();
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(JSON.stringify(settings.data))
        $.ajax(settings);
    };

    // 修改文档设置
    rootElement.ChangeDocumentSettings = function (options) {
        var svcUrl = rootElement.getAttribute("servicepageurl") + "?changedocumentsettings=1&tick=" + new Date().valueOf();
        var win = rootElement.GetContentWindow();
        var optElement = document.getElementById(rootElement.id + "_WebWriterControlOptions");
        if (optElement != null) {
            options.WebOptions = optElement.value;
        }
        else {
            return false;
        }
        rootElement.ShowAppProcessing();
        options.DocumentHtml = this.GetContentHtml(true, false);//win.WriterCommandModuleFile.EncodeContentHtmlForPost( win.WriterCommandModuleFile.GetFileContentHtml());
        
        var settings = {
            method: "POST",
            type: "POST",
            url: svcUrl,
            data: JSON.stringify( options), 
            //error: function () {
            //    alert("Connection error");
            //},
            complete: rootElement.CallbackForLoadDocumentHtml
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        $.ajax(settings);
    };

    //设置文档全局JS脚本 wyc20220323
    rootElement.SetDocumentGlobalJavaScript = function (scriptstring) {
        var doc = rootElement.GetContentDocument();
        if (scriptstring == null || scriptstring.length == 0 || doc == null) {
            return;
        }
        var scriptblock = doc.getElementById("JavaScriptTextForWebClient");
        if (scriptblock != null) {
            scriptblock.innerHTML = scriptstring.toString();
        } else {
            scriptblock = document.createElement("script");
            scriptblock.setAttribute("type", "text/javascript");
            scriptblock.setAttribute("language", "javascript");
            scriptblock.setAttribute("id", "JavaScriptTextForWebClient");
            scriptblock.innerHTML = scriptstring.toString();
            doc.body.appendChild(scriptblock);
        }
        rootElement.ApplyDocumentOptions();
    };

    //获取文档全局JS脚本 wyc20220323
    rootElement.GetDocumentGlobalJavaScript = function () {
        var doc = rootElement.GetContentDocument();
        if (doc == null) {
            return null;
        }
        var scriptblock = doc.getElementById("JavaScriptTextForWebClient");
        if (scriptblock != null) {
            return scriptblock.innerHTML;
        } else {
            return null;
        }
    };

    //更新文档选项状态 wyc20210902
    rootElement.ApplyDocumentOptions = function () {
        rootElement.allTimeAfterCreate()
        var svcUrl = rootElement.getAttribute("servicepageurl") + "?updatedocumentoptions=1&tick=" + new Date().valueOf();
        var doc = rootElement.GetContentDocument();
        //wyc20220322:追加属性
        var scriptblock = doc.getElementById("JavaScriptTextForWebClient");
        var docbody = doc.getElementById("divDocumentBody_0");
        var viewstate = docbody.getAttribute("dcdocumentviewstate");
        var opts = {
            Options: this.Options,
            DocumentOptions: this.DocumentOptions,
            JavaScriptTextForWebClient: scriptblock != null ? scriptblock.innerHTML : "",
            DocumentViewState: viewstate
        };

        //wyc20211130：新增处理新的全局样式与痕迹样式
        var funcCallback = function (responseText, textStatus, jqXHR) {
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (textStatus !== "success") {
                return;
            }
            var resultobj = JSON.parse(responseText);
            var input = $(document.WriterControl).find("#" + document.WriterControl.id + "_WebWriterControlOptions")[0];
            if (input != null && resultobj.optstring) {
                input.value = resultobj.optstring;
            }
            //wyc20220322追加:
            docbody.setAttribute("dcdocumentviewstate",resultobj.documentviewstate.toString());
            /////////////////
            var doc = rootElement.GetContentDocument();
            if (doc != null) {
                var dccontentstyle = doc.querySelector("[id='dccontentstyle']");
                var usertrackstyle = doc.querySelector("[id='usertrackstyle']");
                if (dccontentstyle.nodeName == "STYLE" && resultobj.dccontentstyle) {
                    dccontentstyle.innerHTML = resultobj.dccontentstyle.toString();
                }
                if (usertrackstyle.nodeName == "STYLE" && resultobj.usertrackstyle) {
                    usertrackstyle.innerHTML = resultobj.usertrackstyle.toString();
                }
            }
            rootElement.allTimeBeforeReturn('ApplyDocumentOptions')
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: svcUrl,
            async: false,
            data: JSON.stringify(opts),
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(settings.data)
        $.ajax(settings);
    };
     
    rootElement.CallbackForLoadDocumentHtml = function (responseText, textStatus, jqXHR, specifyLoadPart) {
        var result = false;
        var scrollPositionX = 0;
        var scrollPositionY = 0;
        var descDoc = rootElement.GetContentDocument();
        if (rootElement.preserveScrollPosition == true && descDoc && descDoc.body != null) {
            scrollPositionX = descDoc.body.scrollLeft;
            scrollPositionY = descDoc.body.scrollTop;
        }
        responseText = DCDomTools.getResponseText(responseText, jqXHR);
        if (responseText == null || responseText.length == 0) {
            rootElement.HiddenAppProcessing();
            responseText = rootElement.GetDCWriterString("JS_ReturnNoneContent_Url", rootElement.getAttribute("servicepageurl"));
        }
        if (textStatus == "success") {
            if (responseText != "#nochanged#") {
                rootElement.LoadDocumentFromHtmlText(responseText, specifyLoadPart);
            }
            result = true;
        }
        else {
            rootElement.LoadDocumentFromHtmlText("<b style='color:red;word-break:break-all;'>" + textStatus + ":<br/>" + this.url + "</b><br/>" + responseText);
        }
        //if (textStatus != "success") {
            // HTTP不是正常的，隐藏等待界面。
            rootElement.HiddenAppProcessing();
        //}
        if (rootElement.preserveScrollPosition == true
            && (scrollPositionX > 0 || scrollPositionY > 0)) {
            $(descDoc).ready(function () {
                if (descDoc.body != null
                    && descDoc.body.scrollLeft == 0
                    && descDoc.body.scrollTop == 0) {
                    // 没有被用户滚动过，试图设置加载文档前的滚动位置。
                    descDoc.body.scrollLeft = scrollPositionX;
                    descDoc.body.scrollTop = scrollPositionY;
                }
            });
        }
        rootElement.allTimeBeforeReturn('')
        return result ;
    };


    //wyc20221114：分别提供页眉，文档体，页脚的XML来组合成一个文档进行加载
    //@param options{
    //    FileFormat: "xml",
    //    UseBase64String: "false",
    //    HeaderContent: "",
    //    BodyContent: "",
    //    FooterContent: ""
    //}
    rootElement.LoadDocumentFromMixString = function (options) {
        if (typeof (options) !== "object") {
            return false;
        }
        rootElement.allTimeAfterCreate()
        DCWriterEnsureJQuery();
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?dcwriterloaddocumentfrommixstring=1"
            + "&contentrendermode=" + rootElement.getAttribute("contentrendermode") + "&tick=" + new Date().valueOf();
        // 关闭打印预览功能
        this.ClosePrintPreview();
        // 显示等待界面
        rootElement.ShowAppProcessing();
        // 创建服务URL地址
        url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”

        // 采用Ajax技术来加载文档内容
        var strWebOptions = document.getElementById(rootElement.id + "_WebWriterControlOptions");
        if (strWebOptions != null) {
            options.WebOptionsString = strWebOptions.value;
        }

        var funcCallback = function (responseText, textStatus, jqXHR) {
            if (jqXHR && jqXHR.getResponseHeader) {
                rootElement.totalServerTicks = jqXHR.getResponseHeader("dcservertick");
            }
            if (rootElement.CheckForWattingMessage(responseText) == true) {
                // 检测到延时等待信息，则退出本操作。
                return;
            }
            rootElement.allTimeAfterAjax(responseText, jqXHR)
            if (textStatus == "success") {
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var responsehtml = temparray2[1];
                var message = temparray2[0];

                if (success == "true") {
                    rootElement.ServerMessage = message;
                    var resposeHTML = responsehtml;
                    if (rootElement.CallbackForLoadDocumentHtml(resposeHTML, textStatus, jqXHR) == true) {
                        rootElement.FileFormat = options.FileFormat;
                    }
                } else {
                    rootElement.HiddenAppProcessing();
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
                rootElement.HiddenAppProcessing();
                rootElement.RuntimeError(responseData.message);
            }
            rootElement.allTimeBeforeReturn('LoadDocumentFromMixString')
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            data: encodeURIComponent(JSON.stringify(options)),
            error: function (responseText, textStatus, jqXHR) {
                rootElement.HiddenAppProcessing();
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        document.WriterControl = rootElement;
        rootElement.allTimeBeforeAjax(settings.data)
        $.ajax(settings);
        return true;
    };

    //编辑器加载文档字符串
    //@param fileContent 文档字符串
    //@param fileFormat 字符串格式
    rootElement.LoadDocumentFromString = function (fileContent, fileFormat, specifyLoadPart) {
        rootElement.allTimeAfterCreate()
        DCWriterEnsureJQuery();
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?dcwriterloaddocumentfromstring=" + encodeURI(fileFormat)
            + "&controlinstanceid=" + rootElement.getAttribute("controlinstanceid")
            + "&contentrendermode=" + rootElement.getAttribute("contentrendermode") + "&tick=" + new Date().valueOf();
        // 关闭打印预览功能
        this.ClosePrintPreview();    
        // 显示等待界面
        rootElement.ShowAppProcessing();
        // 创建服务URL地址
        url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”
         
        // 采用Ajax技术来加载文档内容
        var strWebOptions = document.getElementById(rootElement.id + "_WebWriterControlOptions");
        if (strWebOptions != null) {
            strWebOptions = strWebOptions.value;
        }
        
        //fileContent = encodeURIComponent(fileContent);
        var funcCallback = function (responseText, textStatus, jqXHR) {
            if(jqXHR && jqXHR.getResponseHeader){
                rootElement.totalServerTicks = jqXHR.getResponseHeader("dcservertick");
            }
            if (rootElement.CheckForWattingMessage(responseText) == true) {
                // 检测到延时等待信息，则退出本操作。
                return;
            }
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (textStatus == "success") {
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var responsehtml = temparray2[1];
                var message = temparray2[0];

                //var responseData = JSON.parse(responseText);
                if (success == "true") {
                    rootElement.ServerMessage = message;//responseData.message;
                    var resposeHTML = responsehtml;//DCDomTools.HTMLDecode(responseData.result);
                    if (rootElement.CallbackForLoadDocumentHtml(resposeHTML, textStatus, jqXHR, specifyLoadPart) == true) {
                        //rootElement.FileName = fileName;
                        rootElement.FileFormat = fileFormat;
                    }                    
                } else {
                    rootElement.HiddenAppProcessing();
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
                rootElement.HiddenAppProcessing();
                rootElement.RuntimeError(responseData.message);
            }
            rootElement.allTimeBeforeReturn('LoadDocumentFromString')
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            data: strWebOptions + "%$$%" + fileContent,
            error: function (responseText, textStatus, jqXHR) {
                rootElement.HiddenAppProcessing();
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        document.WriterControl = rootElement;
        rootElement.allTimeBeforeAjax(settings.data)
        $.ajax(settings);

        //伍贻超2019/3/26 添加是否自动保存
        var AutoSaveIntervalInSecond = rootElement.DocumentOptions.BehaviorOptions.AutoSaveIntervalInSecond;
        if (AutoSaveIntervalInSecond != null && AutoSaveIntervalInSecond != "") {
            if (isNumber(AutoSaveIntervalInSecond) && AutoSaveIntervalInSecond > 0) { //判断是否为数字
                //根据AutoSaveIntervalInSecond的值设置每几秒保存一次
                clearInterval(i);
                i = window.setInterval(function AutoSave() {
                    //文档内容是否改变
                    if (rootElement.getModified() === true) {
                        var interval = rootElement.DocumentOptions.BehaviorOptions.AutoSaveIntervalInSecond;
                        if (isNumber(interval) && interval <= 0 && i !== null) {
                            clearInterval(i);//如果文档选项关闭自动保存则清除计数
                            i = null;
                        }

                        var html = "";
                        var win = rootElement.GetContentWindow();
                        if (win !== null && win.WriterCommandModuleFile) {
                            html = win.WriterCommandModuleFile.GetFileContentHtml();
                            html = win.WriterCommandModuleFile.EncodeContentHtmlForPost(html);
                        }
                        // 创建自动保存URL地址
                        var url_AutoSave = servicePageUrl + "?dcwriterdraftsavefilecontent=" + encodeURI(fileName)
                            + "&fileformat=" + fileFormat
                            + "&controlinstanceid=" + rootElement.getAttribute("controlinstanceid")
                            + "&contentrendermode=" + rootElement.getAttribute("contentrendermode") + "&tick=" + new Date().valueOf();
                        url_AutoSave = url_AutoSave.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”
                        rootElement.AutosSaveUrl(url_AutoSave, html);
                    }
                }, AutoSaveIntervalInSecond * 1000);
            }
        }
        return true;
    };

    function GetCurrentIFrameElement(rootElement) {
        for (var iCount = 0; iCount < rootElement.childNodes.length; iCount++) {
            var node = rootElement.childNodes[iCount];
            if (node.nodeName == "IFRAME") {
                if (node.style.display == null || node.style.display.length == 0 ) {
                    return node;
                }
            }
        }
        return null;
    }

    rootElement.CheckForWattingMessage = function (responseText , targetDoc ) {
        var id_divMsg = "divWaittingMsg";
        if (targetDoc == null) {
            var targetFrame = GetCurrentIFrameElement(rootElement);
            if (targetFrame != null) {
                targetDoc = targetFrame.contentWindow.document;
            }
        }
        responseText = DCDomTools.getResponseText(responseText);
        if (responseText != null && responseText.length > 10 && responseText.substr(0, 12) == "waittingmsg:") {
            rootElement.HiddenAppProcessing();
            var msg = responseText.substr(12);
            if (targetDoc != null) { 
                var divMsg = targetDoc.getElementById(id_divMsg);
                if (divMsg == null) {
                    divMsg = targetDoc.createElement("div");
                    divMsg.id = id_divMsg;
                    divMsg.setAttribute("style", "border:2px solid red;background-color:yellow;font-size:20pt;border-radius:5px");
                    targetDoc.body.insertBefore(divMsg, targetDoc.body.firstChild);
                }
                divMsg.style.display = "";
                divMsg.innerText = msg;

                var div2 = targetDoc.getElementById("divWarringOverload2020");
                if (div2 != null) {
                    div2.parentNode.removeChild(div2);
                }
            }
            return true;
        }
        else {
            if (targetDoc != null) {
                var divMsg = targetDoc.getElementById(id_divMsg);
                if (divMsg != null) {
                    divMsg.parentNode.removeChild(divMsg);
                }
            }
        }
        return false;
    }

    //编辑器加载文档Base64字符串
    //@param fileContent 文档字符串
    //@param fileFormat 字符串格式
    rootElement.LoadDocumentFromBase64String = function (fileContent, fileFormat, specifyLoadPart) {
        rootElement.allTimeAfterCreate()
        DCWriterEnsureJQuery();
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        var url = servicePageUrl + "?dcwriterloaddocumentfrombase64string=" + encodeURI(fileFormat)
            + "&controlinstanceid=" + rootElement.getAttribute("controlinstanceid")
            + "&contentrendermode=" + rootElement.getAttribute("contentrendermode") + "&tick=" + new Date().valueOf();
        // 关闭打印预览功能
        this.ClosePrintPreview();    
        // 显示等待界面
        rootElement.ShowAppProcessing();
        // 创建服务URL地址
        url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”

        // 采用Ajax技术来加载文档内容
        var postData = "";
        var input = document.getElementById(rootElement.id + "_WebWriterControlOptions");
        if (input != null) {
            postData = input.value;
        }
        var funcCallback = function (responseText, textStatus, jqXHR) {
            responseText = DCDomTools.getResponseText(responseText, jqXHR);
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (rootElement.CheckForWattingMessage(responseText) == true ) {
                // 检测到延时等待信息，则退出本操作。
                return;
            }
             if (textStatus == "success") {
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var responsehtml = temparray2[1];
                var message = temparray2[0];

                if (success == "true") {
                    var resposeHTML = responsehtml;//DCDomTools.HTMLDecode(responseData.result);
                    rootElement.ServerMessage = message;
                    if (rootElement.CallbackForLoadDocumentHtml(resposeHTML, textStatus, jqXHR, specifyLoadPart) == true) {
                        //rootElement.FileName = fileName;
                        rootElement.FileFormat = fileFormat;
                    }                   
                } else {
                    rootElement.HiddenAppProcessing();
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.HiddenAppProcessing();
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
            rootElement.allTimeBeforeReturn('LoadDocumentFromBase64String')
        };

        var dataObj = {
            "fileContent": fileContent,
            "webwritercontroloptions": postData
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            data: encodeURIComponent(JSON.stringify(dataObj)),
            error: function (responseText, textStatus, jqXHR) {
                rootElement.HiddenAppProcessing();
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            },
            success: funcCallback
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        rootElement.allTimeBeforeAjax(JSON.stringify(settings.data))
        $.ajax(settings);

        //伍贻超2019/3/26 添加是否自动保存
        var AutoSaveIntervalInSecond = rootElement.DocumentOptions.BehaviorOptions.AutoSaveIntervalInSecond;
        if (AutoSaveIntervalInSecond != null && AutoSaveIntervalInSecond != "") {
            if (isNumber(AutoSaveIntervalInSecond) && AutoSaveIntervalInSecond > 0) { //判断是否为数字
                //根据AutoSaveIntervalInSecond的值设置每几秒保存一次
                clearInterval(i);
                i = window.setInterval(function AutoSave() {
                    //文档内容是否改变
                    if (rootElement.getModified() === true) {
                        var interval = rootElement.DocumentOptions.BehaviorOptions.AutoSaveIntervalInSecond;
                        if (isNumber(interval) && interval <= 0 && i !== null) {
                            clearInterval(i); //如果文档选项关闭自动保存则清除计数
                            i = null;
                        }

                        var html = "";
                        var win = rootElement.GetContentWindow();
                        if (win !== null && win.WriterCommandModuleFile) {
                            html = win.WriterCommandModuleFile.GetFileContentHtml();
                            html = win.WriterCommandModuleFile.EncodeContentHtmlForPost(html);
                        }
                        // 创建自动保存URL地址
                        var url_AutoSave = servicePageUrl + "?dcwriterdraftsavefilecontent=" + encodeURI(fileName)
                            + "&fileformat=" + fileFormat
                            + "&controlinstanceid=" + rootElement.getAttribute("controlinstanceid")
                            + "&contentrendermode=" + rootElement.getAttribute("contentrendermode") + "&tick=" + new Date().valueOf();
                        url_AutoSave = url_AutoSave.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”
                        rootElement.AutosSaveUrl(url_AutoSave, html);
                    }
                }, AutoSaveIntervalInSecond * 1000);
            }
        }
        return true;
    };

    //zhangbin 20220412
    //集成创建后获取时间
    rootElement.allTimeAfterCreate = function(){
        //计数器名
        rootElement.CheckPerformanceCounter();
        //函数开始时间
        rootElement.PerformanceCounter.TaskStartTime = new Date();
    }
    // 集成请求前获取时间
    rootElement.allTimeBeforeAjax = function(postData){
        //获取请求字符长度
        rootElement.PerformanceCounter.NetworkSendTextLength = postData.length;
        //获取请求前时间
        rootElement.startTickNetwork = new Date().valueOf();
    }
    // 集成请求后获取时间
    rootElement.allTimeAfterAjax = function(responseText, jqXHR){
        if (rootElement.PerformanceCounter != null) {
            //获取网络传输耗时
            rootElement.PerformanceCounter.NetworkTickSpan = new Date().valueOf() - rootElement.startTickNetwork;
            //获取接收字符长度
            rootElement.PerformanceCounter.NetworkReturnTextLength = responseText.length;
            //获取服务器端耗时
            rootElement.PerformanceCounter.ServerTickSpan = DCDomTools.getServerTickSpan(jqXHR);
        }
        //浏览器开始渲染时间
        rootElement.startTickRender = new Date().valueOf();
    }
    //集成返回前获取时间
    rootElement.allTimeBeforeReturn = function(funName){
        if (rootElement.PerformanceCounter != null) {
            //浏览器结束渲染时间
            rootElement.PerformanceCounter.BrowserRenderTickSpan = new Date().valueOf() - rootElement.startTickRender;
        }
        rootElement.OnEventAfterWorkCompleted(funName);
    }

    // 检查性能计数器
    rootElement.CheckPerformanceCounter = function (strCreator) {
        if (rootElement.PerformanceCounter == null) {
            rootElement.PerformanceCounter = new Object();
            rootElement.PerformanceCounter.creator = strCreator;
        }
    }

    // 触发编辑器控件的EventAfterWork事件
    rootElement.OnEventAfterWorkCompleted = function (workName) {
        var pc = rootElement.PerformanceCounter;
        if (pc != null) {
            pc.creator = workName;
            if (pc.TaskStartTime != null && typeof (pc.TaskStartTime) == "object") {
                pc.TaskTotalTickSpan = new Date().valueOf() - pc.TaskStartTime.valueOf();
                pc.TaskStartTime = null;
            }
            if (typeof (pc.NetworkTickSpan) == "number" && typeof (pc.ServerTickSpan) == "number") {
                // 网络传输耗时等于网络等待总耗时减去服务器端耗时
                pc.NetwrokTransformTickSpan = pc.NetworkTickSpan - pc.ServerTickSpan;
            }
            if (rootElement.serverPerformances != null) {
                pc.MaxOnlineNumber = rootElement.serverPerformances.maxOnlineNumber;
                pc.CurrentOnlineNumber = rootElement.serverPerformances.currentOnlineNumber;
                pc.RealClientNumber = rootElement.serverPerformances.RealClientNumber;
                pc.RealWorkSpeedCount = rootElement.serverPerformances.RealWorkSpeedCount;
            }
        }
        window.setTimeout(function () {
            rootElement.PerformanceCounter = null;
            if (typeof (rootElement.EventAfterWorkCompleted) == "function") {
                rootElement.EventAfterWorkCompleted.call(rootElement, pc);
            }
        }, 10);
    }

    //@method 加载指定名称的文件
    // @param fileName  文件名
    // @param fileFormat 文件格式
    // @returns 布尔值，操作是否成功

    var i = null;
    var filename_d = "";
    rootElement.LoadDocumentFromFile = function (fileName, fileFormat) {
        rootElement.allTimeAfterCreate()
        //var startTick = DCDomTools.GetDateMillisecondsTick(new Date());
        
        DCWriterEnsureJQuery();

        if (rootElement.isShowingDialog(false) == true) {
            // 正在显示对话框，表示执行其他的操作界面，无法执行本操作。
            return false;
        }
        // 关闭打印预览功能
        this.ClosePrintPreview();
        if (fileFormat == null) {
            fileFormat = "xml";
        }
        if (fileName == null || fileName.length == 0) {
            // 传入的文件名为空
            return false;
        }
        // 获得服务器页面地址
        var servicePageUrl = rootElement.getAttribute("servicepageurl");
        // 显示等待界面
        rootElement.ShowAppProcessing();
        // 创建服务URL地址
        var url = servicePageUrl + "?dcwritergetfulldocumenthtml=xml" //+ encodeURI(fileName)
            + "&fileformat=" + fileFormat
            + "&controlinstanceid=" + rootElement.getAttribute("controlinstanceid")
            + "&contentrendermode=" + rootElement.getAttribute("contentrendermode") + "&tick=" + new Date().valueOf();
        url = url.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”
        var scrollPositionX = 0;
        var scrollPositionY = 0;
        var descDoc = this.GetContentDocument();
        rootElement.startTimeForLoadDocumentFormFile = new Date();

        if (rootElement.preserveScrollPosition == true && descDoc.body != null) {
            scrollPositionX = descDoc.body.scrollLeft;
            scrollPositionY = descDoc.body.scrollTop;
        }
        var funcCallback = function (responseText, textStatus, jqXHR) {
            if(jqXHR && jqXHR.getResponseHeader){
                rootElement.totalServerTicks = jqXHR.getResponseHeader("dcservertick");
            }
            responseText = DCDomTools.getResponseText(responseText, jqXHR);
            rootElement.allTimeAfterAjax (responseText, jqXHR)
            if (rootElement.CheckForWattingMessage(responseText) == true) {
                // 检测到延时等待信息，则退出本操作。
                return;
            }
            if (textStatus == "success") {
                var temparray = responseText.split("$dcsuccesssplit$");
                var success = temparray[0];
                var temparray2 = temparray[1].split("$dcmessageplit$");
                var responsehtml = temparray2[1];
                var message = temparray2[0];

                if (success == "true") {
                    var resposeHTML = responsehtml;//DCDomTools.HTMLDecode(responseData.result);
                    rootElement.ServerMessage = message;
                    if (rootElement.CallbackForLoadDocumentHtml(resposeHTML, textStatus, jqXHR) == true) {
                        rootElement.FileName = fileName;
                        rootElement.FileFormat = fileFormat;                    
                    }
                } else {
                    rootElement.HiddenAppProcessing();
                    rootElement.RuntimeError(message);
                }
            } else {
                rootElement.HiddenAppProcessing();
                rootElement.ConnectionError(responseText, textStatus, jqXHR);
            }
            rootElement.allTimeBeforeReturn('LoadDocumentFromFile')
            return;
        };
        document.WriterControl = rootElement;

        rootElement.InnerLoadDocumentContentFromUrl(url, funcCallback, null, fileName);

        //杜康亮2018/11/9 添加是否自动保存
        // var AutoSaveIntervalInSecond = rootElement.getAttribute("AutoSaveIntervalInSecond");
        var AutoSaveIntervalInSecond = rootElement.DocumentOptions.BehaviorOptions.AutoSaveIntervalInSecond;
        if (AutoSaveIntervalInSecond != null && AutoSaveIntervalInSecond != "") {
            if (isNumber(AutoSaveIntervalInSecond) && AutoSaveIntervalInSecond > 0) { //判断是否为数字
                //根据AutoSaveIntervalInSecond的值设置每几秒保存一次
                clearInterval(i);
                i = window.setInterval(function AutoSave() {
                    //文档内容是否改变
                    if (rootElement.getModified() === true) {
                        var interval = rootElement.DocumentOptions.BehaviorOptions.AutoSaveIntervalInSecond;
                        if (isNumber(interval) && interval <= 0 && i !== null) {
                            clearInterval(i);//如果文档选项关闭自动保存则清除计数
                            i = null;
                        }

                        var html = "";
                        var win = rootElement.GetContentWindow();
                        if (win !== null && win.WriterCommandModuleFile) {
                            html = win.WriterCommandModuleFile.GetFileContentHtml();
                            html = win.WriterCommandModuleFile.EncodeContentHtmlForPost(html);
                        }

                        // 创建自动保存URL地址
                        var url_AutoSave = servicePageUrl + "?dcwriterdraftsavefilecontent=" + encodeURI(fileName)
                            + "&fileformat=" + fileFormat
                            + "&controlinstanceid=" + rootElement.getAttribute("controlinstanceid")
                            + "&contentrendermode=" + rootElement.getAttribute("contentrendermode") + "&tick=" + new Date().valueOf();
                        url_AutoSave = url_AutoSave.replace(/\+/g, "%2B");  //文档传参数转译文档名中的“+”
                        rootElement.AutosSaveUrl(url_AutoSave, html);                        
                    }
                }, AutoSaveIntervalInSecond * 1000);
            }
        }
        //var result = DCDomTools.GetContentByUrl(url, false, func);
        //var tick999 = DCDomTools.GetDateMillisecondsTick(new Date()) - startTick;
        //return DCWriterControllerClass.LoadDocumentFormFile(this, fileName, fileFormat);
        return true;
    };

    //@method 设置当前用户信息
    //@param userID 用户名
    //@param userName 用户姓名
    //@param userLevel 用户授权等级
    //@param savedTime 保存时间
    //@param clientName 客户端名称
    rootElement.SetUserInfo = function (userID, userName, userLevel, savedTime, clientName) {
        var win = this.GetContentWindow();
        var obj = new Object();
        obj.UserID = userID;
        obj.UserName = userName;
        obj.UserLevel = userLevel;
        obj.SavedTime = savedTime;
        obj.ClientName = clientName;
        rootElement.UserInfo = obj;

        if (win != null && win.WriterCommandModuleFile != null) {
            return win.WriterCommandModuleFile.SetUserInfo(userID, userName, userLevel, savedTime, clientName);
        }
        else {
            return true;
        }
    };

    // 保存文件
    // 
    rootElement.SaveDocumentToFile = function (fileName, format) {
        DCWriterEnsureJQuery();

        if (rootElement.isShowingDialog(false) == true) {
            // 正在显示对话框，表示执行其他的操作界面，无法执行本操作。
            return false;
        }
        var win = this.GetContentWindow();
        if (win != null && win.WriterCommandModuleFile != null) {
            return win.WriterCommandModuleFile.OuterFileSave(fileName, format);
        }
        else {
            return true;
        }
    };

    // 重置修改标记
    // onlyText boolean true表示重置Text修改标记
    // 扩展参数 'resetText' 重置文本 'reseltHtml' 重置html 'setModify' 将文档置为已修改状态
    rootElement.resetModified = function (onlyText) {
        if (onlyText == null) {
            onlyText = false;
        }
        var win = this.GetContentWindow();
        if (win != null && win.DCMultiDocumentManager != null) {
            if (onlyText === true || onlyText == 'resetText') {
                return win.DCMultiDocumentManager.resetTextModified();
            } else if(onlyText === false || onlyText == 'reseltHtml'){
                return win.DCMultiDocumentManager.resetModified();
            } else if(onlyText == 'setModify'){
                win.DCMultiDocumentManager.setModify = true
                return true
            }
        }
        else {
            return null;
        }
    };

    // 控件文档内容是否改变
    // onlyText boolean true表示控件文档Text内容是否改变
    rootElement.getModified = function (onlyText) {
        if (onlyText == null) {
            onlyText = false;
        }
        var win = this.GetContentWindow();
        if (win != null && win.DCMultiDocumentManager != null) {
            if (onlyText == true) {
                return win.DCMultiDocumentManager.getTextModified("XTextDocumentBodyElement");
            } else {
                return win.DCMultiDocumentManager.getModified("XTextDocumentBodyElement");
            }
        }
        else {
            return null;
        }
    };

    // 获取内容修改的元素列表 wyc20210819
    rootElement.resetModifiedSpecifyElement = function (element) {
        var win = this.GetContentWindow();
        if (win != null && win.DCMultiDocumentManager != null) {
            return win.DCMultiDocumentManager.resetModifiedSpecifyElement(element);
        }
    };
    // 重置单个支持重置修改功能的元素 wyc20210819
    rootElement.getModifiedElements = function (specifyType, compareTextOnly) {
        var win = this.GetContentWindow();
        if (win != null && win.DCMultiDocumentManager != null) {
            return win.DCMultiDocumentManager.getModifiedElements(specifyType, compareTextOnly);
        }
    };

    rootElement.getAllSubDocumentInfos = function () {
        var win = this.GetContentWindow();
        if (win != null && win.DCMultiDocumentManager != null) {
            return win.DCMultiDocumentManager.getAllDocumentInfos();
        }
        else {
            return null;
        }
    };

    // 获得所有的子文档元素对象
    rootElement.getSubDocuments = function () {
        var win = this.GetContentWindow();
        if (win != null && win.DCSubDocumentManager != null) {
            return win.DCSubDocumentManager.getSubDocuments();
        }
        else {
            return null;
        }
    };

    // 选择指定编号的子文档
    rootElement.selectSubDocument = function (index) {
        var win = this.GetContentWindow();
        if (win != null && win.DCMultiDocumentManager != null) {
            return win.DCMultiDocumentManager.selectDocument(index);
        }
        else {
            return null;
        }
    };

    // 获得当前子文档信息
    rootElement.getCurrentSubDocumentInfo = function () {
        var win = this.GetContentWindow();
        if (win != null && win.DCMultiDocumentManager != null) {
            return win.DCMultiDocumentManager.getCurrentInfo();
        }
        else {
            return null;
        }
    };

    // 新增清除span输入域的固定宽度接口(removeSpecifywidth)
    // el可以传入id或者元素本身
    rootElement.removeSpecifywidth = function (el) {
        if (!el) {
            return null;
        }
        var element;
        if (typeof (el) == "string") {
            var doc = rootElement.GetContentDocument();
            element = doc.getElementById(el);
        } else if (el.nodeName) {
            element = el;
        }
        var win = this.GetContentWindow();
        if (win != null && win.DCInputFieldManager != null) {
            return win.DCInputFieldManager.removeSpecifywidth(element);
        }
        else {
            return null;
        }
    }

    // @method 集成DCFORM前后端通信功能 wyc20210909
    rootElement.DCFormTransmission = function (options) {
        var resultobj = null;
        var url = rootElement.getAttribute("servicepageurl");
        if (url == null ||
            url.length == 0 ||
            options == null ||
            options.FileContentXML == null) {
            return;
        }
        var usebase64 = options.IsUseBase64 === "true" || options.IsUseBase64 === true ? "true" : "false";
        var returnstruct = options.IsReturnStruct === "true" || options.IsReturnStruct === true ? "true" : "false";
        var returnfilecontent = options.IsReturnFileContent === "true" || options.IsReturnFileContent === true ? "true" : "false";
        var bindingdata = options.IsBindingData === "true" || options.IsBindingData === true ? "true" : "false";
        var outputxml = options.OutputElementInnerXML === "true" || options.OutputElementInnerXML === true ? "true" : "false";
        var nestedmode = options.NestedMode === "true" || options.NestedMode === true ? "true" : "false";

        url = url + "?dcformtransmission=1" +
            "&usebase64=" + usebase64 +
            "&returnstruct=" + returnstruct +
            "&returnfilecontent=" + returnfilecontent +
            "&outputxml=" + outputxml +
            "&nestedmode=" + nestedmode +
            "&bindingdata=" + bindingdata + "&tick=" + new Date().valueOf();

        var opts = {
            FileContentXML: options.FileContentXML,
            BindingData: options.Datas
        };
        var settings = {
            method: "POST",
            type: "POST",
            url: url,
            async: false,
            data: JSON.stringify(opts),
            success: function (result) {
                resultobj = JSON.parse(result);
                var temp = resultobj.Structure.toString();
                resultobj.Structure = temp.length === 0 ? null : JSON.parse(temp);
            }
        };
        DCDomTools.fixAjaxSettings(settings, rootElement);
        $.ajax(settings);

        return resultobj;
    };

    // @method 执行编辑器命令
    rootElement.DCExecuteCommand = function (commandName, showUI, parameter) {
        if (rootElement.isShowingDialog() == true) {
            // 表示正在显示对话框，执行其他操作。
            return null;
        }
        var forceUseEditFrame = null;
        //数组中的命令需要强制使用预览contentwindow
        var arr = ["jumpprintmode"];
        if ($.inArray(commandName.toLowerCase(), arr) != -1) {
            forceUseEditFrame = false;
        }
        var win = this.GetContentWindow(forceUseEditFrame);
        if (win != null && win.DCWriterCommandMananger != null) {
            var result = win.DCWriterCommandMananger.DCExecuteCommand(commandName, showUI, parameter);
            //20190715 xuyiming 首行缩进出现问号，“&#8203;”转成问号，“/\u200B/”表示出现“不可见字符”
            if (commandName.toLowerCase() == "indent") {
                var doc = rootElement.GetContentDocument();
                var boddy = doc.getElementsByTagName("body")[0];
                findText(boddy);
            }
            function findText(dom) {
                for (var i = 0; i < dom.childNodes.length; i++) {
                    if (dom.childNodes[i].nodeName == "#text") {
                        if (dom.childNodes[i].nodeValue.match(/\u200B/)) {
                            dom.childNodes[i].nodeValue = dom.childNodes[i].nodeValue.replace(/\u200B/g, '');
                        }
                    } else if (dom.childNodes[i].childNodes) {
                        findText(dom.childNodes[i]);
                    }
                }
            }
            return result;
        }
        else {
            return null;
        }
    };

    rootElement.IsOldIE = function () {
        var result = false;
        if (navigator && navigator.appName == "Microsoft Internet Explorer") {
            var ver = navigator.appVersion;
            if (ver != null) {
                if (ver.indexOf("MSIE 8.") >= 0
                    || ver.indexOf("MSIE 9.") >= 0
                    || ver.indexOf("MSIE 7.") >= 0
                    || ver.indexOf("MSIE 6.") >= 0) {
                    result = true;
                }
            }
        }
        return result;
    };

    rootElement.IsUseBase64Transfer = function () {
        var isoldie = false;
        if (navigator && navigator.appName == "Microsoft Internet Explorer") {
            var ver = navigator.appVersion;
            if (ver != null) {
                if (ver.indexOf("MSIE 8.") >= 0
                    || ver.indexOf("MSIE 9.") >= 0
                    || ver.indexOf("MSIE 7.") >= 0
                    || ver.indexOf("MSIE 6.") >= 0) {
                    isoldie = true;
                }
            }
        }
        var isusebase64_2 = false;
        var win = this.GetContentWindow();
        if (win != null && win.document && win.document.body) {
            isusebase64_2 = win.document.body.getAttribute("transformusebase64") == "true";//这一句不知道会不会出问题
        }
        var isusebase64_1 = this.getAttribute("transformusebase64") == "true"
        if ((isusebase64_1 == true || isusebase64_2 == true) && isoldie == false) {
            return true;
        } else {
            return false;
        }
    };

    rootElement.GetContentHtml = function (inluceWebConrolOptions, encodeData, forceUseEditFrame, enableLogUserHis, isPrintPreview ) {
        var win = this.GetContentWindow(forceUseEditFrame);
        if (win != null && win.WriterCommandModuleFile != null) {
            return win.WriterCommandModuleFile.GetFileContentHtml(inluceWebConrolOptions, encodeData, enableLogUserHis, null, isPrintPreview);                  
        }
        else {
            return "";
        }
    };

    var insertImageButtonID = rootElement.getAttribute("insertimagebuttonid");
    if (insertImageButtonID != null
        && insertImageButtonID.length > 0
        && document.getElementById(insertImageButtonID) != null) {
        // 定义一个函数，将透明的上传图片的内置框架元素放置到上传图片按钮上面。
        var funcBindInsertImageButton = function () {
            if(rootElement == null){
                clearInterval(_funcBindInsertImageButton);
                _funcBindInsertImageButton = null;
                return;
            }
            var frameID = rootElement.id + "_UploadFrameSimple";
            var frameElement = document.getElementById(frameID);
            if (frameElement == null) {
                frameElement = document.createElement("iframe");
                frameElement.id = frameID;
                frameElement.style.opacity = "0.01";
                frameElement.style.position = "absolute";
                frameElement.scrolling = "no";
                rootElement.appendChild(frameElement);
            }
            if (rootElement.isShowingDialog()) {
                // 正在显示对话框，无法执行操作。
                frameElement.style.display = "none";
                return;
            }
            else {
                frameElement.style.display = "";
            }
            if (frameElement.contentWindow != null
                && frameElement.contentWindow.document != null
                && frameElement.contentWindow.document.getElementById("form1") == null) {
                // 内容为空，则设置内容
                var svr = rootElement.getAttribute("servicepageurl");
                if (svr != null && svr.length > 0) {
                    frameElement.src = svr + "?imagemode=1&dcwriteruploadfile=2";
                }
                else {
                    svr = rootElement.getAttribute("referencepathfordebug");
                    if (svr != null && svr.length > 0) {
                        frameElement.src = svr + "/DCUploadFileSimple.htm";
                    }
                }

                var imgID = "img" + new Date().valueOf();
                frameElement.fileID = imgID;
                frameElement.accept = "image/png,image/gif,image/jpeg,image/bmp";
                // 提交操作回调函数
                frameElement.okCallback = function () {
                    if (rootElement.isShowingDialog() == true) {
                        return false;
                    }
                    var man = rootElement.GetContentWindow().DCFileUploadManager;
                    if (man) {
                        if (man.CanUploadImage(true) == false) {
                            return false;
                        }
                        man.UploadImageCallback(imgID);
                    }
                    return true;
                };
            }
            var btn2 = document.getElementById(insertImageButtonID);
            if (btn2 == null) {
                // 没找到按钮
                frameElement.style.display = "none";
                return;
            }
            else {
                frameElement.style.dispaly = "";
            }
            frameElement.style.left = DCDomTools.GetViewLeftInDocument(btn2) + "px";
            frameElement.style.top = DCDomTools.GetViewTopInDocument(btn2) + "px";
            frameElement.style.width = btn2.offsetWidth + "px";
            frameElement.style.height = btn2.offsetHeight + "px";
            frameElement.style.border = "1px solid black";
            frameElement.style.zIndex = 100004;
        };
        var _funcBindInsertImageButton = window.setInterval(funcBindInsertImageButton, 400);
    };

    // 获得文档中所有的单选框或复选框对象
    // type :radio、checkbox 
    // name: 元素的name属性（可不传参数：表示获取所有）
    // specifyRootElement :指定父节点下查找所有单复选框
    rootElement.GetAllCheckboxOrRadio = function (type, name, specifyRootElement) {
        var result;
        var win = this.GetContentWindow();
        if (win != null && win.DCInputFieldManager != null) {
            result = win.DCInputFieldManager.getAllCheckboxOrRadio(type, name, specifyRootElement);
        }
        return result;
    }

    //wyc20210512设置表格列的可见性
    // tableElement :传入的HTML表格DOM，必须是文档元素
    // columnIndex: 设置的表格列的编号，从0开始
    // visible: 设置的表格列的可见性，布尔值
    rootElement.SetTableColumnVisible = function (tableElement, columnIndex, visible) {
        var result;
        var win = this.GetContentWindow();
        if (win != null && win.DCInputFieldManager != null) {
            result = win.WriterCommandModuleTable.SetTableColumnVisible(tableElement, columnIndex, visible);
        }
        return result;
    }

    // 显示控件的遮盖面板 *********************************************************************
    rootElement.showMaskPanel = function (contentElement, delayMilliSeconds) {
        if (typeof (delayMilliSeconds) == "number" && delayMilliSeconds > 0) {
            // 延时显示
            if (rootElement.timeoutForShowMaskPanel != null) {
                // 取消上一次的延时执行的任务
                window.clearTimeout(rootElement.timeoutForShowMaskPanel);
            }
            rootElement.timeoutForShowMaskPanel = window.setTimeout(this, delayMilliSeconds, contentElement);
            return;
        }
        var mask = document.getElementById(this.id + "_ControlMask");
        if (mask != null) {
            mask.style.display = "";
            if ($(rootElement).css("position") != "absolute") {
                rootElement.style.position = "relative";
            }
            var left1 = 0;
            var top1 = 0;

            // if ($(".layui-body").offset() != null) {
            //     left1 = this.offsetLeft;
            //     top1 = this.offsetTop;
            // } else {
            //     left1 = DCDomTools.GetViewLeftInDocument(this);
            //     top1 = DCDomTools.GetViewTopInDocument(this);
            // }

            //mask.style.position = "absolute";
            //修改遮罩position 2019-04-15 hulijun
            if (rootElement.ControlMaskPosition && mask.id.indexOf("_ControlMask")>=0) {
                mask.style.position = rootElement.ControlMaskPosition;
            } else {
                mask.style.position = "absolute";
            }

            mask.style.left = left1 + "px";
            mask.style.top = top1 + "px";
            mask.style.width = "100%";
            mask.style.height = "100%";
            mask.style.overflow = "hidden";
            mask.PoffsetWidth = this.offsetWidth;
            mask.PoffsetHeight = this.offsetHeight;
            // mask.style.width = this.offsetWidth + "px";
            // mask.style.height = this.offsetHeight + "px";
            mask.currentContentElement = contentElement;
            if (contentElement != null) {
                contentElement.style.display = "";

                //contentElement.style.position = "absolute";
                //修改logoposition 2019-04-15 hulijun
                if (rootElement.ProcessingPosition && contentElement.id.indexOf("_Processing")>=0) {
                    contentElement.style.position = rootElement.ProcessingPosition;
                } else if (rootElement.DialogPosition && contentElement.id.indexOf("_Dialog") >= 0) {
                    contentElement.style.position = rootElement.DialogPosition;
                }else {
                    contentElement.style.position = "absolute";
                }

                var v = mask.offsetLeft + (mask.offsetWidth - contentElement.offsetWidth) / 2;
                contentElement.style.left = v + "px";
                v = mask.offsetTop + (mask.offsetHeight - contentElement.offsetHeight) / 2;
                contentElement.style.top = v + "px";
                contentElement.style.zIndex = mask.style.zIndex + 1;
            }
            // 检测遮盖面板布局的函数
            var checkMaskSizeFunc = function () {
                if (mask.style.display != "none") {
                    var parentNode = mask.parentNode;
                    if ($(parentNode).css("position") != "absolute") {
                        parentNode.style.position = "relative";
                    }
                    var left2 = 0;
                    var top2 = 0;
                    // if ($(".layui-body").offset() != null) {
                    //     left2 = parentNode.offsetLeft;
                    //     top2 = parentNode.offsetTop;
                    // } else {
                    //     left2 = DCDomTools.GetViewLeftInDocument(parentNode);
                    //     top2 = DCDomTools.GetViewTopInDocument(parentNode);
                    // }
                    var modified = false;
                    if (mask.style.left != left2 + "px") {
                        mask.style.left = left2 + "px";
                        modified = true;
                    }
                    if (mask.style.top != top2 + "px") {
                        mask.style.top = top2 + "px";
                        modified = true;
                    }
                    if (mask.PoffsetWidth != rootElement.offsetWidth) {
                        mask.PoffsetWidth = rootElement.offsetWidth;
                        modified = true;
                    }
                    if (mask.PoffsetHeight != rootElement.offsetHeight) {
                        mask.PoffsetHeight = rootElement.offsetHeight;
                        modified = true;
                    }
                    // if (mask.style.width != rootElement.offsetWidth + "px") {
                    //     mask.style.width = rootElement.offsetWidth + "px";
                    //     modified = true;
                    // }
                    // if (mask.style.height != rootElement.offsetHeight + "px") {
                    //     mask.style.height = rootElement.offsetHeight + "px";
                    //     modified = true;
                    // }
                    if (modified == true) {
                        var ce = mask.currentContentElement;
                        if (ce != null && ce.style.display != "none") {
                            var v = mask.offsetLeft + (mask.offsetWidth - ce.offsetWidth) / 2;
                            ce.style.left = v + "px";
                            v = mask.offsetTop + (mask.offsetHeight - ce.offsetHeight) / 2;
                            ce.style.top = v + "px";
                        }
                    }
                    window.setTimeout(checkMaskSizeFunc, 400);
                }
            };
            window.setTimeout(checkMaskSizeFunc, 400);
            if (contentElement.focus) {
                contentElement.focus();
            }
            if (contentElement.setActive) {
                try {
                    contentElement.setActive();
                }
                catch (err) {
                }
            }
            return true;
        }
        return false;
    };

    // 隐藏控件的遮盖面板 *********************************************************************
    rootElement.hiddenMaskPanel = function () {
        if (rootElement.timeoutForShowMaskPanel != null) {
            // 取消上一次的延时执行的任务
            window.clearTimeout(rootElement.timeoutForShowMaskPanel);
            rootElement.timeoutForShowMaskPanel = null;
        }
        var lbl = document.getElementById(this.id + "_ControlMask");
        if (lbl != null) {
            lbl.style.display = "none";
        }
    };
    // 显示操作等待界面 ***********************************************************************
    rootElement.ShowAppProcessing = function () {
        rootElement.showMaskPanel(document.getElementById(this.id + "_Processing"), 0);

    };
    // 隐藏操作等待界面 ***********************************************************************
    rootElement.HiddenAppProcessing = function () {
        rootElement.hiddenMaskPanel();
        var lbl2 = document.getElementById(this.id + "_Processing");
        if (lbl2 != null) {
            lbl2.style.display = "none";
        }
    };

    rootElement.GetDialogContentFrameElement = function () {
        return document.getElementById(this.id + "_DialogContent");
    };

    // @method 显示遮盖对话框
    // @param clientWidth 所需的客户区宽度
    // @param clientHeight 所需的客户区高度
    // @param title  标题
    // @returns 对话框容器元素
    rootElement.ShowMaskDialog = function (clientWidth, clientHeight, title) {

        DCWriterEnsureJQuery();

        if (title == null || title.length == 0) {
            title = rootElement.GetDCWriterString("JS_Dialog");
        }
        // 设置标题
        $("#" + this.id + "_DialogTitle").text(title);
        //var titleBox = document.getElementById(this.id + "_DialogTitle");
        //// 清空所有内容
        //while (titleBox.firstChild != null) {
        //    titleBox.removeChild(titleBox.firstChild);
        //}
        //// 设置标题
        //titleBox.appendChild(document.createTextNode(title));
        var dlg = document.getElementById(this.id + "_Dialog");
        var frame = this.GetDialogContentFrameElement();
        // 20200303 xuyiming 对话框显示时先执行默认设置
        if (clientWidth != null) {
            dlg.style.width = (clientWidth + 6) + "px";
            frame.style.width = clientWidth + "px";
        } else {
            dlg.style.width = "430px";
            frame.style.width = "";
        }
        if (clientHeight != null) {
            dlg.style.height = (clientHeight + 22) + "px";
            frame.style.height = clientHeight + "px";
        } else {
            dlg.style.height = "220px";
            frame.style.height = "";
        }
        rootElement.showMaskPanel(dlg);
        return frame;
    };

    // ************************************************************************
    // @method 关闭遮盖对话框
    rootElement.CloseMaskDialog = function (clearContent) {
        var box = document.getElementById(this.id + "_Dialog");
        rootElement.hiddenMaskPanel();
        var box = document.getElementById(this.id + "_Dialog");
        if (box != null) {
            box.style.display = "none";
        }
        var frame = this.GetDialogContentFrameElement();
        if (frame != null && clearContent != false ) {
            frame.src = "about:blank";
        }
    };

    //@method 手动执行所有的表达式 wyc20211115
    rootElement.ExecuteAllEffectExpressions = function () {
        var doc = rootElement.GetContentDocument();
        if (doc == null) {
            return;
        }
        //wyc20220117:改进处理
        for (var fname in doc.body) {
            if (fname.indexOf &&
                fname.indexOf("DCExp") >= 0 &&
                typeof (doc.body[fname]) == "function") {
                doc.body[fname].call();
            }
        }
    };


    // ************************************************************************
    //@method 开始执行上传图片操作
    rootElement.EditUploadImage = function () {
        var frame = document.getElementById(this.id + "_Dialog");
        if (frame != null) {
            if (rootElement.showMaskPanel(frame)) {
                var func = function () {
                    rootElement.hiddenMaskPanel();
                    frame.style.display = "none";
                };
                var win = this.GetContentWindow();
                win.DCFileUploadManager.BeginUploadImage(this.GetDialogContentFrameElement(), func);
            }
        }
    };

    //// 检查是否正在显示对话框
    //rootElement.checkShowingDialog = function () {
    //    if (this.isShowingDialog()) {
    //        alert(rootElement.GetDCWriterString("JS_PromptShowingDialog"));
    //        return false;
    //    }
    //    return true;
    //}

    // 判断是否正在显示对话框
    rootElement.isShowingDialog = function (showMessage) {
        var lbl = document.getElementById(this.id + "_ControlMask");
        if (lbl != null && lbl.style.display != "none") {
            if (showMessage == true) {
                //alert(rootElement.GetDCWriterString("JS_PromptShowingDialog"));
                var eventObject = new Object();
                eventObject.Message = rootElement.GetDCWriterString("JS_PromptShowingDialog");
                eventObject.State = rootElement.ErrorInfo.Error;
                rootElement.MessageHandler(eventObject);
            }
            return true;
        }
        return false;
    };

    // 获得指定名称的字符串资源
    rootElement.GetDCWriterString = function (name, parameters) {
        if (name == null || name.length == 0) {
            return name;
        }
        if (rootElement.DCWriterStringsContainer == null) {
            return name;
        }
        //if (rootElement.DCWriterStringsContainer != null) {
        name = name.toLowerCase();
        var text = rootElement.DCWriterStringsContainer[name];
        if (typeof (text) == "string" && text.length > 0) {
            for (var iCount = 1; iCount < arguments.length; iCount++) {
                var v = arguments[iCount];
                var strV = "";
                if (typeof (v) != "undefined" && v != null) {
                    strV = v.toString();
                }
                text = text.replace("{" + (iCount - 1) + "}", strV);
            }
            return text;
        }
        //}
        return name;
    };

    //获得编辑器的XMLText 张昊 2017-2-15 EMREDGE-28
    rootElement.GetXmlContent = function (containHeaderFooter) {
        var win = this.GetContentWindow();
        if (win != null && win.WriterCommandModuleFile != null) {
            return win.WriterCommandModuleFile.GetXmlContent(undefined, containHeaderFooter);
        }
        else {
            return "";
        }
    };

    //尝试释放内存 wyc20220923
    rootElement.Deconstruct = function () {
        rootElement.ReleaseSession();
        //debugger;
        //试图释放UE
        document.WriterControl.uiEditor.destroy();
        for (var i in document.WriterControl.uiEditor) {
            document.WriterControl.uiEditor[i] = null;
        }
        document.WriterControl.uiEditor = null;
        document.WriterControl.UE = null;
        //试图销毁所有iframe
        var frames = rootElement.querySelectorAll("iframe");
        for (var f = 0; f < frames.length; f++) {
            var frame = frames[f];
            frame.src = 'about:blank';
            try {
                frame.contentWindow.document.write('');
                frame.contentWindow.document.clear();
            } catch (e) { }
            frame.parentNode.removeChild(frame);
        frame = null;
        }
        for (var i in rootElement) {
            if (typeof (rootElement[i]) === "function") {
                rootElement[i] = null;
            }
        }
        document.WriterControl = null;
        rootElement.innerHTML = "";
    }

    //wyc20200703：获取文档页面设置信息
    rootElement.GetDocumentPageSettings = function () {
        var doc = this.GetContentDocument();
        if (doc != null && doc.body != null && doc.body.getAttribute) {
            //var result = new Object();
            //result.PaperKind = doc.body.getAttribute("paperkind");
            //result.PaperHeight = doc.body.getAttribute("paperheight");
            //result.PaperWidth = doc.body.getAttribute("paperwidth");
            //result.LeftMargin = doc.body.getAttribute("leftmargin");
            //result.TopMargin = doc.body.getAttribute("topmargin");
            //result.BottomMargin = doc.body.getAttribute("bottommargin");
            //result.RightMargin = doc.body.getAttribute("rightmargin");
            //result.HeaderDistance = doc.body.getAttribute("headerdistance");
            //result.FooterDistance = doc.body.getAttribute("footerdistance");
            //result.Landscape = doc.body.getAttribute("landscape");
            //result.unit = "Millimeter";
            //wyc20220125：更新写法并加入水印信息
            var str = doc.body.getAttribute("dcdocinfo");
            //str = str.replaceAll("&quot;", "\"");
            str = str.replace(/&quot;/g, "\"");
            var obj = JSON.parse(str);
            if (obj != null) {
                return obj.PageSettings;
            } else {
                return null;
            }
            //return JSON.parse(str);
        } else {
            return null;
        }
    };

    //WYC20200303：设置容器元素的内容只读特性
    rootElement.SetElementContentReadonly = function (element, breadonly) {
        if (element == null) {
            console.log("SetElementContentReadonly的元素为空")
            return;
        }
        if (element.nodeName) {
            elementtype = element.getAttribute("dctype");
        } else if (typeof (element) == "string") {
            var doc = rootElement.GetContentDocument();
            element = doc.getElementById(element);
            if (element == null) {
                console.log("SetElementContentReadonly的元素为空")
                return;
            }
        }

        var win = rootElement.GetContentWindow();
        if (win && win.DCWriterControllerEditor) {
            win.DCWriterControllerEditor.SetElementContentReadonly(element, breadonly);
        }
    };

    //@method DCWriter编辑器控件客户端自我检测
    //@returns 布尔值，检测是否通过。
    rootElement.DCWebWriterControlSelfChecking = function () {
        //debugger;
        if (rootElement.getAttribute('dctype') != 'WebWriterControl') {
            return false;
        }
        var funcShowErrorMessage = function (element, msg) {
            var node = document.createElement('div');
            node.style.fontSize = '20px';
            node.style.fontWeight = 'bold';
            node.style.color = 'red';
            node.style.backgroundColor = 'yellow';
            node.style.border = '1px solid black';
            node.appendChild(document.createTextNode('编辑器控件[' + element.id + ']自检错误：' + msg));
            if (element.firstChild == null) {
                element.appendChild(node);
            }
            else {
                element.insertBefore(node, element.firstChild);
            }
        };

        var servicePageUrl = rootElement.getAttribute('servicepageurl');
        var referencePath = rootElement.getAttribute("referencepathfordebug");
        if ((servicePageUrl == null || servicePageUrl.length == 0) && (referencePath == null || referencePath.length == 0)) {
            funcShowErrorMessage(rootElement, '没配置ServicePageUrl或ReferencePathForDebug属性!');
        }
        else {
            //var settings = { url: servicePageUrl + "?dcwritertest=1" };
            //DCDomTools.fixAjaxSettings(settings, rootElement);
            //$.ajax( settings ).complete(function (jqXHR, textStatus) {
            //    if (jqXHR.status != 200) {
            //        funcShowErrorMessage(rootElement, '配置的服务器页面地址[' + servicePageUrl + ']测试未通过,HTTP状态为:' + xmlhttp.status);
            //    }
            //    else {
            //        var txt = jqXHR.responseText;
            //        if (txt == null || txt.indexOf('dcwriter_test_ok') < 0) {
            //            funcShowErrorMessage(rootElement, '配置的服务器页面地址[' + servicePageUrl + ']测试未通过，返回:' + txt);
            //        }
            //    }
            //});

        }
        return true;
    };

    //@method DCWriter编辑器控件客户端自我检测
    //@returns 布尔值，检测是否通过。
    rootElement.DCWebWriterControlSelfChecking = function () {

    };

    // 左键选中功能菜单 zhangbin20211216
    // configObj配置对象{direction:'',element:[{type:'',style:'',text:'',exec:{}}]}
    //判断是否是双击
    rootElement.MouseLeftSelectFlag = true
    rootElement.MouseLeftSelect = function (configObj) {
        rootElement.GetContentWindow().addEventListener('scroll', function () {
            var innerBody = $(rootElement.GetContentDocument()).find('body');
            var tab = innerBody.find('#dctable_AllContent')
            if (tab.find("#MouseLeftSelect")) {
                tab.find("#MouseLeftSelect").remove();
            }

        })
        $(rootElement.GetContentDocument()).find('body').on('keydown ', function () {
            var tab = $(this).find('#dctable_AllContent')
            if (tab.find("#MouseLeftSelect")) {
                tab.find("#MouseLeftSelect").remove();
            }
        })
        $(rootElement.GetContentDocument()).find('body').on('mouseup', function (e) {
            e = e || window.event
            var tab = $(this).find('#dctable_AllContent')
            if (tab.find("#MouseLeftSelect")) {
                tab.find("#MouseLeftSelect").remove()
            }
            //确定鼠标按下
            if (e.button == '0') {
                configObj = {
                    direction: configObj.direction ? configObj.direction : "vertical",
                    element: configObj.element ? configObj.element : []
                }
                tab.append('<div id="MouseLeftSelect"></div>');
                var outDiv = tab.find('#MouseLeftSelect');
                outDiv.css({
                    'display': 'none',
                    'position': 'fixed',
                    'z-index': '999',
                    'background-color': '#fff',
                    'box-shadow': '0 0 5px rgba(0,0,0,0.5)',
                    'cursor': 'pointer',
                    'font-size': '12px',
                    'text-align': 'center',
                    "border-radius": '5px'
                })
                rootElement.resolveConfig(configObj, outDiv, configObj.direction)
                var button = outDiv[0];
                var sel = rootElement.GetContentWindow().getSelection();
                if (sel.isCollapsed) {
                    rootElement.MouseLeftSelectFlag = false;
                    button.style.display = 'none';
                    outDiv.remove();
                    setTimeout(function(){
                        rootElement.MouseLeftSelectFlag = true;
                    },500)
                } else {
                    if(rootElement.MouseLeftSelectFlag){
                        if (configObj.direction == 'horizonta') {
                            button.style.display = 'flex';
                            button.style.left = (e.clientX - 50) + 'px';
                            button.style.top = (e.clientY - 50) + 'px';
                            //outDiv.find('.arrow').css({ 'transform': 'rotate(90deg)', 'right': '-3px' })
                        } else if (configObj.direction == 'vertical') {
                            button.style.display = 'block';
                            //button.style.width = '100px';
                            button.style.left = e.clientX + 'px';
                            button.style.top = e.clientY + 'px';
                        }
                    }
                }
            }
        })
    }
    //自调用解析配置对象 zhangbin20211216
    rootElement.resolveConfig = function (configObj, outDiv, direction) {
        for (var i = 0; i < configObj.element.length; i++) {
            if (configObj.element[i].type == 'button') {
                outDiv.append('<div class="button">' + configObj.element[i].text + '</div>')
                outDiv.find(".button:nth-child(" + (i + 1) + ")").mousedown(configObj.element[i].exec)
                outDiv.find(".button:nth-child(" + (i + 1) + ")").attr("index", i)
                outDiv.find(".button:nth-child(" + (i + 1) + ")").css({ "padding": "8px" })
                outDiv.find(".button:nth-child(" + (i + 1) + ")").css(configObj.element[i].style)
                outDiv.find(".button:nth-child(" + (i + 1) + ")").hover(function () {
                    $(this).css({ 'outline': '1px solid rgb(220,172,108)', 'background-color': 'rgb(255,245,212)' })
                }, function () {
                    $(this).css({ 'outline': 'none', 'background-color': '#fff' })
                    $(this).css(configObj.element[$(this).attr('index')].style)
                })
            } else if (configObj.element[i].type == 'dropDown') {
                outDiv.append('<div class= dropDown' + i + '>' + configObj.element[i].text + '</div>')
                outDiv.find(".dropDown" + i + ":nth-child(" + (i + 1) + ")").css({ "padding": "8px" })
                var dropDown = outDiv.children('.dropDown' + i)
                dropDown.css({ 'position': 'relative', 'margin': '1px' })
                dropDown.html(dropDown.text() + '<span class="arrow" style="padding-left: 2px">></span>')
                //dropDown.append('<span class=arrow style="position:absolute;right:8px;top:50%;margin-top:-8px"> > </span>')
                dropDown.append('<div class="dropDownDiv"></div>')
                var dropDownDiv = dropDown.children('.dropDownDiv')
                dropDownDiv.css({
                    'display': 'none',
                    'position': 'absolute',
                    'z-index': '1000',
                    'background-color': '#fff',
                    'box-shadow': '0 0 5px rgba(0,0,0,0.5)',
                    'cursor': 'pointer',
                    'font-size': '12px',
                    'text-align': 'center',
                    "border-radius": '5px'
                })
                outDiv.children("[class*=dropDown]").hover(function () {
                    $(this).css({ 'outline': '1px solid rgb(220,172,108)', 'background-color': 'rgb(255,245,212)' })
                    $(this).children('.dropDownDiv').css('display', 'block')
                }, function () {
                    $(this).css({ 'outline': 'none', 'background-color': '#fff' })
                    $(this).children('.dropDownDiv').css('display', 'none')
                })
                dropDown.children('.dropDownDiv').children('[class*=dropDown]').hover(function () {
                    $(this).css({ 'outline': '1px solid rgb(220,172,108)', 'background-color': 'rgb(255,245,212)' })
                    $(this).children('.dropDownDiv').css('display', 'block')
                }, function () {
                    $(this).css({ 'outline': 'none', 'background-color': '#fff' })
                    $(this).children('.dropDownDiv').css('display', 'none')
                })
                if (direction == 'horizonta') {
                    dropDownDiv.css({
                        "bottom": '0px',
                        "left": '0px',
                        //"width": "60px",
                        "transform": 'translateY(95%)',
                        "white-space":"nowrap",
                    })
                } else if (direction == 'vertical') {
                    dropDownDiv.css({
                        "top": '0px',
                        "right": '0px',
                        //"width": "100px",
                        "transform": 'translateX(95%)',
                        "white-space":"nowrap",
                    })
                }
                rootElement.resolveConfig(configObj.element[i], dropDownDiv, direction)
            }
        }
    }

    rootElement.DCDrag = function(dropFun,options){
        if(typeof dropFun == 'function'){
            win.DCWriterControllerEditor.removeDragListener(dropFun,options) 
        }
    }

    //zhangbin 20220801 选中表格行
    rootElement.FocusTableRow = function(color){
        var win = rootElement.GetContentWindow()
        if (win && win.WriterCommandModuleTable) {
            win.WriterCommandModuleTable.FocusTableRow(color);
        }
    }

    //zhangbin 20220802 返回选中的表格行表头和数据
    rootElement.TableCurrentTrResult = function(td,direction){
        var win = rootElement.GetContentWindow()
        if (win && win.WriterCommandModuleTable) {
            return win.WriterCommandModuleTable.TableCurrentTrResult(td,direction);
        }
    }

    //zhangbin20220630 给指定元素添加子节点
    //@param targetEle 目标元素(id/节点)
    //@param newEles 添加节点的数组 input 输入域 checkbox 复选框 radio 单选框 img 图片 button 按钮
    //@param options 插入位置 afterBegin 开始位置 beforeEnd 最后位置 index 坐标
    //@returns 布尔值是否插入成功
    //光标移动不够准确,现操作dom插入的方式
    rootElement.SetChildElements = function(targetEle,newEles,options){
        var returnEle = []
        var win = rootElement.GetContentWindow();
        if (!win) {
            return returnEle;
        }
        // 只读时不在光标处插入内容
        if (rootElement && rootElement.Options) {
            var isReadonly = DCDomTools.toBoolean(rootElement.Options.Readonly, false);
            if (isReadonly == true) {
                return false;
            }
        }
        // 如果没有指定位置插入，需要判断当前是否可以插入元素
        if(!targetEle && win.DCWriterControllerEditor.CanInsertElementAtCurentPosition() == false){
            // 当前位置无法插入
            return returnEle;
        }
        if (typeof (targetEle) == "string") {
            var doc = rootElement.GetContentDocument();
            //表格单元格如果id相同可以使用>拼接id
            if (targetEle.indexOf(' ') >= 0) {
                targetEle = $(targetEle)[0]
            } else { 
                targetEle = doc.getElementById(targetEle);
            }
        }else if(targetEle == null){
            targetEle = rootElement.CurrnetElement()
            if(targetEle.nodeName == '#text'){
                var parentNode = rootElement.CurrentInputField()
                if(parentNode != null){
                    targetEle = parentNode
                }else if(targetEle.parentNode != null && targetEle.parentNode.nodeType == 1){
                    targetEle = targetEle.parentNode
                }
            }
        }
        //插入的元素
        var field = null
        //之前插入元素
        var oldField = null
        // 当前元素的字符长度
        var targetIndex = 0;
        if(targetEle && targetEle.nodeName){
            //插入节点
            if(newEles){
                newEles.forEach(function(item){
                    for(var i in item){
                        if(win != null){
                            switch(i){
                                case 'input':
                                    item[i].SetChildElements = true
                                    field = win.DCInputFieldManager.eventAndDirection(item[i])
                                    win.DCInputFieldManager.FocusOwnerInputField(field);
                                    win.DCInputFieldManager.AddCascade(field);
                                    win.DCInputFieldManager.FixInputElementDom(field);
                                    InsertElement(oldField,field)
                                    break
                                case 'checkbox':
                                    field = win.DCInputFieldManager.createCheckboxOrRadio(item[i])
                                    InsertElement(oldField,field)
                                    break
                                case 'radio':
                                    field = win.DCInputFieldManager.createCheckboxOrRadio(item[i])
                                    InsertElement(oldField,field)
                                    break
                                case 'image':                                
                                    for(var z=0;z<item[i].length;z++){
                                        var img = item[i][z]
                                        //创建图片
                                        field = document.createElement('img')
                                        if(img.width){
                                            field.setAttribute('width', img.width + 'px');
                                            field.style.width = img.width + 'px';
                                        }
                                        if(img.height){
                                            field.setAttribute('height', img.height + 'px');
                                            field.style.height = img.height + 'px';
                                        } 
                                        if (img.id && img.id.length > 0) {
                                            field.setAttribute('id', img.id);
                                        }else{
                                            field.setAttribute('id', img.id ? img.id : 'img' + Math.random());
                                        }                                      
                                        field.src = img.src;                                        
                                        InsertElement(oldField,field)
                                    }
                                    break
                                case 'button':
                                    rootElement.DCExecuteCommand("InsertInputButton", true, item[i])
                                    break
                                case 'text':
                                    field = document.createTextNode(item[i])
                                    InsertElement(oldField,field)
                                    break
                                case 'html':
                                    var spanEle = document.createElement('div')
                                    spanEle.innerHTML = item[i]
                                    for(var z=0;z<spanEle.childNodes.length;z++){
                                        //再次重置
                                        if(oldField != field){
                                            oldField = field
                                        }
                                        field = spanEle.childNodes[z].cloneNode(true)
                                        InsertElement(oldField,field)
                                    }
                                    break
                            }
                        }

                        function InsertElement(oldField,field){
                            if(oldField){
                                oldField.after(field)
                            }else{
                                //根据options查找指定的元素
                                if(options == 'beforeBegin' || options == 'afterBegin' || parseInt(options) <= 0){
                                    //判断是否时输入域
                                    if(targetEle.getAttribute('dctype') == 'XTextInputFieldElement'){
                                        $(targetEle).find('span[dctype=start]:first').after(field)
                                    }else{
                                        if(targetEle.childNodes[0]){
                                            $(targetEle.childNodes[0]).before(field)
                                        }else{
                                            $(targetEle).append(field)
                                        }
                                        
                                    }
                                }else if(options == 'afterEnd' || options == 'beforeEnd'){
                                    //判断是否时输入域
                                    if(targetEle.getAttribute('dctype') == 'XTextInputFieldElement'){
                                        $(targetEle).find('span[dctype=end]:last').before(field)
                                    }else{
                                        $(targetEle).append(field)
                                    }
                                }else{
                                    if(targetEle.getAttribute('dctype') == 'XTextInputFieldElement'){
                                        var newField = getTextIndex(targetEle.childNodes[1],parseInt(options))
                                    }else{
                                        var newField = getTextIndex(targetEle,parseInt(options))
                                    }
                                    if(newField){
                                        newField.after(field)
                                    }else{ //边界值超出元素文本长度
                                        //判断是否时输入域
                                        if(targetEle.getAttribute('dctype') == 'XTextInputFieldElement'){
                                            $(targetEle).find('span[dctype=end]:last').before(field)
                                        }else{
                                            $(targetEle).append(field)
                                        }
                                    }
                                }
                            }
                            //对oldField重新赋值
                            oldField = field
                            returnEle.push(field)
                        }
                        
                        //获取元素的下标
                        function getTextIndex(ele,index){
                            if(typeof index == 'number'){
                                for(var i=0;i<ele.childNodes.length;i++){
                                    if(ele.childNodes[i].nodeName == '#text'){
                                        if(targetIndex + ele.childNodes[i].length >= index){
                                            index -= targetIndex
                                            var textEle1 = document.createTextNode(ele.childNodes[i].nodeValue.substring(0,index))
                                            var textEle2 = document.createTextNode(ele.childNodes[i].nodeValue.substring(index))
                                            ele.childNodes[i].after(textEle2)
                                            ele.childNodes[i].after(textEle1)
                                            $(ele.childNodes[i]).remove()
                                            return textEle1
                                        }else{
                                            targetIndex += ele.childNodes[i].length
                                        }
                                    }else if(ele.childNodes[i].getAttribute('dctype') != 'backgroundtext' && ele.childNodes[i].getAttribute('dctype') != 'start' && ele.childNodes[i].getAttribute('dctype') != 'end'){
                                        var target = getTextIndex(ele.childNodes[i],index)
                                        if(target){
                                            return target
                                        }
                                    }
                                }
                            }
                        }
                    }
                    oldField = field
                })
            }
            // 20221206 xym 插入元素后删除背景文本【SetChildElements】
            if(win.DCInputFieldManager.IsInputFieldElement(targetEle) == true && returnEle.length > 0){
                $(targetEle).find(">[dctype='backgroundtext']").remove();
            }
            return returnEle
        }else{
            return returnEle
        }
    }

    //zhangbin20220705 获取指定元素下的所有节点
    //@param targetEle 目标元素(id/节点)
    rootElement.GetChildElements = function(targetEle,deep){
        if (typeof (targetEle) == "string") {
            var doc = rootElement.GetContentDocument();
            //表格单元格如果id相同可以使用>拼接id
            if(targetEle.indexOf(' ') >= 0){
                targetEle = '#'+ targetEle.slice(0,targetEle.indexOf(' ')+1) + '#' + targetEle.slice(targetEle.indexOf(' ')+1,targetEle.length)
                targetEle = doc.querySelector(targetEle)
            }else{ 
                targetEle = doc.getElementById(targetEle);
            }
        }
        if(targetEle && targetEle.nodeName){
            var win = rootElement.GetContentWindow()
            if(win != null){
                return win.DCDomTools.loopGetAllChildNode(targetEle,deep)
            }
        }
    }

    //zhangbin 20220515 获取单元格属性
    rootElement.GetTableCellAttribute = function(cell){
        var win = rootElement.GetContentWindow();
        return win.WriterCommandModuleTable.TableJudge('GetTableCellAttribute',cell)
    }

    //zhangbin 20220815 设置单元格属性
    rootElement.SetTableCellAttribute = function(cell,options){
        var win = rootElement.GetContentWindow();
        return win.WriterCommandModuleTable.TableJudge('SetTableCellAttribute',cell,options)
    }

    //zhangbin 20220704 单元格对话框
    rootElement.TableCellPopUpWindow = function(cell){
        var win = rootElement.GetContentWindow();
        return win.WriterCommandModuleTable.TableJudge('showTabelCellPopUpWindow',cell)
    }

    //zhangbin 20220805 获取表格行属性
    rootElement.GetTableRowAttribute = function(tr){
        var win = rootElement.GetContentWindow();
        return win.WriterCommandModuleTable.TableJudge('GetTableRowAttribute',tr)
    }

    //zhangbin 20220805 设置表格行属性
    rootElement.SetTableRowAttribute = function(tr,options){
        var win = rootElement.GetContentWindow();
        return win.WriterCommandModuleTable.TableJudge('SetTableRowAttribute',tr,options)
    }

    //zhangbin 20220805 表格行对话框
    rootElement.TableRowPopUpWindow = function(tr){
        var win = rootElement.GetContentWindow();
        return win.WriterCommandModuleTable.TableJudge('showTableRowPopUpWindow',tr)
    }

    //zhangbin 20220811 获取表格属性
    rootElement.GetTableAttribute = function(table){
        var win = rootElement.GetContentWindow();
        return win.WriterCommandModuleTable.TableJudge('GetTableAttribute',table)
    }

    //zhangbin 20220811 获取表格属性
    rootElement.SetTableAttribute = function(table,options){
        var win = rootElement.GetContentWindow();
        return win.WriterCommandModuleTable.TableJudge('SetTableAttribute',table,options)
    }

    //zhangbin 20220811 获取表格属性
    rootElement.TablePopUpWindow = function(table){
        var win = rootElement.GetContentWindow();
        return win.WriterCommandModuleTable.TableJudge('showTablePopUpWindow',table)
    }

    //zhangbin 20220413 BSDCWRIT-680
    rootElement.moveCursorToElement = function(element){
        var container = rootElement.GetContentContainer()
        if(element){
            if(typeof element == 'string'){
                element = rootElement.GetContentDocument().getElementById(element)
            }
            var children = element
        }else{
            var children = container.lastChild
        }
        container.focus()
        rootElement.FocusAdjacent('afterEnd',children)
    }

    //新增一个遮罩层来实现全文只读的效果
    rootElement.ReadOnlyMask = function(flag){
        var win = rootElement.GetContentWindow();
        return win.WriterCommandModuleFile.ReadOnlyMask(rootElement,flag)
    }

    //zhangbin 新增方法返回文档格式树
    rootElement.DOMTreeTools = function(){
        var win = rootElement.GetContentWindow();
        return win.WriterCommandModuleFile.DOMTreeTools()
    }

    //zhangbin 在编辑器中开启内置的工具栏
    rootElement.Toolbar = function(){
        var win = rootElement.GetContentWindow();
        return win.WriterCommandModuleFile.Toolbar()
    }

    //zhangbin 20220816 诊断树结构
    //hierarchy - Expand Merge 
    //symbol - 1,a,A,ⅰ,Ⅰ 默认1  8544  ⅰ 8560
    rootElement.SetDiagnosticTextById = function(ele,value,hierarchy,symbol){
        if(typeof ele == 'string'){
            ele = rootElement.GetContentDocument().getElementById(ele)
        }else{
            ele = ele
        }
        if(!ele){
            ele = rootElement.CurrentInputField()
        }
        if(ele && $.isArray(value)){
            if($(ele).attr('dctype') != 'XTextInputFieldElement'){
                return
            }
            symbol = symbol ? symbol.charCodeAt(0) : '1'.charCodeAt(0)
            var start = $(ele).children('*[dctype=start]:first')[0]
            var end = $(ele).children('*[dctype=end]:first')[0]
            var allChild = $(ele).children()
            var allStyle = ele.style
            var styleArr = ["font-family","font-size","font-style","font-weight","text-decoration"]
            var br = document.createElement('br') //换行标签
            br.setAttribute('dcpf','1')
            var span = document.createElement('span') //包裹文本的span
            var fontSize = ''
            //给span添加上样式
            for(var i=0;i<allStyle.length;i++){
                if($.inArray(allStyle[i],styleArr) >= 0){
                    span.style[allStyle[i]] = allStyle.getPropertyValue(allStyle[i])
                    if(allStyle[i] == 'font-size'){
                        // console.log(allStyle.getPropertyValue(allStyle[i]))
                        // console.log($(span).css('fontSize'))
                        if($(span).css('fontSize').substr(-2) == 'pt'){
                            fontSize = parseInt($(span).css('fontSize')) / 3 * 4
                        }else if($(span).css('fontSize').substr(-2) == 'px'){
                            fontSize = parseInt($(span).css('fontSize'))
                        }
                    }
                }
            }
            // span.style.display = 'inline-block'
            //清除除了开始结束边框外的所有子元素
            for(var i=0;i<allChild.length;i++){
                if(allChild[i] != start && allChild[i] != end){
                    $(allChild[i]).remove()
                }
            }
            //找到其元素并计算之前全部文本的长度
            var containerP = $(ele).parents('p:first')[0]
            var allSelectionText = ''
            var textFlag = false
            returnTextNode(containerP)
            var charLen = ''
            for(var i=0;i<allSelectionText.length;i++){
                if(allSelectionText.charCodeAt(i) > 127 || allSelectionText.charCodeAt(i) == 94){
                    charLen += '&ensp;&ensp;'
                }else{
                    charLen += '&ensp;'
                }
            }

            var beginEle = $(start)
            //拼接字符串
            for(var i=0;i<value.length;i++){
                if(!value[i].mainDX){
                    continue
                }
                if(i > 0){
                    beginEle.after(br.cloneNode())
                    beginEle = beginEle.next()
                }
                //获取复制的span
                //对mainDX进行判断
                var spanClone = span.cloneNode()
                beginEle.after(spanClone)
                var spanCloneHeight = $(spanClone).height() ? $(spanClone).height() : fontSize
                if(i > 0){
                    $(spanClone).html(charLen)
                }
                var insertText = String.fromCharCode(symbol+i) + '、'+ (value[i].mainDX)
                insertTextFun(insertText,spanClone)
                beginEle = beginEle.next()
                if(value[i].secondDX && $.inArray(value[i].secondDX) && value[i].secondDX.length > 0){
                    //插入换行
                    beginEle.after(br.cloneNode())
                    beginEle = beginEle.next()
                    //展开状态下
                    if(hierarchy == 'Expand'){
                        for(var j=0;j<value[i].secondDX.length;j++){
                            var secondClone = span.cloneNode()
                            beginEle.after(secondClone)
                            spanCloneHeight = $(secondClone).height() ? $(secondClone).height() : fontSize
                            $(secondClone).html(charLen + '&ensp;&ensp;&ensp;')
                            var secondText = value[i].secondDX[j]
                            insertTextFun(secondText,secondClone)
                            //beginEle.after($(span.cloneNode()).html((charLen + '&ensp;&ensp;&ensp;') + '、' + value[i].secondDX[j])[0])
                            beginEle = beginEle.next()
                            if(j < value[i].secondDX.length - 1){
                                beginEle.after(br.cloneNode())
                                beginEle = beginEle.next()
                            }
                        }
                    //合并状态下
                    }else{
                        var secondClone = span.cloneNode()
                        beginEle.after(secondClone)
                        spanCloneHeight = $(secondClone).height() ? $(secondClone).height() : fontSize
                        $(secondClone).html(charLen + '&ensp;&ensp;&ensp;')
                        var secondText = ''
                        for(var j=0;j<value[i].secondDX.length;j++){
                            if(j > 0){
                                secondText += '、'
                            }
                            secondText += value[i].secondDX[j]
                        }
                        insertTextFun(secondText,secondClone)
                        beginEle = beginEle.next()
                    }
                }
            }
        }
        
        //对span填写内容
        function insertTextFun(insertText,spanClone){
            for(var j=0;j<insertText.length;j++){
                $(spanClone).html($(spanClone).html() + insertText[j])
                if($(spanClone).height() > spanCloneHeight){
                    //$(spanClone).html($(spanClone).html().substring(0,$(spanClone).html().length-1) + charLen + '&ensp;&ensp;&ensp;&ensp;&ensp;' + insertText[j])
                    $(spanClone).html($(spanClone).html().substring(0,$(spanClone).html().length-1))
                    beginEle = beginEle.next()
                    //插入换行
                    beginEle.after(br.cloneNode())
                    beginEle = beginEle.next()
                    spanClone = span.cloneNode()
                    beginEle.after(spanClone)
                    $(spanClone).html(charLen + '&ensp;&ensp;&ensp;' + insertText[j])
                    spanCloneHeight = $(spanClone).height() ? $(spanClone).height() : fontSize
                }
            }
        }

        //返回文本
        function returnTextNode(el){
            var childNodes = el.childNodes;
            for (var i = 0; i < childNodes.length; i ++) {
                if(textFlag){
                    break
                }
                var c = childNodes[i];
                if(c == ele){
                    textFlag = true
                    break
                }
                if(c.nodeType == 3){
                    allSelectionText += c.textContent
                }else{
                    if(c.nodeName == 'BR'){
                        allSelectionText = ''
                    }
                    returnTextNode(c)
                }
            }
        }
    }

    // 加载初始化的文档内容
    for (var iCount = 0; iCount < rootElement.childNodes.length; iCount++) {
        var node = rootElement.childNodes[iCount];
        if (node.nodeName == "TEXTAREA" && node.getAttribute("dctype") == "DCHtmlSource") {
            var msg = rootElement.GetDCWriterString("JS_LoadingFile_Name", node.title);
            var win = rootElement.GetContentWindow();
            win.document.write(msg);
            win.document.close();

            var htmlresult = node.value;;
            //debugger; wyc20220629注释
            //if (htmlresult.indexOf("&") != -1) {
            //    htmlresult = DCDomTools.HTMLDecode(node.value);
            //}
            rootElement.LoadDocumentFromHtmlText(htmlresult);
            node.parentNode.removeChild(node);

            break;
        }
    } //for

    return true;
};

//@method 自动创建编辑器框架。
function DCWriterAutoLoadControl() {
    if (document.readyState == "complete") {
        var divs = document.getElementsByTagName("DIV");
        if (divs != null && divs.length > 0) {
            for (var iCount = 0; iCount < divs.length; iCount++) {
                var div = divs[iCount];
                if (div.getAttribute("autobuildwebwriterControl") != "true") {
                    continue;
                }
                if (div.FrameBuilded == true) {
                    continue;
                }
                BindingDCWriterClientControl(div);
                div.BuildFrame();
            }
        }
    }
}

if (document.addEventListener) {
    // Use the handy event callback
    document.addEventListener("DOMContentLoaded", DCWriterAutoLoadControl, false);

    // A fallback to window.onload, that will always work
    window.addEventListener("load", DCWriterAutoLoadControl, false);

    // If IE event model is used
} else if (document.attachEvent) {
    // ensure firing before onload,
    // maybe late but safe also for iframes
    document.attachEvent("onreadystatechange", DCWriterAutoLoadControl);
}


var DCTemperatureConfig=new Object();DCTemperatureConfig.NewConfig=function(){var config=new Object();config.GridYSpaceNum="5";config.GridYSplitNum="8";config.NumOfDaysInOnePage="7";config.SpecifyStartDate=null;config.SpecifyEndDate=null;config.Title=null;config.SpecifyTitleHeight="0";config.BigVerticalGridLineColorValue=null;config.GridLineColorValue=null;config.FontName="宋体";config.FontSize="9";config.DateFormatString="yyyy-MM-dd";config.DateFormatStringForCrossMonth="MM-dd";config.DateFormatStringForCrossWeek="dd";config.DateFormatStringForCrossYear="yyyy-MM-dd";config.DateFormatStringForFirstIndexFirstPage="yyyy-MM-dd";config.DateFormatStringForFirstIndexOtherPage="dd";config.PageIndexText="第[%pageindex%]页";config.EnableDataGridLinearAxisMode=false;config.TickTexts="2,6,10,14,18,22";config.TickValues="2,6,10,14,18,22";config.TickColorValues="#FF0000,#FF0000,,,,#FF0000";config.DataGridBottomPadding="0";config.DataGridTopPadding="0";config.BigTitleFontSize="27";config.FooterDescription="";config.ThinLineWidth="1";config.ThickLineWidth="2";config.IllegalTextEndCharForLinux=null;config.FooterLines=new Array();config.HeaderLabels=new Array();config.HeaderLines=new Array();config.YAxisInfos=new Array();config.Labels=new Array();return config;};DCTemperatureConfig.NewHeaderLabel=function(){var label=new Object();label.ParameterName="";label.Title="";label.Value="";return label;};DCTemperatureConfig.NewLabel=function(){var label=new Object();label.Left="0";label.Top="0";label.Width="100";label.Height="100";label.Text="文本框";label.MultiLine=false;label.ShowBorder=false;label.BackColorValue=null;label.TextColorValue=null;label.Alignment="Center";label.LineAlignment="Center";label.TextFontName="宋体";label.TextFontSize="9";label.TextFontBold=false;label.TextFontItalic=false;label.TextFontUnderline=false;label.ImageDataBase64String=null;label.ParameterName="";label.PositionFixModeForAutoHeightLine="None";return label;};DCTemperatureConfig.NewPageSettings=function(){var ps=new Object();ps.PaperSizeName="A4";ps.PaperHeight="296.93";ps.PaperWidth="210.06";ps.Landscape=false;ps.TopMargin="25.4";ps.BottomMargin="25.4";ps.LeftMargin="25.4";ps.RightMargin="25.4";ps.Unit="Millimeter";return ps;};var DCTemperatureControl=new Object();var resultHTML=null;var ServerMessage=null;DCTemperatureControl.PostInfoToBackGroundWithoutCallBack=function(url,content,async){var xmlhttp=null;if(window.XMLHttpRequest){xmlhttp=new XMLHttpRequest();}
else if(window.ActiveXObject){xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");}
if(async==null||async==undefined){async=false;}
if(xmlhttp!==null){xmlhttp.withCredentials=true;xmlhttp.open("POST",url,async);if(content!==null){xmlhttp.send(content);}else{xmlhttp.send();}
var responseobj=JSON.parse(xmlhttp.responseText);if(responseobj!=null){var success=responseobj.success;resultHTML=responseobj.result;ServerMessage=responseobj.message;if(success=="true"){return true;}}
return false;}
else{if(promptError){alert("浏览器不支持XMLHTTP.");return false;}}
return false;};function InitializeDCTemperatureControl(ctl){if(ctl!==null){ctl.OnLinkClick=function(linkString){if(ctl.EventLinkClick!=null&&typeof(ctl.EventLinkClick)=="function"){ctl.EventLinkClick.call(ctl,linkString);}}
ctl.LoadDocumentFromString=function(documentxmlstring,async){var servicepageurl=ctl.getAttribute("servicepageurl");var advancedmode=ctl.getAttribute("advancedmode");var sessionname=ctl.getAttribute("documentbuffername");var url=servicepageurl +"?dctimelineloaddocumentfromfrontend=1"
+"&sessionname="+ sessionname
+"&advancedmode="+ advancedmode
+"&serviceflag=fdjia8324";var content="loaddocumentinfo="+ encodeURIComponent(documentxmlstring);DCTemperatureControl.PostInfoToBackGroundWithoutCallBack(url,content,async);if((advancedmode===true||advancedmode==="true")&&document.TemperatureControl!=null){document.TemperatureControl.Document=JSON.parse(resultHTML);;}};ctl.SaveDocumentToString=function(){resultHTML=null;var servicepageurl=ctl.getAttribute("servicepageurl");var advancedmode=ctl.getAttribute("advancedmode");var sessionname=ctl.getAttribute("documentbuffername");var url=servicepageurl +"?dctimelinesavedocumenttofrontend=1"
+"&sessionname="+ sessionname
+"&advancedmode="+ advancedmode
+"&serviceflag=fdjia8324";var content="1=1";if((advancedmode===true||advancedmode==="true")&&document.TemperatureControl!=null&&document.TemperatureControl.Document!=null){content="temperaturedocument="+ encodeURIComponent(JSON.stringify(document.TemperatureControl.Document));}
DCTemperatureControl.PostInfoToBackGroundWithoutCallBack(url,content,false);return resultHTML;};ctl.SaveDocumentToFile=function(filename){resultHTML=null;var servicepageurl=ctl.getAttribute("servicepageurl");var advancedmode=ctl.getAttribute("advancedmode");var sessionname=ctl.getAttribute("documentbuffername");var url=servicepageurl +"?dctimelinesavedocumenttofile=1"
+"&sessionname="+ sessionname
+"&advancedmode="+ advancedmode
+"&filename="+ filename
+"&serviceflag=fdjia8324";var frm=document.createElement("form");frm.action=url;frm.method="POST";frm.enctype="application/x-www-form-urlencoded";frm.target="_blank";var field=document.createElement("input");field.type="hidden";field.name="1";var postData="1";if((advancedmode===true||advancedmode==="true")&&document.TemperatureControl!=null&&document.TemperatureControl.Document!=null){field.name="temperaturedocument";postData=JSON.stringify(document.TemperatureControl.Document);}
field.value=postData;frm.appendChild(field);document.body.appendChild(frm);frm.submit();document.body.removeChild(frm);};ctl.RefreshView=function(){var servicepageurl=ctl.getAttribute("servicepageurl");var sessionname=ctl.getAttribute("documentbuffername");var advancedmode=ctl.getAttribute("advancedmode");var contentpixelheight=ctl.getAttribute("contentpixelheight");var pixelpagespacing=ctl.getAttribute("pixelpagespacing");var height=ctl.getAttribute("dc_height");var pageshadow=ctl.getAttribute("pageshadow");var url=servicepageurl +"?dctimelinerefreshview=1"
+"&sessionname="+ sessionname
+"&contentpixelheight="+ contentpixelheight
+"&pixelpagespacing="+ pixelpagespacing
+"&advancedmode="+ advancedmode
+"&dc_height="+ height
+"&pageshadow="+ pageshadow
+"&serviceflag=fdjia8324";var content=null;if((advancedmode===true||advancedmode==="true")&&ctl.Document!=null){content="temperaturedocument="+ encodeURIComponent(JSON.stringify(ctl.Document));}
DCTemperatureControl.PostInfoToBackGroundWithoutCallBack(url,content,false);var divprimarycontent=document.getElementById(ctl.id +"_dctimelineprimarycontent");var resultobj=JSON.parse(resultHTML);divprimarycontent.innerHTML=resultobj.timelinehtml;document.TemperatureControl=ctl;document.TemperatureControl.Document=JSON.parse(resultobj.timelinemodel);if(ctl.EventAfterRefreshView!=null&&typeof(ctl.EventAfterRefreshView)=="function"){ctl.EventAfterRefreshView.call(ctl);}};ctl.GetPrintPreviewHTML=function(specifyPageIndexes){var html=null;var contentHTML=null;var isLandscape=document.TemperatureControl!=null&&document.TemperatureControl.Document!=null&&document.TemperatureControl.Document.Config.PageSettings.Landscape===true
if(specifyPageIndexes==null||specifyPageIndexes==undefined||specifyPageIndexes.toString==undefined){var images=document.getElementsByClassName("dctimelinebigimg");if(images.length>0){var divroot=document.createElement("div");var div=document.createElement("div");div.style.pageBreakAfter="always";div.style.pageBreakInside="avoid";div.appendChild(images[0].cloneNode(true));divroot.appendChild(div);contentHTML=divroot.innerHTML;}}else{var specifypageindexes=specifyPageIndexes.toString();var servicepageurl=ctl.getAttribute("servicepageurl");var advancedmode=ctl.getAttribute("advancedmode");var sessionname=ctl.getAttribute("documentbuffername");var url=servicepageurl +"?dctimelinegetpageimage=1"
+"&sessionname="+ sessionname
+"&advancedmode="+ advancedmode
+"&specifypageindexes="+ specifypageindexes
+"&serviceflag=fdjia8324";var content="1=1";if((advancedmode===true||advancedmode==="true")&&document.TemperatureControl!=null&&document.TemperatureControl.Document!=null){content="temperaturedocument="+ encodeURIComponent(JSON.stringify(document.TemperatureControl.Document));}
if(DCTemperatureControl.PostInfoToBackGroundWithoutCallBack(url,content,false)===true){var resultObj=JSON.parse(resultHTML);if(Array.isArray(resultObj)===true&&resultObj.length>0){var divroot=document.createElement("div");for(var i=0;i<resultObj.length;i++){var base64=resultObj[i].toString();var div=document.createElement("div");div.style.pageBreakAfter="always";if(i!==resultObj.length-1){div.style.pageBreakInside="avoid";}
var img=document.createElement("img");img.className="dctimelinebigimg";img.src=base64;div.appendChild(img);divroot.appendChild(div);}
contentHTML=divroot.innerHTML;}}}
if(contentHTML!=null&&contentHTML.length>0){var str="margin-left:0cm;margin-top:0cm;margin-right:0cm;margin-bottom:0cm";if(isLandscape===true){str=str +";size:landscape";}
var ps="@page{"+ str +"}";html="<html><head><style> P{margin:0px}  "+ ps +" </style>";html=html +"</head><body style='padding-left:0px;padding-top:0px;padding-right:0px;padding-bottom:0px;margin:0px'>";html=html + contentHTML;html=html +"</body></html>";}
return html;};ctl.PrintDocument=function(specifyPageIndexes,timeout){var isLandscape=document.TemperatureControl!=null&&document.TemperatureControl.Document!=null&&document.TemperatureControl.Document.Config.PageSettings.Landscape===true
var html=ctl.GetPrintPreviewHTML(specifyPageIndexes);var wind=window.open("","_blank","menubar=no,toolbar=no,location=no,scrollbars=no,resizable=yes");wind.document.write(html);$(wind.document).ready(function(){var imgs=wind.document.querySelectorAll(".dctimelinebigimg");for(var i=0;i<imgs.length;i++){var img=imgs[i];if(isLandscape===true){img.style.height=(img.offsetHeight-1)+"px";}
maskDiv=document.createElement("div");maskDiv.style.position="absolute";maskDiv.style.backgroundColor="white";maskDiv.style.zIndex="11000";maskDiv.style.top=img.offsetTop +"px";maskDiv.style.width=img.offsetWidth/3 +"px";maskDiv.style.height="20px";img.parentElement.insertBefore(maskDiv,img);}
var iv=1500;try{if(timeout!=null){iv=parseInt(timeout);}}catch(error){iv=1500;}
var i=wind.setTimeout(function(){clearTimeout(i);wind.print();wind.close();},iv);});};ctl.LoadDocumentFromFile=function(){var input=document.getElementById("dcwritertempinputforfileopen");if(input===null){input=document.createElement("input");input.id="dcwritertempinputforfileopen";input.type="file";input.name="file";input.accept="text/xml, application/xml";input.style.display="none";input.onchange=function(){fileurl=window.URL.createObjectURL(input.files[0]);input.remove();if(fileurl!=null){$.ajax({url:fileurl,dataType:'text',type:'GET',contentType:"text/plain;charset=UTF-8",async:false,timeout:2000,error:function(xml){alert("加载XML 文件出错！");},success:function(xml){var ctl=document.TemperatureControl;if(ctl!=null){ctl.LoadDocumentFromString(xml,false);ctl.RefreshView();}}});}
return;};document.body.appendChild(input);input.click();}else{input.click();}}
ctl.NewConfig=function(){return DCTemperatureConfig.NewConfig();};ctl.NewYAxis=function(){return DCTemperatureYAxis.NewYAxis();};ctl.NewTitleLine=function(){return DCTemperatureTitleLine.NewTitleLine();};ctl.NewHeaderLabel=function(){return DCTemperatureConfig.NewHeaderLabel();};ctl.NewDocument=function(){return DCTemperatureDocument.NewDocument();};ctl.NewValuePoint=function(){return DCTemperatureValuePoint.NewValuePoint();};ctl.NewLabel=function(){return DCTemperatureConfig.NewLabel();};ctl.NewPageSettings=function(){return DCTemperatureConfig.NewPageSettings();};ctl.AddPoint=function(name,valuepointobj){if(ctl.Document&&ctl.Document.Values){return DCTemperatureDatas.AddValuePoint(ctl.Document.Values,name,valuepointobj);}
return false;};var divv=document.createElement("div");divv.id=ctl.id +"_dctimelineprimarycontent";ctl.appendChild(divv);ctl.Document=ctl.NewDocument();document.TemperatureControl=ctl;}};var DCTemperatureDatas=new Object();DCTemperatureDatas.AddValuePoint=function(datas,name,vp){if(Array.isArray(datas)==false||typeof(name)!="string"||typeof(vp)!="object"){return false;}
var addname=true;for(var i=0;i<datas.length;i++){var data=datas[i];if(data.Name!=null&&data.Name===name){addname=false;if(Array.isArray(data.Datas)==false){data.Datas=new Array();}
data.Datas.push(vp);return true;}}
if(addname===true){var data=new Object();data.Name=name;data.Datas=new Array();data.Datas.push(vp);return true;}
return false;};var DCTemperatureDocument=new Object();DCTemperatureDocument.NewDocument=function(){var doc=new Object();doc.PageIndex=0;doc.ViewMode="Page";doc.RegisterCode="";doc.Config=DCTemperatureConfig.NewConfig();doc.Values=new Array();return doc;};var DCTemperatureTitleLine=new Object();DCTemperatureTitleLine.NewTitleLine=function(){var line=new Object();line.Name="";line.Title="";line.StartDateKeyword="";line.LayoutType="Normal";line.TickStep="6";line.ValueType="Text";line.TickLineVisible="true";line.SpecifyHeight="0";line.AutoHeight="false";line.AfterOperaDaysFromZero="false";line.TextFontName=null;line.TextFontSize=null;line.LoopTextList=null;line.UpAndDownTextType="None";line.TitleAlign="Center";line.SpecifyTitleWidth="0";line.PreserveStartKeywordOrder="false";line.PageTitleTexts=null;line.BlankDateWhenNoData=false;return line;};var DCTemperatureValuePoint=new Object();Date.prototype.Format=function(fmt){var o={"M+":this.getMonth()+ 1,"d+":this.getDate(),"h+":this.getHours(),"m+":this.getMinutes(),"s+":this.getSeconds(),"q+":Math.floor((this.getMonth()+ 3)/3),"S":this.getMilliseconds()};if(/(y+)/.test(fmt))
fmt=fmt.replace(RegExp.$1,(this.getFullYear()+"").substr(4-RegExp.$1.length));for(var k in o)
if(new RegExp("("+ k +")").test(fmt))
fmt=fmt.replace(RegExp.$1,(RegExp.$1.length==1)?(o[k]):(("00"+ o[k]).substr((""+ o[k]).length)));return fmt;}
DCTemperatureValuePoint.NewValuePoint=function(){var date=new Date();var point={ColorValue:null,EndTime:null,LanternValue:null,Link:null,SpecifySymbolStyle:"Default",Text:null,TextAlign:"Center",TextColorValue:null,Time:date.Format("yyyy-MM-dd hh:mm:ss"),Title:null,Value:null,CharSymbol:"R",CharLantern:"R",TextFontSize:null,TextFontName:null,UseAdvVerticalStyle:false,UseAdvVerticalStyle2:false,Verified:false};return point;}
var DCTemperatureYAxis=new Object();DCTemperatureYAxis.NewYAxis=function(){var yaxis=new Object();yaxis.AllowOutofRange=false;yaxis.MaxValue="100";yaxis.MinValue="0";yaxis.Name="";yaxis.Style="Value";yaxis.SymbolColorValue=null;yaxis.SymbolStyle="SolidCicle";yaxis.Title="";yaxis.YSplitNum="8";yaxis.EnableLanternValue=false;yaxis.SymbolSize="20";yaxis.TitleColorValue=null;yaxis.ShadowName=null;yaxis.CharSymbol="R";yaxis.CharLantern="R";yaxis.TitleVisible=true;yaxis.ShowLegendInRule=true;yaxis.BottomTitle="";yaxis.RedLineValue=null;yaxis.VerticalLine=true;yaxis.ShadowPointVisible=true;yaxis.HollowCovertTargetName=null;yaxis.AllowInterrupt=false;yaxis.LanternValueColorForUpValue="#0000FF";yaxis.LanternValueColorForDownValue="#FF0000";yaxis.TopPadding="-10000";yaxis.BottomPadding="-10000";yaxis.MergeIntoLeft=false;yaxis.SpecifyTitleWidth="0";yaxis.AlertLineColorValue="#FF0000";yaxis.LineStyleForLanternValue="Dash";return yaxis;};