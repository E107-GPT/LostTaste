// {
// 	<prefix>: {
// 		<code>: <description>
//  }
// }

export interface CodeRecord {
    code: string,
    description: string
}

export type CodeMap = Map<string, CodeRecord[]>;

export interface PrefixAndCode {
    prefix: string,
    code: string
}