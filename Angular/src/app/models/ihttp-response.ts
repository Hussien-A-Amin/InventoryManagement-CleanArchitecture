export interface IhttpResponse<T> {
    isSuccess: boolean,
    message: string,
    data: T
}
