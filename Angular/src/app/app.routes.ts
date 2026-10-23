import { Routes } from '@angular/router';
import { Home } from './components/home/home';
import { About } from './components/public/about/about';
import { Shop } from './components/client/shop/shop';
import { NotFound } from './components/not-found/not-found';
import { __values } from 'tslib';
import { Login } from './components/login/login';
import { authGuard } from './guards/auth-guard';
import { AddProduct } from './components/client/add-product/add-product';
import { ClientOutlet } from './components/client/client-outlet/client-outlet';
import { Settings } from './components/client/settings/settings';
import { Overview } from './components/client/overview/overview';
import { PublicOutlet } from './components/public/public-outlet/public-outlet';
import { ProductDetails } from './components/client/product-details/product-details';
import { PortalOutlet } from './components/portal/portal-outlet/portal-outlet';
import { AdminOutlet } from './components/admin/admin-outlet/admin-outlet';

export const routes: Routes = [
    { path: '', component: Home },




    {
        path: 'public', component: PublicOutlet, children: [


            { path: 'about', component: About },
            { path: 'shop', component: Shop },

        ]
    },

    {
        path: 'client', component: ClientOutlet, canActivate: [authGuard], children: [
            { path: '', pathMatch: 'full', redirectTo: 'overview' },
            { path: 'overview', component: Overview },
            { path: 'shop', component: Shop },
            { path: 'settings', component: Settings },
            { path: 'ProductsDetails/:id', component: ProductDetails },
            { path: 'shop/add', component: AddProduct }
        ]
    },
   
   
    {
        path: 'admin', component: AdminOutlet, canActivate: [authGuard], children: [
            { path: '', pathMatch: 'full', redirectTo: 'overview' },
            { path: 'overview', component: Overview },
            { path: 'shop', component: Shop },
            { path: 'settings', component: Settings },
            { path: 'ProductsDetails/:id', component: ProductDetails },
            { path: 'shop/add', component: AddProduct }
        ]
    },


    {
        path: 'portal', component: PortalOutlet, canActivate: [authGuard], children: [
            { path: '', pathMatch: 'full', redirectTo: 'overview' },
            { path: 'overview', component: Overview },
            { path: 'shop', component: Shop },
            { path: 'settings', component: Settings },
            { path: 'ProductsDetails/:id', component: ProductDetails },
            { path: 'shop/add', component: AddProduct }
        ]
    },


    


    { path: 'login', component: Login },
    { path: '**', component: NotFound },


];
