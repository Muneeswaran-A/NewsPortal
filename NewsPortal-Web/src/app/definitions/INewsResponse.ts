import { IPaginationMetadata } from "./IPaginationMetadata";
import { INews } from "./inews";

export interface INewsResponse {
    metadata: IPaginationMetadata,
    items: INews[]
}