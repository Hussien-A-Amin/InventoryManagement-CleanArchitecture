export interface Iproduct {
id:string,
title:string,
description:string
isActive:boolean,
price:number

}
export interface INewproduct {
title:string,
description:string
price:number
}
export interface IEditproduct {
id:string,
title:string,
description:string
price:number
}
export interface IAuthResponse {
refreshToken: string
token: string
}
export interface IProblemDetails {
    type?: string;
    title?: string;
    status?: number;
    detail?: string;
    instance?: string;
    errors?: Record<string, string[]>;
    [key: string]: any;
}