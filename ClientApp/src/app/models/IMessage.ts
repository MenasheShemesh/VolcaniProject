import { KeyValue } from "@angular/common";
import { IPhone } from "./IPhone";
import { IWorker } from "./IWorker";

export interface IMessage {
    Id?: number,
    Worker: IWorker,
    Phone: IPhone,
    Content: string,
    Status?: KeyValue<number, string>
    CreateTime?: Date,
    UpdateTime?: Date
}