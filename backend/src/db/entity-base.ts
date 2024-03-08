import { CreateDateColumn } from "typeorm";

export abstract class EntityBase {
    @CreateDateColumn({ type: "timestamp" })
    createdAt: Date;
}