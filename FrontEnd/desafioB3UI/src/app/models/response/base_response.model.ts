import { ErrorResponse } from './error_response.model';

export class BaseResponse {
  public success!: boolean;
  public status!: number;
  public data!: any;
  public error!: ErrorResponse;
}
