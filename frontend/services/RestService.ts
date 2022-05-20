export default class RestService {

    /**
     * The general shaped request function for doing a
     * REST http request.
     *
     * @param method The request method
     * @param path The path to the endpoint
     * @param body The request body if its given
     * @param contentType The content type of the request
     * @param emptyResponse If the response should be empty
     * @param blob If the response should be returned as blob
     * @return Promise<T> The generic promise response
     * @throws Error If the status code is not 200
     */
    private static async fetchEndpoint<T>(
        method: string,
        path: string,
        body?: any,
        contentType?: string,
        emptyResponse: boolean = false,
        blob: boolean = false
    ): Promise<T|Blob> {
        const fetchResult = await window.fetch(path, {
            body: body,
            method: method,
            mode: process.env.NODE_ENV === "production" ? "same-origin" : "cors",
            headers: contentType ? {
                'Content-Type': contentType
            }: undefined,
            credentials: process.env.NODE_ENV === "production" ? "same-origin" : "include"
        });
        if (fetchResult.status === 401) {
            throw new Error("Unauthorized");
        }
        if (fetchResult.status !== 200 && fetchResult.status !== 204) {
            // Parse to generic error response
            throw new Error('Something went wrong');
        }
        if (fetchResult.status === 204) {
            return {} as any;
        }
        if (blob) {
            return await fetchResult.blob();
        }
        if (!emptyResponse) {
            return (await fetchResult.json()) as T;
        }
        return {} as any;
    }

    /**
     * The general GET request.
     *
     * @param path The path to the endpoint
     * @param blob If the response should be returned as blob
     * @return Promise<T> The response as generic promise
     */
    protected async get<T>(path: string, blob: boolean = false): Promise<T|Blob> {
        return await RestService.fetchEndpoint<T>("GET", path, undefined, 'application/json');
    }

    /**
     * The general POST request.
     *
     * @param path The path to the resp endpoint
     * @param body The http body of the request
     * @param emptyResponse If the response has no json body
     * @param contentType The content type that is used for the request
     * @return Promise<T> The response as generic promise
     */
    protected async post<T>(path: string, body: any, emptyResponse: boolean = false, contentType?: string): Promise<T|Blob> {
        return await RestService.fetchEndpoint<T>("POST", path, body, contentType, emptyResponse);
    }

    /**
     * The general DELETE request.
     *
     * @param path The path to the REST endopint.
     * @param body The body of the delete request
     */
    protected async delete<T>(path: string, body?: any): Promise<T|Blob> {
        return await RestService.fetchEndpoint<T>("DELETE", path, body, 'application/json');
    }
}