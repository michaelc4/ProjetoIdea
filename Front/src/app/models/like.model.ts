import { Serializable } from './serializable';

export class LikePostParamModel extends Serializable {
    public ipUsr: string = '';
    public ideiaId?: string = '';
    public problemaId?: string = '';
}
