import { CommonCode } from "src/db/entity/common-code";
import { CommonCodeType } from "src/db/entity/common-code-type";

/*

codeCache: {
	hierarchical: {
		<type-id>: {
			type: <type-entity> 
			codes: {
				<code-id>: <common-codeentity>
				...
			}
		}
	}
	horizontal: {
		<full-code>: <entity>
		...
	}
}

*/
type CommonCodeTypeId = typeof CommonCodeType.prototype.id;
type CommonCodeId = typeof CommonCode.prototype.id;

export class CodeCacheHierarchyTypeData {
    type: CommonCodeType;
    codes: Map<CommonCodeId, CommonCode>;

	constructor(type: CommonCodeType) {
		this.type = type;
		this.codes = new Map();
	}
}

export class CodeCache {
    hierarchical: Map<CommonCodeTypeId, CodeCacheHierarchyTypeData>;
    horizontal: Map<string, CommonCode>;

	constructor() {
		this.hierarchical = new Map();
		this.horizontal = new Map();
	}
}

