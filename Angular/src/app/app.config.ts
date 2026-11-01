import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideSweetAlert2 } from '@sweetalert2/ngx-sweetalert2';

import { routes } from './app.routes';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { httpInterceptor } from './Interceptors/httpInterceptor';
import { provideStore } from '@ngrx/store';
import { counterReducer } from './store/counter/conter.reducer';
import { languageReducer } from './store/language/language.reducer';
import { provideEffects } from '@ngrx/effects';
import { LanguageEffect } from './store/language/language.effect';
import { localConfigReducer } from './store/localConfig/localconfig.reducer';

export const appConfig: ApplicationConfig = {
    providers: [
        provideBrowserGlobalErrorListeners(),
        provideRouter(routes),
        provideSweetAlert2({
            // Optional configuration
            fireOnInit: false,
            dismissOnDestroy: true,
        }),
        provideHttpClient(withFetch(), withInterceptors([httpInterceptor])),
        provideStore({
            counter: counterReducer,
            language: languageReducer,
            localConfig:localConfigReducer
        }),
        // provideEffects([LanguageEffect]),
    ],
};
