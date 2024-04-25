import { CallHandler, ExecutionContext, NestInterceptor } from "@nestjs/common";
import { Observable, map } from "rxjs";

/**
 * Map(new Map())을 Object({...})로 바꾸는 Interceptor
 */
export class MapToObjectInterceptor implements NestInterceptor {
    intercept(context: ExecutionContext, next: CallHandler<any>): Observable<any> {
        return next.handle().pipe(
            map(data => this.transformData(data))
        );
    }

    private transformData(data: any): any {
        if (data instanceof Map) {
            return this.mapToObject(data);
        } else if (Array.isArray(data)) {
            return data.map(item => this.transformData(item));
        } else if (typeof data === 'object' && data !== null) {
            const transformedData: any = {};
            for (const key of Object.keys(data)) {
                transformedData[key] = this.transformData(data[key]);
            }
            return transformedData;
        }
        return data;
    }

    private mapToObject(map: Map<any, any>): any {
        const obj: any = {};
        map.forEach((value, key) => {
            obj[key] = this.transformData(value);
        });
        return obj;
    }
}