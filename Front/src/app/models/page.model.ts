import { Serializable } from './serializable';

export class PageModel extends Serializable {
    public size: number = 0;
    public totalElements: number = 0;
    public totalPages: number = 0;
    public pageNumber: number = 0;
}