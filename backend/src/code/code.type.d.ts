type CustomCodeMap = Map<string, string>;
type CustomCodeTypeMap = Map<string, CustomCodeMap>;
type ItemCodeMap = Map<string, string>;

// {
// 	custom: {
// 		<prefix>: {
// 			<code>: <description>
// 		}
// 	}
// 	item: {
// 		<code>: <description>
// 	}
// }
export interface CodeMap {
    custom: CustomCodeTypeMap
    item: ItemCodeMap;
}

export interface PrefixAndCode {
    prefix: string,
    code: string
}