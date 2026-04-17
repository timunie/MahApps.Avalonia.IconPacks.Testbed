//! Licensed to the .NET Foundation under one or more agreements.
//! The .NET Foundation licenses this file to you under the MIT license.

var e=!1;const t=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,4,1,96,0,0,3,2,1,0,10,8,1,6,0,6,64,25,11,11])),o=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,5,1,96,0,1,123,3,2,1,0,10,15,1,13,0,65,1,253,15,65,2,253,15,253,128,2,11])),n=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,5,1,96,0,1,123,3,2,1,0,10,10,1,8,0,65,0,253,15,253,98,11])),r=Symbol.for("wasm promise_control");function i(e,t){let o=null;const n=new Promise((function(n,r){o={isDone:!1,promise:null,resolve:t=>{o.isDone||(o.isDone=!0,n(t),e&&e())},reject:e=>{o.isDone||(o.isDone=!0,r(e),t&&t())}}}));o.promise=n;const i=n;return i[r]=o,{promise:i,promise_control:o}}function s(e){return e[r]}function a(e){e&&function(e){return void 0!==e[r]}(e)||Be(!1,"Promise is not controllable")}const l="__mono_message__",c=["debug","log","trace","warn","info","error"],d="MONO_WASM: ";let u,f,m,g,p,h;function w(e){g=e}function b(e){if(Pe.diagnosticTracing){const t="function"==typeof e?e():e;console.debug(d+t)}}function y(e,...t){console.info(d+e,...t)}function v(e,...t){console.info(e,...t)}function E(e,...t){console.warn(d+e,...t)}function _(e,...t){if(t&&t.length>0&&t[0]&&"object"==typeof t[0]){if(t[0].silent)return;if(t[0].toString)return void console.error(d+e,t[0].toString())}console.error(d+e,...t)}function x(e,t,o){return function(...n){try{let r=n[0];if(void 0===r)r="undefined";else if(null===r)r="null";else if("function"==typeof r)r=r.toString();else if("string"!=typeof r)try{r=JSON.stringify(r)}catch(e){r=r.toString()}t(o?JSON.stringify({method:e,payload:r,arguments:n.slice(1)}):[e+r,...n.slice(1)])}catch(e){m.error(`proxyConsole failed: ${e}`)}}}function j(e,t,o){f=t,g=e,m={...t};const n=`${o}/console`.replace("https://","wss://").replace("http://","ws://");u=new WebSocket(n),u.addEventListener("error",A),u.addEventListener("close",S),function(){for(const e of c)f[e]=x(`console.${e}`,T,!0)}()}function R(e){let t=30;const o=()=>{u?0==u.bufferedAmount||0==t?(e&&v(e),function(){for(const e of c)f[e]=x(`console.${e}`,m.log,!1)}(),u.removeEventListener("error",A),u.removeEventListener("close",S),u.close(1e3,e),u=void 0):(t--,globalThis.setTimeout(o,100)):e&&m&&m.log(e)};o()}function T(e){u&&u.readyState===WebSocket.OPEN?u.send(e):m.log(e)}function A(e){m.error(`[${g}] proxy console websocket error: ${e}`,e)}function S(e){m.debug(`[${g}] proxy console websocket closed: ${e}`,e)}function D(){Pe.preferredIcuAsset=O(Pe.config);let e="invariant"==Pe.config.globalizationMode;if(!e)if(Pe.preferredIcuAsset)Pe.diagnosticTracing&&b("ICU data archive(s) available, disabling invariant mode");else{if("custom"===Pe.config.globalizationMode||"all"===Pe.config.globalizationMode||"sharded"===Pe.config.globalizationMode){const e="invariant globalization mode is inactive and no ICU data archives are available";throw _(`ERROR: ${e}`),new Error(e)}Pe.diagnosticTracing&&b("ICU data archive(s) not available, using invariant globalization mode"),e=!0,Pe.preferredIcuAsset=null}const t="DOTNET_SYSTEM_GLOBALIZATION_INVARIANT",o=Pe.config.environmentVariables;if(void 0===o[t]&&e&&(o[t]="1"),void 0===o.TZ)try{const e=Intl.DateTimeFormat().resolvedOptions().timeZone||null;e&&(o.TZ=e)}catch(e){y("failed to detect timezone, will fallback to UTC")}}function O(e){var t;if((null===(t=e.resources)||void 0===t?void 0:t.icu)&&"invariant"!=e.globalizationMode){const t=e.applicationCulture||(ke?globalThis.navigator&&globalThis.navigator.languages&&globalThis.navigator.languages[0]:Intl.DateTimeFormat().resolvedOptions().locale),o=e.resources.icu;let n=null;if("custom"===e.globalizationMode){if(o.length>=1)return o[0].name}else t&&"all"!==e.globalizationMode?"sharded"===e.globalizationMode&&(n=function(e){const t=e.split("-")[0];return"en"===t||["fr","fr-FR","it","it-IT","de","de-DE","es","es-ES"].includes(e)?"icudt_EFIGS.dat":["zh","ko","ja"].includes(t)?"icudt_CJK.dat":"icudt_no_CJK.dat"}(t)):n="icudt.dat";if(n)for(let e=0;e<o.length;e++){const t=o[e];if(t.virtualPath===n)return t.name}}return e.globalizationMode="invariant",null}(new Date).valueOf();const C=class{constructor(e){this.url=e}toString(){return this.url}};async function k(e,t){try{const o="function"==typeof globalThis.fetch;if(Se){const n=e.startsWith("file://");if(!n&&o)return globalThis.fetch(e,t||{credentials:"same-origin"});p||(h=Ne.require("url"),p=Ne.require("fs")),n&&(e=h.fileURLToPath(e));const r=await p.promises.readFile(e);return{ok:!0,headers:{length:0,get:()=>null},url:e,arrayBuffer:()=>r,json:()=>JSON.parse(r),text:()=>{throw new Error("NotImplementedException")}}}if(o)return globalThis.fetch(e,t||{credentials:"same-origin"});if("function"==typeof read)return{ok:!0,url:e,headers:{length:0,get:()=>null},arrayBuffer:()=>new Uint8Array(read(e,"binary")),json:()=>JSON.parse(read(e,"utf8")),text:()=>read(e,"utf8")}}catch(t){return{ok:!1,url:e,status:500,headers:{length:0,get:()=>null},statusText:"ERR28: "+t,arrayBuffer:()=>{throw t},json:()=>{throw t},text:()=>{throw t}}}throw new Error("No fetch implementation available")}function I(e){return"string"!=typeof e&&Be(!1,"url must be a string"),!M(e)&&0!==e.indexOf("./")&&0!==e.indexOf("../")&&globalThis.URL&&globalThis.document&&globalThis.document.baseURI&&(e=new URL(e,globalThis.document.baseURI).toString()),e}const U=/^[a-zA-Z][a-zA-Z\d+\-.]*?:\/\//,P=/[a-zA-Z]:[\\/]/;function M(e){return Se||Ie?e.startsWith("/")||e.startsWith("\\")||-1!==e.indexOf("///")||P.test(e):U.test(e)}let L,N=0;const $=[],z=[],W=new Map,F={"js-module-threads":!0,"js-module-runtime":!0,"js-module-dotnet":!0,"js-module-native":!0,"js-module-diagnostics":!0},B={...F,"js-module-library-initializer":!0},V={...F,dotnetwasm:!0,heap:!0,manifest:!0},q={...B,manifest:!0},H={...B,dotnetwasm:!0},J={dotnetwasm:!0,symbols:!0},Z={...B,dotnetwasm:!0,symbols:!0},Q={symbols:!0};function G(e){return!("icu"==e.behavior&&e.name!=Pe.preferredIcuAsset)}function K(e,t,o){null!=t||(t=[]),Be(1==t.length,`Expect to have one ${o} asset in resources`);const n=t[0];return n.behavior=o,X(n),e.push(n),n}function X(e){V[e.behavior]&&W.set(e.behavior,e)}function Y(e){Be(V[e],`Unknown single asset behavior ${e}`);const t=W.get(e);if(t&&!t.resolvedUrl)if(t.resolvedUrl=Pe.locateFile(t.name),F[t.behavior]){const e=ge(t);e?("string"!=typeof e&&Be(!1,"loadBootResource response for 'dotnetjs' type should be a URL string"),t.resolvedUrl=e):t.resolvedUrl=ce(t.resolvedUrl,t.behavior)}else if("dotnetwasm"!==t.behavior)throw new Error(`Unknown single asset behavior ${e}`);return t}function ee(e){const t=Y(e);return Be(t,`Single asset for ${e} not found`),t}let te=!1;async function oe(){if(!te){te=!0,Pe.diagnosticTracing&&b("mono_download_assets");try{const e=[],t=[],o=(e,t)=>{!Z[e.behavior]&&G(e)&&Pe.expected_instantiated_assets_count++,!H[e.behavior]&&G(e)&&(Pe.expected_downloaded_assets_count++,t.push(se(e)))};for(const t of $)o(t,e);for(const e of z)o(e,t);Pe.allDownloadsQueued.promise_control.resolve(),Promise.all([...e,...t]).then((()=>{Pe.allDownloadsFinished.promise_control.resolve()})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e})),await Pe.runtimeModuleLoaded.promise;const n=async e=>{const t=await e;if(t.buffer){if(!Z[t.behavior]){t.buffer&&"object"==typeof t.buffer||Be(!1,"asset buffer must be array-like or buffer-like or promise of these"),"string"!=typeof t.resolvedUrl&&Be(!1,"resolvedUrl must be string");const e=t.resolvedUrl,o=await t.buffer,n=new Uint8Array(o);pe(t),await Ue.beforeOnRuntimeInitialized.promise,Ue.instantiate_asset(t,e,n)}}else J[t.behavior]?("symbols"===t.behavior&&(await Ue.instantiate_symbols_asset(t),pe(t)),J[t.behavior]&&++Pe.actual_downloaded_assets_count):(t.isOptional||Be(!1,"Expected asset to have the downloaded buffer"),!H[t.behavior]&&G(t)&&Pe.expected_downloaded_assets_count--,!Z[t.behavior]&&G(t)&&Pe.expected_instantiated_assets_count--)},r=[],i=[];for(const t of e)r.push(n(t));for(const e of t)i.push(n(e));Promise.all(r).then((()=>{Ce||Ue.coreAssetsInMemory.promise_control.resolve()})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e})),Promise.all(i).then((async()=>{Ce||(await Ue.coreAssetsInMemory.promise,Ue.allAssetsInMemory.promise_control.resolve())})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e}))}catch(e){throw Pe.err("Error in mono_download_assets: "+e),e}}}let ne=!1;function re(){if(ne)return;ne=!0;const e=Pe.config,t=[];if(e.assets)for(const t of e.assets)"object"!=typeof t&&Be(!1,`asset must be object, it was ${typeof t} : ${t}`),"string"!=typeof t.behavior&&Be(!1,"asset behavior must be known string"),"string"!=typeof t.name&&Be(!1,"asset name must be string"),t.resolvedUrl&&"string"!=typeof t.resolvedUrl&&Be(!1,"asset resolvedUrl could be string"),t.hash&&"string"!=typeof t.hash&&Be(!1,"asset resolvedUrl could be string"),t.pendingDownload&&"object"!=typeof t.pendingDownload&&Be(!1,"asset pendingDownload could be object"),t.isCore?$.push(t):z.push(t),X(t);else if(e.resources){const o=e.resources;o.wasmNative||Be(!1,"resources.wasmNative must be defined"),o.jsModuleNative||Be(!1,"resources.jsModuleNative must be defined"),o.jsModuleRuntime||Be(!1,"resources.jsModuleRuntime must be defined"),K(z,o.wasmNative,"dotnetwasm"),K(t,o.jsModuleNative,"js-module-native"),K(t,o.jsModuleRuntime,"js-module-runtime"),o.jsModuleDiagnostics&&K(t,o.jsModuleDiagnostics,"js-module-diagnostics");const n=(e,t,o)=>{const n=e;n.behavior=t,o?(n.isCore=!0,$.push(n)):z.push(n)};if(o.coreAssembly)for(let e=0;e<o.coreAssembly.length;e++)n(o.coreAssembly[e],"assembly",!0);if(o.assembly)for(let e=0;e<o.assembly.length;e++)n(o.assembly[e],"assembly",!o.coreAssembly);if(0!=e.debugLevel&&Pe.isDebuggingSupported()){if(o.corePdb)for(let e=0;e<o.corePdb.length;e++)n(o.corePdb[e],"pdb",!0);if(o.pdb)for(let e=0;e<o.pdb.length;e++)n(o.pdb[e],"pdb",!o.corePdb)}if(e.loadAllSatelliteResources&&o.satelliteResources)for(const e in o.satelliteResources)for(let t=0;t<o.satelliteResources[e].length;t++){const r=o.satelliteResources[e][t];r.culture=e,n(r,"resource",!o.coreAssembly)}if(o.coreVfs)for(let e=0;e<o.coreVfs.length;e++)n(o.coreVfs[e],"vfs",!0);if(o.vfs)for(let e=0;e<o.vfs.length;e++)n(o.vfs[e],"vfs",!o.coreVfs);const r=O(e);if(r&&o.icu)for(let e=0;e<o.icu.length;e++){const t=o.icu[e];t.name===r&&n(t,"icu",!1)}if(o.wasmSymbols)for(let e=0;e<o.wasmSymbols.length;e++)n(o.wasmSymbols[e],"symbols",!1)}if(e.appsettings)for(let t=0;t<e.appsettings.length;t++){const o=e.appsettings[t],n=he(o);"appsettings.json"!==n&&n!==`appsettings.${e.applicationEnvironment}.json`||z.push({name:o,behavior:"vfs",cache:"no-cache",useCredentials:!0})}e.assets=[...$,...z,...t]}async function ie(e){const t=await se(e);return await t.pendingDownloadInternal.response,t.buffer}async function se(e){try{return await ae(e)}catch(t){if(!Pe.enableDownloadRetry)throw t;if(Ie||Se)throw t;if(e.pendingDownload&&e.pendingDownloadInternal==e.pendingDownload)throw t;if(e.resolvedUrl&&-1!=e.resolvedUrl.indexOf("file://"))throw t;if(t&&404==t.status)throw t;e.pendingDownloadInternal=void 0,await Pe.allDownloadsQueued.promise;try{return Pe.diagnosticTracing&&b(`Retrying download '${e.name}'`),await ae(e)}catch(t){return e.pendingDownloadInternal=void 0,await new Promise((e=>globalThis.setTimeout(e,100))),Pe.diagnosticTracing&&b(`Retrying download (2) '${e.name}' after delay`),await ae(e)}}}async function ae(e){for(;L;)await L.promise;try{++N,N==Pe.maxParallelDownloads&&(Pe.diagnosticTracing&&b("Throttling further parallel downloads"),L=i());const t=await async function(e){if(e.pendingDownload&&(e.pendingDownloadInternal=e.pendingDownload),e.pendingDownloadInternal&&e.pendingDownloadInternal.response)return e.pendingDownloadInternal.response;if(e.buffer){const t=await e.buffer;return e.resolvedUrl||(e.resolvedUrl="undefined://"+e.name),e.pendingDownloadInternal={url:e.resolvedUrl,name:e.name,response:Promise.resolve({ok:!0,arrayBuffer:()=>t,json:()=>JSON.parse(new TextDecoder("utf-8").decode(t)),text:()=>{throw new Error("NotImplementedException")},headers:{get:()=>{}}})},e.pendingDownloadInternal.response}const t=e.loadRemote&&Pe.config.remoteSources?Pe.config.remoteSources:[""];let o;for(let n of t){n=n.trim(),"./"===n&&(n="");const t=le(e,n);e.name===t?Pe.diagnosticTracing&&b(`Attempting to download '${t}'`):Pe.diagnosticTracing&&b(`Attempting to download '${t}' for ${e.name}`);try{e.resolvedUrl=t;const n=fe(e);if(e.pendingDownloadInternal=n,o=await n.response,!o||!o.ok)continue;return o}catch(e){o||(o={ok:!1,url:t,status:0,statusText:""+e});continue}}const n=e.isOptional||e.name.match(/\.pdb$/)&&Pe.config.ignorePdbLoadErrors;if(o||Be(!1,`Response undefined ${e.name}`),!n){const t=new Error(`download '${o.url}' for ${e.name} failed ${o.status} ${o.statusText}`);throw t.status=o.status,t}y(`optional download '${o.url}' for ${e.name} failed ${o.status} ${o.statusText}`)}(e);return t?(J[e.behavior]||(e.buffer=await t.arrayBuffer(),++Pe.actual_downloaded_assets_count),e):e}finally{if(--N,L&&N==Pe.maxParallelDownloads-1){Pe.diagnosticTracing&&b("Resuming more parallel downloads");const e=L;L=void 0,e.promise_control.resolve()}}}function le(e,t){let o;return null==t&&Be(!1,`sourcePrefix must be provided for ${e.name}`),e.resolvedUrl?o=e.resolvedUrl:(o=""===t?"assembly"===e.behavior||"pdb"===e.behavior?e.name:"resource"===e.behavior&&e.culture&&""!==e.culture?`${e.culture}/${e.name}`:e.name:t+e.name,o=ce(Pe.locateFile(o),e.behavior)),o&&"string"==typeof o||Be(!1,"attemptUrl need to be path or url string"),o}function ce(e,t){return Pe.modulesUniqueQuery&&q[t]&&(e+=Pe.modulesUniqueQuery),e}let de=0;const ue=new Set;function fe(e){try{e.resolvedUrl||Be(!1,"Request's resolvedUrl must be set");const t=function(e){let t=e.resolvedUrl;if(Pe.loadBootResource){const o=ge(e);if(o instanceof Promise)return o;"string"==typeof o&&(t=o)}const o={};return e.cache?o.cache=e.cache:Pe.config.disableNoCacheFetch||(o.cache="no-cache"),e.useCredentials?o.credentials="include":!Pe.config.disableIntegrityCheck&&e.hash&&(o.integrity=e.hash),Pe.fetch_like(t,o)}(e),o={name:e.name,url:e.resolvedUrl,response:t};return ue.add(e.name),o.response.then((()=>{"assembly"==e.behavior&&Pe.loadedAssemblies.push(e.name),de++,Pe.onDownloadResourceProgress&&Pe.onDownloadResourceProgress(de,ue.size)})),o}catch(t){const o={ok:!1,url:e.resolvedUrl,status:500,statusText:"ERR29: "+t,arrayBuffer:()=>{throw t},json:()=>{throw t}};return{name:e.name,url:e.resolvedUrl,response:Promise.resolve(o)}}}const me={resource:"assembly",assembly:"assembly",pdb:"pdb",icu:"globalization",vfs:"configuration",manifest:"manifest",dotnetwasm:"dotnetwasm","js-module-dotnet":"dotnetjs","js-module-native":"dotnetjs","js-module-runtime":"dotnetjs","js-module-threads":"dotnetjs"};function ge(e){var t;if(Pe.loadBootResource){const o=null!==(t=e.hash)&&void 0!==t?t:"",n=e.resolvedUrl,r=me[e.behavior];if(r){const t=Pe.loadBootResource(r,e.name,n,o,e.behavior);return"string"==typeof t?I(t):t}}}function pe(e){e.pendingDownloadInternal=null,e.pendingDownload=null,e.buffer=null,e.moduleExports=null}function he(e){let t=e.lastIndexOf("/");return t>=0&&t++,e.substring(t)}async function we(e){e&&await Promise.all((null!=e?e:[]).map((e=>async function(e){try{const t=e.name;if(!e.moduleExports){const o=ce(Pe.locateFile(t),"js-module-library-initializer");Pe.diagnosticTracing&&b(`Attempting to import '${o}' for ${e}`),e.moduleExports=await import(/*! webpackIgnore: true */o)}Pe.libraryInitializers.push({scriptName:t,exports:e.moduleExports})}catch(t){E(`Failed to import library initializer '${e}': ${t}`)}}(e))))}async function be(e,t){if(!Pe.libraryInitializers)return;const o=[];for(let n=0;n<Pe.libraryInitializers.length;n++){const r=Pe.libraryInitializers[n];r.exports[e]&&o.push(ye(r.scriptName,e,(()=>r.exports[e](...t))))}await Promise.all(o)}async function ye(e,t,o){try{await o()}catch(o){throw E(`Failed to invoke '${t}' on library initializer '${e}': ${o}`),Xe(1,o),o}}function ve(e,t){if(e===t)return e;const o={...t};return void 0!==o.assets&&o.assets!==e.assets&&(o.assets=[...e.assets||[],...o.assets||[]]),void 0!==o.resources&&(o.resources=_e(e.resources||{assembly:[],jsModuleNative:[],jsModuleRuntime:[],wasmNative:[]},o.resources)),void 0!==o.environmentVariables&&(o.environmentVariables={...e.environmentVariables||{},...o.environmentVariables||{}}),void 0!==o.runtimeOptions&&o.runtimeOptions!==e.runtimeOptions&&(o.runtimeOptions=[...e.runtimeOptions||[],...o.runtimeOptions||[]]),Object.assign(e,o)}function Ee(e,t){if(e===t)return e;const o={...t};return o.config&&(e.config||(e.config={}),o.config=ve(e.config,o.config)),Object.assign(e,o)}function _e(e,t){if(e===t)return e;const o={...t};return void 0!==o.coreAssembly&&(o.coreAssembly=[...e.coreAssembly||[],...o.coreAssembly||[]]),void 0!==o.assembly&&(o.assembly=[...e.assembly||[],...o.assembly||[]]),void 0!==o.lazyAssembly&&(o.lazyAssembly=[...e.lazyAssembly||[],...o.lazyAssembly||[]]),void 0!==o.corePdb&&(o.corePdb=[...e.corePdb||[],...o.corePdb||[]]),void 0!==o.pdb&&(o.pdb=[...e.pdb||[],...o.pdb||[]]),void 0!==o.jsModuleWorker&&(o.jsModuleWorker=[...e.jsModuleWorker||[],...o.jsModuleWorker||[]]),void 0!==o.jsModuleNative&&(o.jsModuleNative=[...e.jsModuleNative||[],...o.jsModuleNative||[]]),void 0!==o.jsModuleDiagnostics&&(o.jsModuleDiagnostics=[...e.jsModuleDiagnostics||[],...o.jsModuleDiagnostics||[]]),void 0!==o.jsModuleRuntime&&(o.jsModuleRuntime=[...e.jsModuleRuntime||[],...o.jsModuleRuntime||[]]),void 0!==o.wasmSymbols&&(o.wasmSymbols=[...e.wasmSymbols||[],...o.wasmSymbols||[]]),void 0!==o.wasmNative&&(o.wasmNative=[...e.wasmNative||[],...o.wasmNative||[]]),void 0!==o.icu&&(o.icu=[...e.icu||[],...o.icu||[]]),void 0!==o.satelliteResources&&(o.satelliteResources=function(e,t){if(e===t)return e;for(const o in t)e[o]=[...e[o]||[],...t[o]||[]];return e}(e.satelliteResources||{},o.satelliteResources||{})),void 0!==o.modulesAfterConfigLoaded&&(o.modulesAfterConfigLoaded=[...e.modulesAfterConfigLoaded||[],...o.modulesAfterConfigLoaded||[]]),void 0!==o.modulesAfterRuntimeReady&&(o.modulesAfterRuntimeReady=[...e.modulesAfterRuntimeReady||[],...o.modulesAfterRuntimeReady||[]]),void 0!==o.extensions&&(o.extensions={...e.extensions||{},...o.extensions||{}}),void 0!==o.vfs&&(o.vfs=[...e.vfs||[],...o.vfs||[]]),Object.assign(e,o)}function xe(){const e=Pe.config;if(e.environmentVariables=e.environmentVariables||{},e.runtimeOptions=e.runtimeOptions||[],e.resources=e.resources||{assembly:[],jsModuleNative:[],jsModuleWorker:[],jsModuleRuntime:[],wasmNative:[],vfs:[],satelliteResources:{}},e.assets){Pe.diagnosticTracing&&b("config.assets is deprecated, use config.resources instead");for(const t of e.assets){const o={};switch(t.behavior){case"assembly":o.assembly=[t];break;case"pdb":o.pdb=[t];break;case"resource":o.satelliteResources={},o.satelliteResources[t.culture]=[t];break;case"icu":o.icu=[t];break;case"symbols":o.wasmSymbols=[t];break;case"vfs":o.vfs=[t];break;case"dotnetwasm":o.wasmNative=[t];break;case"js-module-threads":o.jsModuleWorker=[t];break;case"js-module-runtime":o.jsModuleRuntime=[t];break;case"js-module-native":o.jsModuleNative=[t];break;case"js-module-diagnostics":o.jsModuleDiagnostics=[t];break;case"js-module-dotnet":break;default:throw new Error(`Unexpected behavior ${t.behavior} of asset ${t.name}`)}_e(e.resources,o)}}e.debugLevel,e.applicationEnvironment||(e.applicationEnvironment="Production"),e.applicationCulture&&(e.environmentVariables.LANG=`${e.applicationCulture}.UTF-8`),Ue.diagnosticTracing=Pe.diagnosticTracing=!!e.diagnosticTracing,Ue.waitForDebugger=e.waitForDebugger,Pe.maxParallelDownloads=e.maxParallelDownloads||Pe.maxParallelDownloads,Pe.enableDownloadRetry=void 0!==e.enableDownloadRetry?e.enableDownloadRetry:Pe.enableDownloadRetry}let je=!1;async function Re(e){var t;if(je)return void await Pe.afterConfigLoaded.promise;let o;try{if(e.configSrc||Pe.config&&0!==Object.keys(Pe.config).length&&(Pe.config.assets||Pe.config.resources)||(e.configSrc="dotnet.boot.js"),o=e.configSrc,je=!0,o&&(Pe.diagnosticTracing&&b("mono_wasm_load_config"),await async function(e){const t=e.configSrc,o=Pe.locateFile(t);let n=null;void 0!==Pe.loadBootResource&&(n=Pe.loadBootResource("manifest",t,o,"","manifest"));let r,i=null;if(n)if("string"==typeof n)n.includes(".json")?(i=await s(I(n)),r=await Ae(i)):r=(await import(I(n))).config;else{const e=await n;"function"==typeof e.json?(i=e,r=await Ae(i)):r=e.config}else o.includes(".json")?(i=await s(ce(o,"manifest")),r=await Ae(i)):r=(await import(ce(o,"manifest"))).config;function s(e){return Pe.fetch_like(e,{method:"GET",credentials:"include",cache:"no-cache"})}Pe.config.applicationEnvironment&&(r.applicationEnvironment=Pe.config.applicationEnvironment),ve(Pe.config,r)}(e)),xe(),await we(null===(t=Pe.config.resources)||void 0===t?void 0:t.modulesAfterConfigLoaded),await be("onRuntimeConfigLoaded",[Pe.config]),e.onConfigLoaded)try{await e.onConfigLoaded(Pe.config,Le),xe()}catch(e){throw _("onConfigLoaded() failed",e),e}xe(),Pe.afterConfigLoaded.promise_control.resolve(Pe.config)}catch(t){const n=`Failed to load config file ${o} ${t} ${null==t?void 0:t.stack}`;throw Pe.config=e.config=Object.assign(Pe.config,{message:n,error:t,isError:!0}),Xe(1,new Error(n)),t}}function Te(){return!!globalThis.navigator&&(Pe.isChromium||Pe.isFirefox)}async function Ae(e){const t=Pe.config,o=await e.json();t.applicationEnvironment||o.applicationEnvironment||(o.applicationEnvironment=e.headers.get("Blazor-Environment")||e.headers.get("DotNet-Environment")||void 0),o.environmentVariables||(o.environmentVariables={});const n=e.headers.get("DOTNET-MODIFIABLE-ASSEMBLIES");n&&(o.environmentVariables.DOTNET_MODIFIABLE_ASSEMBLIES=n);const r=e.headers.get("ASPNETCORE-BROWSER-TOOLS");return r&&(o.environmentVariables.__ASPNETCORE_BROWSER_TOOLS=r),o}"function"!=typeof importScripts||globalThis.onmessage||(globalThis.dotnetSidecar=!0);const Se="object"==typeof process&&"object"==typeof process.versions&&"string"==typeof process.versions.node,De="function"==typeof importScripts,Oe=De&&"undefined"!=typeof dotnetSidecar,Ce=De&&!Oe,ke="object"==typeof window||De&&!Se,Ie=!ke&&!Se;let Ue={},Pe={},Me={},Le={},Ne={},$e=!1;const ze={},We={config:ze},Fe={mono:{},binding:{},internal:Ne,module:We,loaderHelpers:Pe,runtimeHelpers:Ue,diagnosticHelpers:Me,api:Le};function Be(e,t){if(e)return;const o="Assert failed: "+("function"==typeof t?t():t),n=new Error(o);_(o,n),Ue.nativeAbort(n)}function Ve(){return void 0!==Pe.exitCode}function qe(){return Ue.runtimeReady&&!Ve()}function He(){Ve()&&Be(!1,`.NET runtime already exited with ${Pe.exitCode} ${Pe.exitReason}. You can use runtime.runMain() which doesn't exit the runtime.`),Ue.runtimeReady||Be(!1,".NET runtime didn't start yet. Please call dotnet.create() first.")}function Je(){ke&&(globalThis.addEventListener("unhandledrejection",et),globalThis.addEventListener("error",tt))}let Ze,Qe;function Ge(e){Qe&&Qe(e),Xe(e,Pe.exitReason)}function Ke(e){Ze&&Ze(e||Pe.exitReason),Xe(1,e||Pe.exitReason)}function Xe(t,o){var n,r;const i=o&&"object"==typeof o;t=i&&"number"==typeof o.status?o.status:void 0===t?-1:t;const s=i&&"string"==typeof o.message?o.message:""+o;(o=i?o:Ue.ExitStatus?function(e,t){const o=new Ue.ExitStatus(e);return o.message=t,o.toString=()=>t,o}(t,s):new Error("Exit with code "+t+" "+s)).status=t,o.message||(o.message=s);const a=""+(o.stack||(new Error).stack);try{Object.defineProperty(o,"stack",{get:()=>a})}catch(e){}const l=!!o.silent;if(o.silent=!0,Ve())Pe.diagnosticTracing&&b("mono_exit called after exit");else{try{We.onAbort==Ke&&(We.onAbort=Ze),We.onExit==Ge&&(We.onExit=Qe),ke&&(globalThis.removeEventListener("unhandledrejection",et),globalThis.removeEventListener("error",tt)),Ue.runtimeReady?(Ue.jiterpreter_dump_stats&&Ue.jiterpreter_dump_stats(!1),0===t&&(null===(n=Pe.config)||void 0===n?void 0:n.interopCleanupOnExit)&&Ue.forceDisposeProxies(!0,!0),e&&0!==t&&(null===(r=Pe.config)||void 0===r||r.dumpThreadsOnNonZeroExit)):(Pe.diagnosticTracing&&b(`abort_startup, reason: ${o}`),function(e){Pe.allDownloadsQueued.promise_control.reject(e),Pe.allDownloadsFinished.promise_control.reject(e),Pe.afterConfigLoaded.promise_control.reject(e),Pe.wasmCompilePromise.promise_control.reject(e),Pe.runtimeModuleLoaded.promise_control.reject(e),Ue.dotnetReady&&(Ue.dotnetReady.promise_control.reject(e),Ue.afterInstantiateWasm.promise_control.reject(e),Ue.beforePreInit.promise_control.reject(e),Ue.afterPreInit.promise_control.reject(e),Ue.afterPreRun.promise_control.reject(e),Ue.beforeOnRuntimeInitialized.promise_control.reject(e),Ue.afterOnRuntimeInitialized.promise_control.reject(e),Ue.afterPostRun.promise_control.reject(e))}(o))}catch(e){E("mono_exit A failed",e)}try{l||(function(e,t){if(0!==e&&t){const e=Ue.ExitStatus&&t instanceof Ue.ExitStatus?b:_;"string"==typeof t?e(t):(void 0===t.stack&&(t.stack=(new Error).stack+""),t.message?e(Ue.stringify_as_error_with_stack?Ue.stringify_as_error_with_stack(t.message+"\n"+t.stack):t.message+"\n"+t.stack):e(JSON.stringify(t)))}!Ce&&Pe.config&&(Pe.config.logExitCode?Pe.config.forwardConsoleLogsToWS?R("WASM EXIT "+e):v("WASM EXIT "+e):Pe.config.forwardConsoleLogsToWS&&R())}(t,o),function(e){if(ke&&!Ce&&Pe.config&&Pe.config.appendElementOnExit&&document){const t=document.createElement("label");t.id="tests_done",0!==e&&(t.style.background="red"),t.innerHTML=""+e,document.body.appendChild(t)}}(t))}catch(e){E("mono_exit B failed",e)}Pe.exitCode=t,Pe.exitReason||(Pe.exitReason=o),!Ce&&Ue.runtimeReady&&We.runtimeKeepalivePop()}if(Pe.config&&Pe.config.asyncFlushOnExit&&0===t)throw(async()=>{try{await async function(){try{const e=await import(/*! webpackIgnore: true */"process"),t=e=>new Promise(((t,o)=>{e.on("error",o),e.end("","utf8",t)})),o=t(e.stderr),n=t(e.stdout);let r;const i=new Promise((e=>{r=setTimeout((()=>e("timeout")),1e3)}));await Promise.race([Promise.all([n,o]),i]),clearTimeout(r)}catch(e){_(`flushing std* streams failed: ${e}`)}}()}finally{Ye(t,o)}})(),o;Ye(t,o)}function Ye(e,t){if(Ue.runtimeReady&&Ue.nativeExit)try{Ue.nativeExit(e)}catch(e){!Ue.ExitStatus||e instanceof Ue.ExitStatus||E("set_exit_code_and_quit_now failed: "+e.toString())}if(0!==e||!ke)throw Se&&Ne.process?Ne.process.exit(e):Ue.quit&&Ue.quit(e,t),t}function et(e){ot(e,e.reason,"rejection")}function tt(e){ot(e,e.error,"error")}function ot(e,t,o){e.preventDefault();try{t||(t=new Error("Unhandled "+o)),void 0===t.stack&&(t.stack=(new Error).stack),t.stack=t.stack+"",t.silent||(_("Unhandled error:",t),Xe(1,t))}catch(e){}}!function(e){if($e)throw new Error("Loader module already loaded");$e=!0,Ue=e.runtimeHelpers,Pe=e.loaderHelpers,Me=e.diagnosticHelpers,Le=e.api,Ne=e.internal,Object.assign(Le,{INTERNAL:Ne,invokeLibraryInitializers:be}),Object.assign(e.module,{config:ve(ze,{environmentVariables:{}})});const r={mono_wasm_bindings_is_ready:!1,config:e.module.config,diagnosticTracing:!1,nativeAbort:e=>{throw e||new Error("abort")},nativeExit:e=>{throw new Error("exit:"+e)}},l={gitHash:"47fb725acf5d7094af51aebbb5b7e5c44a3b2a77",config:e.module.config,diagnosticTracing:!1,maxParallelDownloads:16,enableDownloadRetry:!0,_loaded_files:[],loadedFiles:[],loadedAssemblies:[],libraryInitializers:[],workerNextNumber:1,actual_downloaded_assets_count:0,actual_instantiated_assets_count:0,expected_downloaded_assets_count:0,expected_instantiated_assets_count:0,afterConfigLoaded:i(),allDownloadsQueued:i(),allDownloadsFinished:i(),wasmCompilePromise:i(),runtimeModuleLoaded:i(),loadingWorkers:i(),is_exited:Ve,is_runtime_running:qe,assert_runtime_running:He,mono_exit:Xe,createPromiseController:i,getPromiseController:s,assertIsControllablePromise:a,mono_download_assets:oe,resolve_single_asset_path:ee,setup_proxy_console:j,set_thread_prefix:w,installUnhandledErrorHandler:Je,retrieve_asset_download:ie,invokeLibraryInitializers:be,isDebuggingSupported:Te,exceptions:t,simd:n,relaxedSimd:o};Object.assign(Ue,r),Object.assign(Pe,l)}(Fe);let nt,rt,it,st=!1,at=!1;async function lt(e){if(!at){if(at=!0,ke&&Pe.config.forwardConsoleLogsToWS&&void 0!==globalThis.WebSocket&&j("main",globalThis.console,globalThis.location.origin),We||Be(!1,"Null moduleConfig"),Pe.config||Be(!1,"Null moduleConfig.config"),"function"==typeof e){const t=e(Fe.api);if(t.ready)throw new Error("Module.ready couldn't be redefined.");Object.assign(We,t),Ee(We,t)}else{if("object"!=typeof e)throw new Error("Can't use moduleFactory callback of createDotnetRuntime function.");Ee(We,e)}await async function(e){if(Se){const e=await import(/*! webpackIgnore: true */"process"),t=14;if(e.versions.node.split(".")[0]<t)throw new Error(`NodeJS at '${e.execPath}' has too low version '${e.versions.node}', please use at least ${t}. See also https://aka.ms/dotnet-wasm-features`)}const t=/*! webpackIgnore: true */import.meta.url,o=t.indexOf("?");var n;if(o>0&&(Pe.modulesUniqueQuery=t.substring(o)),Pe.scriptUrl=t.replace(/\\/g,"/").replace(/[?#].*/,""),Pe.scriptDirectory=(n=Pe.scriptUrl).slice(0,n.lastIndexOf("/"))+"/",Pe.locateFile=e=>"URL"in globalThis&&globalThis.URL!==C?new URL(e,Pe.scriptDirectory).toString():M(e)?e:Pe.scriptDirectory+e,Pe.fetch_like=k,Pe.out=console.log,Pe.err=console.error,Pe.onDownloadResourceProgress=e.onDownloadResourceProgress,ke&&globalThis.navigator){const e=globalThis.navigator,t=e.userAgentData&&e.userAgentData.brands;t&&t.length>0?Pe.isChromium=t.some((e=>"Google Chrome"===e.brand||"Microsoft Edge"===e.brand||"Chromium"===e.brand)):e.userAgent&&(Pe.isChromium=e.userAgent.includes("Chrome"),Pe.isFirefox=e.userAgent.includes("Firefox"))}Ne.require=Se?await import(/*! webpackIgnore: true */"module").then((e=>e.createRequire(/*! webpackIgnore: true */import.meta.url))):Promise.resolve((()=>{throw new Error("require not supported")})),void 0===globalThis.URL&&(globalThis.URL=C)}(We)}}async function ct(e){return await lt(e),Ze=We.onAbort,Qe=We.onExit,We.onAbort=Ke,We.onExit=Ge,We.ENVIRONMENT_IS_PTHREAD?async function(){(function(){const e=new MessageChannel,t=e.port1,o=e.port2;t.addEventListener("message",(e=>{var n,r;n=JSON.parse(e.data.config),r=JSON.parse(e.data.monoThreadInfo),st?Pe.diagnosticTracing&&b("mono config already received"):(ve(Pe.config,n),Ue.monoThreadInfo=r,xe(),Pe.diagnosticTracing&&b("mono config received"),st=!0,Pe.afterConfigLoaded.promise_control.resolve(Pe.config),ke&&n.forwardConsoleLogsToWS&&void 0!==globalThis.WebSocket&&Pe.setup_proxy_console("worker-idle",console,globalThis.location.origin)),t.close(),o.close()}),{once:!0}),t.start(),self.postMessage({[l]:{monoCmd:"preload",port:o}},[o])})(),await Pe.afterConfigLoaded.promise,function(){const e=Pe.config;e.assets||Be(!1,"config.assets must be defined");for(const t of e.assets)X(t),Q[t.behavior]&&z.push(t)}(),setTimeout((async()=>{try{await oe()}catch(e){Xe(1,e)}}),0);const e=dt(),t=await Promise.all(e);return await ut(t),We}():async function(){var e;await Re(We),re();const t=dt();(async function(){try{const e=ee("dotnetwasm");await se(e),e&&e.pendingDownloadInternal&&e.pendingDownloadInternal.response||Be(!1,"Can't load dotnet.native.wasm");const t=await e.pendingDownloadInternal.response,o=t.headers&&t.headers.get?t.headers.get("Content-Type"):void 0;let n;if("function"==typeof WebAssembly.compileStreaming&&"application/wasm"===o)n=await WebAssembly.compileStreaming(t);else{ke&&"application/wasm"!==o&&E('WebAssembly resource does not have the expected content type "application/wasm", so falling back to slower ArrayBuffer instantiation.');const e=await t.arrayBuffer();Pe.diagnosticTracing&&b("instantiate_wasm_module buffered"),n=Ie?await Promise.resolve(new WebAssembly.Module(e)):await WebAssembly.compile(e)}e.pendingDownloadInternal=null,e.pendingDownload=null,e.buffer=null,e.moduleExports=null,Pe.wasmCompilePromise.promise_control.resolve(n)}catch(e){Pe.wasmCompilePromise.promise_control.reject(e)}})(),setTimeout((async()=>{try{D(),await oe()}catch(e){Xe(1,e)}}),0);const o=await Promise.all(t);return await ut(o),await Ue.dotnetReady.promise,await we(null===(e=Pe.config.resources)||void 0===e?void 0:e.modulesAfterRuntimeReady),await be("onRuntimeReady",[Fe.api]),Le}()}function dt(){const e=ee("js-module-runtime"),t=ee("js-module-native");if(nt&&rt)return[nt,rt,it];"object"==typeof e.moduleExports?nt=e.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${e.resolvedUrl}' for ${e.name}`),nt=import(/*! webpackIgnore: true */e.resolvedUrl)),"object"==typeof t.moduleExports?rt=t.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${t.resolvedUrl}' for ${t.name}`),rt=import(/*! webpackIgnore: true */t.resolvedUrl));const o=Y("js-module-diagnostics");return o&&("object"==typeof o.moduleExports?it=o.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${o.resolvedUrl}' for ${o.name}`),it=import(/*! webpackIgnore: true */o.resolvedUrl))),[nt,rt,it]}async function ut(e){const{initializeExports:t,initializeReplacements:o,configureRuntimeStartup:n,configureEmscriptenStartup:r,configureWorkerStartup:i,setRuntimeGlobals:s,passEmscriptenInternals:a}=e[0],{default:l}=e[1],c=e[2];s(Fe),t(Fe),c&&c.setRuntimeGlobals(Fe),await n(We),Pe.runtimeModuleLoaded.promise_control.resolve(),l((e=>(Object.assign(We,{ready:e.ready,__dotnet_runtime:{initializeReplacements:o,configureEmscriptenStartup:r,configureWorkerStartup:i,passEmscriptenInternals:a}}),We))).catch((e=>{if(e.message&&e.message.toLowerCase().includes("out of memory"))throw new Error(".NET runtime has failed to start, because too much memory was requested. Please decrease the memory by adjusting EmccMaximumHeapSize. See also https://aka.ms/dotnet-wasm-features");throw e}))}const ft=new class{withModuleConfig(e){try{return Ee(We,e),this}catch(e){throw Xe(1,e),e}}withOnConfigLoaded(e){try{return Ee(We,{onConfigLoaded:e}),this}catch(e){throw Xe(1,e),e}}withConsoleForwarding(){try{return ve(ze,{forwardConsoleLogsToWS:!0}),this}catch(e){throw Xe(1,e),e}}withExitOnUnhandledError(){try{return ve(ze,{exitOnUnhandledError:!0}),Je(),this}catch(e){throw Xe(1,e),e}}withAsyncFlushOnExit(){try{return ve(ze,{asyncFlushOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withExitCodeLogging(){try{return ve(ze,{logExitCode:!0}),this}catch(e){throw Xe(1,e),e}}withElementOnExit(){try{return ve(ze,{appendElementOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withInteropCleanupOnExit(){try{return ve(ze,{interopCleanupOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withDumpThreadsOnNonZeroExit(){try{return ve(ze,{dumpThreadsOnNonZeroExit:!0}),this}catch(e){throw Xe(1,e),e}}withWaitingForDebugger(e){try{return ve(ze,{waitForDebugger:e}),this}catch(e){throw Xe(1,e),e}}withInterpreterPgo(e,t){try{return ve(ze,{interpreterPgo:e,interpreterPgoSaveDelay:t}),ze.runtimeOptions?ze.runtimeOptions.push("--interp-pgo-recording"):ze.runtimeOptions=["--interp-pgo-recording"],this}catch(e){throw Xe(1,e),e}}withConfig(e){try{return ve(ze,e),this}catch(e){throw Xe(1,e),e}}withConfigSrc(e){try{return e&&"string"==typeof e||Be(!1,"must be file path or URL"),Ee(We,{configSrc:e}),this}catch(e){throw Xe(1,e),e}}withVirtualWorkingDirectory(e){try{return e&&"string"==typeof e||Be(!1,"must be directory path"),ve(ze,{virtualWorkingDirectory:e}),this}catch(e){throw Xe(1,e),e}}withEnvironmentVariable(e,t){try{const o={};return o[e]=t,ve(ze,{environmentVariables:o}),this}catch(e){throw Xe(1,e),e}}withEnvironmentVariables(e){try{return e&&"object"==typeof e||Be(!1,"must be dictionary object"),ve(ze,{environmentVariables:e}),this}catch(e){throw Xe(1,e),e}}withDiagnosticTracing(e){try{return"boolean"!=typeof e&&Be(!1,"must be boolean"),ve(ze,{diagnosticTracing:e}),this}catch(e){throw Xe(1,e),e}}withDebugging(e){try{return null!=e&&"number"==typeof e||Be(!1,"must be number"),ve(ze,{debugLevel:e}),this}catch(e){throw Xe(1,e),e}}withApplicationArguments(...e){try{return e&&Array.isArray(e)||Be(!1,"must be array of strings"),ve(ze,{applicationArguments:e}),this}catch(e){throw Xe(1,e),e}}withRuntimeOptions(e){try{return e&&Array.isArray(e)||Be(!1,"must be array of strings"),ze.runtimeOptions?ze.runtimeOptions.push(...e):ze.runtimeOptions=e,this}catch(e){throw Xe(1,e),e}}withMainAssembly(e){try{return ve(ze,{mainAssemblyName:e}),this}catch(e){throw Xe(1,e),e}}withApplicationArgumentsFromQuery(){try{if(!globalThis.window)throw new Error("Missing window to the query parameters from");if(void 0===globalThis.URLSearchParams)throw new Error("URLSearchParams is supported");const e=new URLSearchParams(globalThis.window.location.search).getAll("arg");return this.withApplicationArguments(...e)}catch(e){throw Xe(1,e),e}}withApplicationEnvironment(e){try{return ve(ze,{applicationEnvironment:e}),this}catch(e){throw Xe(1,e),e}}withApplicationCulture(e){try{return ve(ze,{applicationCulture:e}),this}catch(e){throw Xe(1,e),e}}withResourceLoader(e){try{return Pe.loadBootResource=e,this}catch(e){throw Xe(1,e),e}}async download(){try{await async function(){lt(We),await Re(We),re(),D(),oe(),await Pe.allDownloadsFinished.promise}()}catch(e){throw Xe(1,e),e}}async create(){try{return this.instance||(this.instance=await async function(){return await ct(We),Fe.api}()),this.instance}catch(e){throw Xe(1,e),e}}async run(){try{return We.config||Be(!1,"Null moduleConfig.config"),this.instance||await this.create(),this.instance.runMainAndExit()}catch(e){throw Xe(1,e),e}}},mt=Xe,gt=ct;Ie||"function"==typeof globalThis.URL||Be(!1,"This browser/engine doesn't support URL API. Please use a modern version. See also https://aka.ms/dotnet-wasm-features"),"function"!=typeof globalThis.BigInt64Array&&Be(!1,"This browser/engine doesn't support BigInt64Array API. Please use a modern version. See also https://aka.ms/dotnet-wasm-features"),ft.withConfig(/*json-start*/{
  "mainAssemblyName": "MahApps.IconPacksBrowser.Avalonia.Browser",
  "resources": {
    "hash": "sha256-0vkJRjJNHULj9w0z1r5WAL4mpBmPcMiW7BiSnlxb+lw=",
    "jsModuleNative": [
      {
        "name": "dotnet.native.9odbhdccjg.js"
      }
    ],
    "jsModuleRuntime": [
      {
        "name": "dotnet.runtime.f4b1oiwlzh.js"
      }
    ],
    "wasmNative": [
      {
        "name": "dotnet.native.31j29ttbyl.wasm",
        "integrity": "sha256-KWbrVi19q2S54cjkIfESVfyLQiFGGj4phIyxpX3M5/M=",
        "cache": "force-cache"
      }
    ],
    "icu": [
      {
        "virtualPath": "icudt_CJK.dat",
        "name": "icudt_CJK.tjcz0u77k5.dat",
        "integrity": "sha256-SZLtQnRc0JkwqHab0VUVP7T3uBPSeYzxzDnpxPpUnHk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "icudt_EFIGS.dat",
        "name": "icudt_EFIGS.tptq2av103.dat",
        "integrity": "sha256-8fItetYY8kQ0ww6oxwTLiT3oXlBwHKumbeP2pRF4yTc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "icudt_no_CJK.dat",
        "name": "icudt_no_CJK.lfu7j35m59.dat",
        "integrity": "sha256-L7sV7NEYP37/Qr2FPCePo5cJqRgTXRwGHuwF5Q+0Nfs=",
        "cache": "force-cache"
      }
    ],
    "coreAssembly": [
      {
        "virtualPath": "AsyncAwaitBestPractices.wasm",
        "name": "AsyncAwaitBestPractices.eokz6l24b8.wasm",
        "integrity": "sha256-bq3k1GOXh4dn+3u/APETREr621gDqsRTsT5d/TZ/q6Y=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Base.wasm",
        "name": "Avalonia.Base.y9zqsnrrc4.wasm",
        "integrity": "sha256-Fv24ingqHe0FDfs9PSLaZBsbLFvZeD2hmiz0o/A5FHU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Browser.wasm",
        "name": "Avalonia.Browser.3gwoo7eju0.wasm",
        "integrity": "sha256-Q+4imJTE5aQaMcN9X1UucagFrcVAPWHR6Kg09Dm1U4c=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Controls.wasm",
        "name": "Avalonia.Controls.ohanwueowl.wasm",
        "integrity": "sha256-JDN/ZiM7yUKSl+MfUboT0+WyWfN9z24nvX4JgjMJTLI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Controls.ColorPicker.wasm",
        "name": "Avalonia.Controls.ColorPicker.088l6iel94.wasm",
        "integrity": "sha256-IHVAa0yBDcfCqeAw0TdIDILxCyg09mAcVKBsOmp6RT8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Dialogs.wasm",
        "name": "Avalonia.Dialogs.ah26kw5430.wasm",
        "integrity": "sha256-DrawidnYecgcwcc15aB5Vea7qlY896fkCABRJigbH6E=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Fonts.Inter.wasm",
        "name": "Avalonia.Fonts.Inter.8nbmom02rd.wasm",
        "integrity": "sha256-jGsDf2B4TvPGzQ7zCWC0MBTY6DrYeqA5pyoHRa6vXsQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.HarfBuzz.wasm",
        "name": "Avalonia.HarfBuzz.qujl5quqom.wasm",
        "integrity": "sha256-PnvVonYIRp+nDeYiWTPeswunltcfWPZA2FQ18am7n70=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Markup.Xaml.wasm",
        "name": "Avalonia.Markup.Xaml.a96fb8y06s.wasm",
        "integrity": "sha256-bviPU7rsvh0tpB4mOTVz0+ppZgd9eHgZG5Yempt2znk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Markup.wasm",
        "name": "Avalonia.Markup.acyrw58gt5.wasm",
        "integrity": "sha256-ZkUlmgEDHGLVYoPc13+cIjESIYjOBU9BEwb3d9e9lAA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Metal.wasm",
        "name": "Avalonia.Metal.a7usk1ezlj.wasm",
        "integrity": "sha256-GqB5qHIpW6O4WhdiFIbobd6zYjdVbG3xtb+i50xa4zU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.OpenGL.wasm",
        "name": "Avalonia.OpenGL.0wu2hvqwai.wasm",
        "integrity": "sha256-4DXhNTpzsnMLVXbmNmHKeBinPWD0d380ZAUcWmIiV2A=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Skia.wasm",
        "name": "Avalonia.Skia.0f7i6qhnji.wasm",
        "integrity": "sha256-DDRO1B9NBAFtaqYgnaE5zP/NRGiUpwZIwRkHBEkZebo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Themes.Simple.wasm",
        "name": "Avalonia.Themes.Simple.3o09iusd7h.wasm",
        "integrity": "sha256-XQoTp9asO1DVJeAuXgpk1MNpzo62vB19vyMrccRoqYY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Vulkan.wasm",
        "name": "Avalonia.Vulkan.1z6kt7d5a4.wasm",
        "integrity": "sha256-s9YqkmNpdTvpCBi8NgrSf5TxnpmsxiIRBCbCzlTC3H0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "CommunityToolkit.Mvvm.wasm",
        "name": "CommunityToolkit.Mvvm.xs7zyvx8jw.wasm",
        "integrity": "sha256-v59/EkrQXB/uwdW93OHK4p8lXX35+oT/+5WxDa36yVA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DynamicData.wasm",
        "name": "DynamicData.ddaotcyj50.wasm",
        "integrity": "sha256-f+iTsM1WvDW4Ga1VmADhuCcYbAKnuUGLV0aQbaeTTIo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "HarfBuzzSharp.wasm",
        "name": "HarfBuzzSharp.r8i4zr2803.wasm",
        "integrity": "sha256-pVuwCrZW2fqu6fnQMMMDA3aYsEKVzM0h2IUXBWn35vI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.BootstrapIcons.wasm",
        "name": "IconPacks.Avalonia.BootstrapIcons.ifhxujc5b8.wasm",
        "integrity": "sha256-fuDZ1YwVZoFkskXCDbeqaegsk9DHNrkbEQ3JB99Ozu8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.BoxIcons.wasm",
        "name": "IconPacks.Avalonia.BoxIcons.xppwfy1xdw.wasm",
        "integrity": "sha256-yy5sww24gnO318jLGYHj8xTWyNxbUfqSREpYjS7dYSc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.BoxIcons2.wasm",
        "name": "IconPacks.Avalonia.BoxIcons2.f411x5bjtx.wasm",
        "integrity": "sha256-Ed92I7+OKkzzEiLU0Iyx9fUUzM1J1Tq5FwZIdNK7sXs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.CircumIcons.wasm",
        "name": "IconPacks.Avalonia.CircumIcons.02wadlef1s.wasm",
        "integrity": "sha256-OP7RJn/q0wiIOpdA+eH4ZIAGu0B4b/qTprQnd/SCnxk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.Codicons.wasm",
        "name": "IconPacks.Avalonia.Codicons.q4rjkfc2ts.wasm",
        "integrity": "sha256-GoB8Dn5m1KNSkF3612ak9Tq2n8jxd6Br38xBCsh0pkw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.Coolicons.wasm",
        "name": "IconPacks.Avalonia.Coolicons.s3g6u2587f.wasm",
        "integrity": "sha256-CA2bUy7UhX0/FNjRgGqAIQe3uLTW0MxsgkAVFOKKEg4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.Core.wasm",
        "name": "IconPacks.Avalonia.Core.mg9e7eg863.wasm",
        "integrity": "sha256-mnmI9n49LpaFE0z6L6a41JnnBStsZyUIpBvj2fcK4f8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.Entypo.wasm",
        "name": "IconPacks.Avalonia.Entypo.06sny1u5ol.wasm",
        "integrity": "sha256-WBQqgplTwY9O75y2vJ3gwVTqWJHlz9ekePpA/QSWrZE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.EvaIcons.wasm",
        "name": "IconPacks.Avalonia.EvaIcons.s8lkh2jnlg.wasm",
        "integrity": "sha256-jJFy9bFkNjNnp3KiCWGsEhG8PoJ8+WKtk7CrLY29g/E=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.FeatherIcons.wasm",
        "name": "IconPacks.Avalonia.FeatherIcons.8mayb0olkb.wasm",
        "integrity": "sha256-wOoooJy++kyTB07OEsY3JmNbsLzpNg8UXHmdKGCb7O0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.FileIcons.wasm",
        "name": "IconPacks.Avalonia.FileIcons.rfwx7d5evd.wasm",
        "integrity": "sha256-d627PUW60q4zflaMXl1ULgHRKTWSqnDajORwCCKDQv0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.FontAwesome.wasm",
        "name": "IconPacks.Avalonia.FontAwesome.8oxxvpl1z7.wasm",
        "integrity": "sha256-uDv2Py7athNZP6mkjab7uiF8S2TakCZa3eP6/9Nf5Fk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.FontAwesome5.wasm",
        "name": "IconPacks.Avalonia.FontAwesome5.70r7h164k9.wasm",
        "integrity": "sha256-icYg+oduZRWuV/vQa41UlhXuvG/V0+GmkK53ataCZHU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.FontAwesome6.wasm",
        "name": "IconPacks.Avalonia.FontAwesome6.03j7g32lod.wasm",
        "integrity": "sha256-XLh0Yncrvd1L1OTGFLjV7KAY4M1u+LrUySOgCnqeMSQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.Fontaudio.wasm",
        "name": "IconPacks.Avalonia.Fontaudio.6jidgpra8z.wasm",
        "integrity": "sha256-EuO2ehbGBcZd4bRu8b9qHLfxonqyrPP666iQkqWDf6k=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.Fontisto.wasm",
        "name": "IconPacks.Avalonia.Fontisto.iizrudlmuj.wasm",
        "integrity": "sha256-dhEBTFnq0heUmqOhMHibhfmGNIhXgrDAsm+4Coivtlo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.ForkAwesome.wasm",
        "name": "IconPacks.Avalonia.ForkAwesome.i0av5uaufj.wasm",
        "integrity": "sha256-jj5toKNSse/TN2Rlnv3hssP7yyr9lGlAFiA3NfwZjzQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.GameIcons.wasm",
        "name": "IconPacks.Avalonia.GameIcons.kxfo5wgn3x.wasm",
        "integrity": "sha256-qMuVkr335A53jPNtCQNcqH/usafrT63RA92RAcVvK2A=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.Ionicons.wasm",
        "name": "IconPacks.Avalonia.Ionicons.kzcj1n3syg.wasm",
        "integrity": "sha256-gKo1RRO8yCf8Sfk9Rl6DzsWENSTCjsg7V4Hzmwhi5eo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.JamIcons.wasm",
        "name": "IconPacks.Avalonia.JamIcons.hmtxnui7ff.wasm",
        "integrity": "sha256-x1xthZLvCU/93AMVOzyGaIQDoqNXULOwJQkE/jrG3Dc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.KeyruneIcons.wasm",
        "name": "IconPacks.Avalonia.KeyruneIcons.75httqk8e0.wasm",
        "integrity": "sha256-9d5hrp09jZuDso/KbLQapiJKkhBOM67r46MaxdWNhRw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.Lucide.wasm",
        "name": "IconPacks.Avalonia.Lucide.ijg6be5aqi.wasm",
        "integrity": "sha256-PkxvELzBwwd/QeVzXzZR68skjjO/YwnOc6/JuIVOvqI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.Material.wasm",
        "name": "IconPacks.Avalonia.Material.01xhky9inh.wasm",
        "integrity": "sha256-dC0qizMaB4NVdLnSQ0ErJlfx1i0qRhCVXXVfLbRy7to=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.MaterialDesign.wasm",
        "name": "IconPacks.Avalonia.MaterialDesign.ytbl4rzwrn.wasm",
        "integrity": "sha256-2tDM3GnMCWrlexEAI1a2zdqWGGl7v6MV3XKn98cNAnY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.MaterialLight.wasm",
        "name": "IconPacks.Avalonia.MaterialLight.9c2d7mf2yz.wasm",
        "integrity": "sha256-GTwDSMQzJpcNZteZ6b4FPBOKNOwdhRQKrWLXrjRPQYU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.MemoryIcons.wasm",
        "name": "IconPacks.Avalonia.MemoryIcons.t7a1n9ubts.wasm",
        "integrity": "sha256-+cQ7hwwXh/EjcLsq0VEzh12yrZVWuLLUVgZIBdnZkeg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.Microns.wasm",
        "name": "IconPacks.Avalonia.Microns.w3o8jt97q1.wasm",
        "integrity": "sha256-DAMrBKlR7AEs1ibf9pecUIxuMB48tPjgoXKfhgnOVB0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.MingCuteIcons.wasm",
        "name": "IconPacks.Avalonia.MingCuteIcons.3suwreshi2.wasm",
        "integrity": "sha256-01Yy3nlwGMAs4PaFS7MK4LoEXteJ+n2Fe45DAc6jrdc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.Modern.wasm",
        "name": "IconPacks.Avalonia.Modern.27wvuhyk1n.wasm",
        "integrity": "sha256-vpaGovo1fU5UE30aFtHkeVEYCJktH1z2hm1NmTbBsrA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.MynaUIIcons.wasm",
        "name": "IconPacks.Avalonia.MynaUIIcons.qm2x5njteg.wasm",
        "integrity": "sha256-biNq5jFiqn6kz4XGRoqLwAY+2am72aQqaSxQEl4sTuU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.Octicons.wasm",
        "name": "IconPacks.Avalonia.Octicons.58kc1z3c3k.wasm",
        "integrity": "sha256-m9G91dY5yR6Wr1je3hxqNOqxkFtB0czNGxDwonUbyeo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.PhosphorIcons.wasm",
        "name": "IconPacks.Avalonia.PhosphorIcons.1ti22ex8f7.wasm",
        "integrity": "sha256-tRXY4N2uau0N8OgOTCrNjdvF4sM4mpOxVXiPGh4kX5Y=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.PicolIcons.wasm",
        "name": "IconPacks.Avalonia.PicolIcons.hhgu21s37d.wasm",
        "integrity": "sha256-C+sQbqvrIXbYJn5GNW3z+1r8KasVPb7PR9+WFgOZcfE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.PixelartIcons.wasm",
        "name": "IconPacks.Avalonia.PixelartIcons.wksms0pvih.wasm",
        "integrity": "sha256-0NmpLkYtSVTdgMREstzLETA/GAqcHF0MAkhv+VNnvhk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.RPGAwesome.wasm",
        "name": "IconPacks.Avalonia.RPGAwesome.9c13e2gkvj.wasm",
        "integrity": "sha256-iT+njIJ+jRUCXq9/P/cch+U/St+l8mD+dJqa9awbUHk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.RadixIcons.wasm",
        "name": "IconPacks.Avalonia.RadixIcons.jr0pe02l6n.wasm",
        "integrity": "sha256-4QFcPmItCnPkBBO7izxIVpNWO6LrjfWqS0ao4PtlN3o=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.RemixIcon.wasm",
        "name": "IconPacks.Avalonia.RemixIcon.jqzucrk28s.wasm",
        "integrity": "sha256-a6jKFkkmv4gwMZGRvhuwFvSSUSMiZM/DdSnk6H+vVlw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.SimpleIcons.wasm",
        "name": "IconPacks.Avalonia.SimpleIcons.2n1m25afop.wasm",
        "integrity": "sha256-zodQQWWlCq8KHltNghcaxVo5IBPZk9fGeETUOx78AOs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.Typicons.wasm",
        "name": "IconPacks.Avalonia.Typicons.66efpucma0.wasm",
        "integrity": "sha256-fiaD5g1+NI4YBEr72tqx9tycuKUCiTD7750JNh2jjA8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.Unicons.wasm",
        "name": "IconPacks.Avalonia.Unicons.m1ayjbi21d.wasm",
        "integrity": "sha256-WgaFOVhOIdsRuG53ht5fK0/Ug+x+Yqe8x4V8aojOEek=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.VaadinIcons.wasm",
        "name": "IconPacks.Avalonia.VaadinIcons.oxoa3wm3jl.wasm",
        "integrity": "sha256-vB/mK8Bru0Nry2sDN34Ie3/iSHNZDvmnb3pPvnAVrtA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.WeatherIcons.wasm",
        "name": "IconPacks.Avalonia.WeatherIcons.jn3dju43gh.wasm",
        "integrity": "sha256-QYGlmQlZOCaajx/WNw0sEeLti7dX9C+HwzJ14olALBc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.Zondicons.wasm",
        "name": "IconPacks.Avalonia.Zondicons.sq7yofz95v.wasm",
        "integrity": "sha256-tH/vOl0zdNJVMvj/wpLq3pfCYj/rxWQ3OAgK0vIitKQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "IconPacks.Avalonia.wasm",
        "name": "IconPacks.Avalonia.zypsfh12hj.wasm",
        "integrity": "sha256-hapdi2UJcUr0v2q4Qwq+tRfR3Y1RmcXsJmQObP1t+vs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "LiveMarkdown.Avalonia.wasm",
        "name": "LiveMarkdown.Avalonia.k601ez379m.wasm",
        "integrity": "sha256-1B7CXwXSyIesA2mqkgT9a7eqtMoHNdrrFw5e7Gbe36Q=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MahApps.IconPacksBrowser.Avalonia.wasm",
        "name": "MahApps.IconPacksBrowser.Avalonia.nfkqei73fd.wasm",
        "integrity": "sha256-DZDf4nzm5QPZMtSBZ+ft0oCHqKYi/3kEuYAAjG740nQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MahApps.IconPacksBrowser.Avalonia.Browser.wasm",
        "name": "MahApps.IconPacksBrowser.Avalonia.Browser.s81sp0w3gx.wasm",
        "integrity": "sha256-TG71RXYhmlh9T4SsbkmX9kKLX73AEV7gnANqrL+LLso=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Markdig.wasm",
        "name": "Markdig.fpr750y7xf.wasm",
        "integrity": "sha256-A422bLb8KOhXCKeWSR6snHckOOZc/3JPzjKgCLGxz1I=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Onigwrap.wasm",
        "name": "Onigwrap.6l085dyb16.wasm",
        "integrity": "sha256-+g0GT35yxZw3CwRFFyFJvk1ZhaGIfRpCD2LXqkNtWok=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SkiaSharp.wasm",
        "name": "SkiaSharp.4gih76pqs9.wasm",
        "integrity": "sha256-pMZ6q9DTvl4pGhbgIwCRV539MP84KtgZLPWZ3qbeiSQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.Concurrent.wasm",
        "name": "System.Collections.Concurrent.fq7d0cepsm.wasm",
        "integrity": "sha256-ZziU8cb8qWV2Evxd1Al2tJrZoGJiViiQAHRipz5YOsQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.Immutable.wasm",
        "name": "System.Collections.Immutable.ymt287xyg6.wasm",
        "integrity": "sha256-ihYnKAOj6odbH4aoaMQBwt2QlRQs89m+kHDMzf10Dwk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.NonGeneric.wasm",
        "name": "System.Collections.NonGeneric.hjzaw7nekb.wasm",
        "integrity": "sha256-k6Gn9u4fYyX9a2ZlX/5HbnDGfWGhPuVKBncECT8IzVY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.Specialized.wasm",
        "name": "System.Collections.Specialized.wgd757iopz.wasm",
        "integrity": "sha256-EP+1Xw/yUfLDQr6knBF7Kg4DqvtWwaPP02BaGVKZxRc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.wasm",
        "name": "System.Collections.tmxvkaw96p.wasm",
        "integrity": "sha256-BfhNVEKr1h0RqInmeq7C+xkLXNZr12qH/4LXU3pT4bc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.Primitives.wasm",
        "name": "System.ComponentModel.Primitives.anoo3q4bma.wasm",
        "integrity": "sha256-xh3KWEdCoSR3J4Ajd6bDLxF62nc40dy6vhfAadRmU9E=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.TypeConverter.wasm",
        "name": "System.ComponentModel.TypeConverter.jdn3qtf6pk.wasm",
        "integrity": "sha256-F/cx9cp1Q8xKjA+OKw1iGyeSHGSYBhoB2yN4YQY6qow=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.wasm",
        "name": "System.ComponentModel.ox0c730jqt.wasm",
        "integrity": "sha256-vKJIfOVbwZcFjiBgMpVzmwnrfkMAEfHGXgoUx8126bk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Console.wasm",
        "name": "System.Console.osybz928gs.wasm",
        "integrity": "sha256-6GvJkHC5oPcTkzcMeoofKgPzEWix1uhEfGWjNFHlYSE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.DiagnosticSource.wasm",
        "name": "System.Diagnostics.DiagnosticSource.r36gmqh2o4.wasm",
        "integrity": "sha256-GW+HnUONRzLzQq5stqbB4sdeN2JSn7OigDQzG1bwZH0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.TraceSource.wasm",
        "name": "System.Diagnostics.TraceSource.k3mqujah0x.wasm",
        "integrity": "sha256-YNx68yhDjqR46wGKCuQGFJta2yaFOChUOW9SqaS8/wg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Pipelines.wasm",
        "name": "System.IO.Pipelines.ui0nzyeuli.wasm",
        "integrity": "sha256-Zo3WhHKpsUZdPl4HU2ZRA1HQouyTNRHVc2BCPEbuwuw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.Expressions.wasm",
        "name": "System.Linq.Expressions.zvo3b3pvxo.wasm",
        "integrity": "sha256-v8ePxbPiiDht+TaczNcYGhoDVQZLp1WshYcjsjS1Xsk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.wasm",
        "name": "System.Linq.mb169znhez.wasm",
        "integrity": "sha256-zu7ZTgtQBszulJAemULTgIajhkukSe46Q9+lXRRfDY8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Memory.wasm",
        "name": "System.Memory.z6yvjycv3n.wasm",
        "integrity": "sha256-JQZLOVR8stCIwnoOGMXo0HQ5OZoBjP7BpQETWeEuoKQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Http.wasm",
        "name": "System.Net.Http.p81e2t2xw0.wasm",
        "integrity": "sha256-N8w5xW5nsR7Ac/VdcVfYIBbowXGDBp57ho9fI7lb/9k=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Primitives.wasm",
        "name": "System.Net.Primitives.g4tvxp1wad.wasm",
        "integrity": "sha256-0IWcODGGafGRfK9wz3AW/oZbjmmKiYqeUWJUfopFKMc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ObjectModel.wasm",
        "name": "System.ObjectModel.8llc6x7zbj.wasm",
        "integrity": "sha256-TBKVMiC9xAxFOk3M9ZIFe/ega6+LLENu0QkGIHEE2AI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.CoreLib.wasm",
        "name": "System.Private.CoreLib.vk5nmc4bkj.wasm",
        "integrity": "sha256-Af7ikkwvHKXvsVTJj0KEFXhmuIhHpecAU0bMpD8hVG8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.Uri.wasm",
        "name": "System.Private.Uri.kzoe8ifsy0.wasm",
        "integrity": "sha256-APYpqu6mGuwrcQjmZOMJRpinW5bn0G0xH37NdPZ5Soc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reactive.wasm",
        "name": "System.Reactive.ckw7sq47eg.wasm",
        "integrity": "sha256-ZBsKLF7AxbmIeRhJyA6yYoYlO8+wjpIHkVs+IdPc/ds=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.InteropServices.JavaScript.wasm",
        "name": "System.Runtime.InteropServices.JavaScript.ivivbfkunn.wasm",
        "integrity": "sha256-Trhb4HhX3bsIEPIpyz8T/d017F5g2SL+pcgPuNMI8r4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Encodings.Web.wasm",
        "name": "System.Text.Encodings.Web.6x4vfd2nbd.wasm",
        "integrity": "sha256-qs2F2V9H9hlrAcuDZCCAyjJz9yuO52WT6HRXZSfqjw8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Json.wasm",
        "name": "System.Text.Json.jw3wq0hlj0.wasm",
        "integrity": "sha256-w4VfRqsE3roa5v9fpaj077E+nlK/iQo1NWnmJ9lRCHg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.RegularExpressions.wasm",
        "name": "System.Text.RegularExpressions.gxa8pe1fnj.wasm",
        "integrity": "sha256-rJU8AHh7DImdxVj2z3PcVa6BPcpDWFka5eKpIA1IJNM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.wasm",
        "name": "System.6ata22n2ok.wasm",
        "integrity": "sha256-QtNQDl1G146IubTNbgjYbQoRbKzZLw21++nXIVBupWI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "TextMateSharp.Grammars.wasm",
        "name": "TextMateSharp.Grammars.9h80lm4rgp.wasm",
        "integrity": "sha256-E4g6eo5zhsO1GEK4bHOPIUr2QLg4VsUBdc9l1xOgSlc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "TextMateSharp.wasm",
        "name": "TextMateSharp.d92yhichf0.wasm",
        "integrity": "sha256-D4f+M1Qblj68ymRSwg+CQBLZsAB0AkeIQOoZv8Ua4B4=",
        "cache": "force-cache"
      }
    ],
    "assembly": [],
    "libraryInitializers": [
      {
        "name": "_content/Microsoft.DotNet.HotReload.WebAssembly.Browser/Microsoft.DotNet.HotReload.WebAssembly.Browser.99zm1jdh75.lib.module.js"
      }
    ],
    "modulesAfterConfigLoaded": [
      {
        "name": "../_content/Microsoft.DotNet.HotReload.WebAssembly.Browser/Microsoft.DotNet.HotReload.WebAssembly.Browser.99zm1jdh75.lib.module.js"
      }
    ]
  },
  "debugLevel": 0,
  "linkerEnabled": true,
  "globalizationMode": "sharded",
  "runtimeConfig": {
    "runtimeOptions": {
      "configProperties": {
        "MVVMTOOLKIT_ENABLE_INOTIFYPROPERTYCHANGING_SUPPORT": true,
        "Microsoft.Extensions.DependencyInjection.VerifyOpenGenericServiceTrimmability": true,
        "System.ComponentModel.DefaultValueAttribute.IsSupported": false,
        "System.ComponentModel.Design.IDesignerHost.IsSupported": false,
        "System.ComponentModel.TypeConverter.EnableUnsafeBinaryFormatterInDesigntimeLicenseContextSerialization": false,
        "System.ComponentModel.TypeDescriptor.IsComObjectDescriptorSupported": false,
        "System.Data.DataSet.XmlSerializationIsSupported": false,
        "System.Diagnostics.Debugger.IsSupported": false,
        "System.Diagnostics.Metrics.Meter.IsSupported": false,
        "System.Diagnostics.Tracing.EventSource.IsSupported": false,
        "System.Globalization.Invariant": false,
        "System.TimeZoneInfo.Invariant": false,
        "System.Linq.Enumerable.IsSizeOptimized": true,
        "System.Net.Http.EnableActivityPropagation": false,
        "System.Net.Http.WasmEnableStreamingResponse": true,
        "System.Net.SocketsHttpHandler.Http3Support": false,
        "System.Reflection.Metadata.MetadataUpdater.IsSupported": false,
        "System.Resources.ResourceManager.AllowCustomResourceTypes": false,
        "System.Resources.UseSystemResourceKeys": true,
        "System.Runtime.CompilerServices.RuntimeFeature.IsDynamicCodeSupported": true,
        "System.Runtime.InteropServices.BuiltInComInterop.IsSupported": false,
        "System.Runtime.InteropServices.EnableConsumingManagedCodeFromNativeHosting": false,
        "System.Runtime.InteropServices.EnableCppCLIHostActivation": false,
        "System.Runtime.InteropServices.Marshalling.EnableGeneratedComInterfaceComImportInterop": false,
        "System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization": false,
        "System.StartupHookProvider.IsSupported": false,
        "System.Text.Encoding.EnableUnsafeUTF7Encoding": false,
        "System.Text.Json.JsonSerializer.IsReflectionEnabledByDefault": false,
        "System.Threading.Thread.EnableAutoreleasePool": false
      }
    }
  }
}/*json-end*/);export{gt as default,ft as dotnet,mt as exit};
