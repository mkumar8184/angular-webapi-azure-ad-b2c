
import { MsalGuardConfiguration, MsalInterceptorConfiguration } from '@azure/msal-angular';
import { LogLevel, Configuration, BrowserCacheLocation, PublicClientApplication, IPublicClientApplication, InteractionType } from '@azure/msal-browser';

const isIE = window.navigator.userAgent.indexOf("MSIE ") > -1 || window.navigator.userAgent.indexOf("Trident/") > -1;

export const b2cPoliciesSetting = {
    names: {
        signUpSignIn: "B2C_1_new_user"
         //other policies can be added 


    },
    authorities: {
        signUpSignIn: {
            authority: "https://xxxxxxx.b2clogin.com/xxxxxxxx.onmicrosoft.com/B2C_1_new_user",
        }

    },
    authorityDomain: "xxxxxxxx.b2clogin.com"
};


export const msalConfig: Configuration = {
    auth: {
        clientId: 'ws3333333333333333333333-9c8d-be5408917063', // mandatory to provide client id.
        authority: b2cPoliciesSetting.authorities.signUpSignIn.authority,
        knownAuthorities: [b2cPoliciesSetting.authorityDomain],
        redirectUri: '/', //register this url on azure b2c portal application registration
        postLogoutRedirectUri: '/', // redirect after logout.
        navigateToLoginRequestUrl: true, // If "true", will navigate back to the original request location before processing the auth code response.
    },
    cache: {
        cacheLocation: BrowserCacheLocation.LocalStorage, // Configures cache location. "sessionStorage" is more secure, but "localStorage" gives you SSO between tabs.
        storeAuthStateInCookie: isIE, // Set this to "true" if you are having issues on IE11 or Edge
    },
    system: {
        loggerOptions: {
            loggerCallback(logLevel: LogLevel, message: string) {
                console.log(message);
            },
            logLevel: LogLevel.Verbose,
            piiLoggingEnabled: false
        }
    }
}
export const protectedResources = {
    apiResource: {
        endpoint: "http://localhost:5199", //secure web api url
        scopes: ["https://xxxxxxxxx.onmicrosoft.com/tenant/api.readwrite.all"] //azure expose api url

    }

}
export function MSALInstanceFactory(): IPublicClientApplication {
    return new PublicClientApplication(msalConfig);
}

export function MSALGuardConfigFactory(): MsalGuardConfiguration {
    return {
        interactionType: InteractionType.Redirect, //you can use redirect/popup to open login screen
    };
}
//configure interceptor 
export function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
    const protectedResourceMap = new Map<string,
        Array<string>>();
    protectedResourceMap.set(protectedResources.apiResource.endpoint, protectedResources.apiResource.scopes);
    return {
        interactionType: InteractionType.Redirect,
        protectedResourceMap
    }
}
