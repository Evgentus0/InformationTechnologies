// package: dbmsCore
// file: src/app/protos/dbmsCore.proto

import * as src_app_protos_dbmsCore_pb from "./dbmsCore_pb";
import {grpc} from "@improbable-eng/grpc-web";

type GrpcDBServiceCreateDatabase = {
  readonly methodName: string;
  readonly service: typeof GrpcDBService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbmsCore_pb.CreateDbRequest;
  readonly responseType: typeof src_app_protos_dbmsCore_pb.BaseReply;
};

type GrpcDBServiceCreateTable = {
  readonly methodName: string;
  readonly service: typeof GrpcDBService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbmsCore_pb.TableRequest;
  readonly responseType: typeof src_app_protos_dbmsCore_pb.BaseReply;
};

type GrpcDBServiceDeleteTable = {
  readonly methodName: string;
  readonly service: typeof GrpcDBService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbmsCore_pb.TableRequest;
  readonly responseType: typeof src_app_protos_dbmsCore_pb.BaseReply;
};

type GrpcDBServiceGetTableList = {
  readonly methodName: string;
  readonly service: typeof GrpcDBService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbmsCore_pb.GetTableListRequest;
  readonly responseType: typeof src_app_protos_dbmsCore_pb.GetTableListReply;
};

type GrpcDBServiceGetTable = {
  readonly methodName: string;
  readonly service: typeof GrpcDBService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbmsCore_pb.GetEntityRequest;
  readonly responseType: typeof src_app_protos_dbmsCore_pb.GetEntityReply;
};

export class GrpcDBService {
  static readonly serviceName: string;
  static readonly CreateDatabase: GrpcDBServiceCreateDatabase;
  static readonly CreateTable: GrpcDBServiceCreateTable;
  static readonly DeleteTable: GrpcDBServiceDeleteTable;
  static readonly GetTableList: GrpcDBServiceGetTableList;
  static readonly GetTable: GrpcDBServiceGetTable;
}

type GrpcTableServiceAddColumn = {
  readonly methodName: string;
  readonly service: typeof GrpcTableService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbmsCore_pb.AddColumnRequest;
  readonly responseType: typeof src_app_protos_dbmsCore_pb.BaseReply;
};

type GrpcTableServiceDropColumn = {
  readonly methodName: string;
  readonly service: typeof GrpcTableService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbmsCore_pb.DropColumnRequst;
  readonly responseType: typeof src_app_protos_dbmsCore_pb.BaseReply;
};

type GrpcTableServiceInsert = {
  readonly methodName: string;
  readonly service: typeof GrpcTableService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbmsCore_pb.InsertRequest;
  readonly responseType: typeof src_app_protos_dbmsCore_pb.BaseReply;
};

type GrpcTableServiceDelete = {
  readonly methodName: string;
  readonly service: typeof GrpcTableService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbmsCore_pb.DeleteRequest;
  readonly responseType: typeof src_app_protos_dbmsCore_pb.BaseReply;
};

type GrpcTableServiceUpdate = {
  readonly methodName: string;
  readonly service: typeof GrpcTableService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbmsCore_pb.UpdateRequest;
  readonly responseType: typeof src_app_protos_dbmsCore_pb.BaseReply;
};

type GrpcTableServiceSelect = {
  readonly methodName: string;
  readonly service: typeof GrpcTableService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbmsCore_pb.SelectRequest;
  readonly responseType: typeof src_app_protos_dbmsCore_pb.SelectReply;
};

type GrpcTableServiceUnion = {
  readonly methodName: string;
  readonly service: typeof GrpcTableService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbmsCore_pb.UnionRequest;
  readonly responseType: typeof src_app_protos_dbmsCore_pb.SelectReply;
};

export class GrpcTableService {
  static readonly serviceName: string;
  static readonly AddColumn: GrpcTableServiceAddColumn;
  static readonly DropColumn: GrpcTableServiceDropColumn;
  static readonly Insert: GrpcTableServiceInsert;
  static readonly Delete: GrpcTableServiceDelete;
  static readonly Update: GrpcTableServiceUpdate;
  static readonly Select: GrpcTableServiceSelect;
  static readonly Union: GrpcTableServiceUnion;
}

export type ServiceError = { message: string, code: number; metadata: grpc.Metadata }
export type Status = { details: string, code: number; metadata: grpc.Metadata }

interface UnaryResponse {
  cancel(): void;
}
interface ResponseStream<T> {
  cancel(): void;
  on(type: 'data', handler: (message: T) => void): ResponseStream<T>;
  on(type: 'end', handler: (status?: Status) => void): ResponseStream<T>;
  on(type: 'status', handler: (status: Status) => void): ResponseStream<T>;
}
interface RequestStream<T> {
  write(message: T): RequestStream<T>;
  end(): void;
  cancel(): void;
  on(type: 'end', handler: (status?: Status) => void): RequestStream<T>;
  on(type: 'status', handler: (status: Status) => void): RequestStream<T>;
}
interface BidirectionalStream<ReqT, ResT> {
  write(message: ReqT): BidirectionalStream<ReqT, ResT>;
  end(): void;
  cancel(): void;
  on(type: 'data', handler: (message: ResT) => void): BidirectionalStream<ReqT, ResT>;
  on(type: 'end', handler: (status?: Status) => void): BidirectionalStream<ReqT, ResT>;
  on(type: 'status', handler: (status: Status) => void): BidirectionalStream<ReqT, ResT>;
}

export class GrpcDBServiceClient {
  readonly serviceHost: string;

  constructor(serviceHost: string, options?: grpc.RpcOptions);
  createDatabase(
    requestMessage: src_app_protos_dbmsCore_pb.CreateDbRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.BaseReply|null) => void
  ): UnaryResponse;
  createDatabase(
    requestMessage: src_app_protos_dbmsCore_pb.CreateDbRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.BaseReply|null) => void
  ): UnaryResponse;
  createTable(
    requestMessage: src_app_protos_dbmsCore_pb.TableRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.BaseReply|null) => void
  ): UnaryResponse;
  createTable(
    requestMessage: src_app_protos_dbmsCore_pb.TableRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.BaseReply|null) => void
  ): UnaryResponse;
  deleteTable(
    requestMessage: src_app_protos_dbmsCore_pb.TableRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.BaseReply|null) => void
  ): UnaryResponse;
  deleteTable(
    requestMessage: src_app_protos_dbmsCore_pb.TableRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.BaseReply|null) => void
  ): UnaryResponse;
  getTableList(
    requestMessage: src_app_protos_dbmsCore_pb.GetTableListRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.GetTableListReply|null) => void
  ): UnaryResponse;
  getTableList(
    requestMessage: src_app_protos_dbmsCore_pb.GetTableListRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.GetTableListReply|null) => void
  ): UnaryResponse;
  getTable(
    requestMessage: src_app_protos_dbmsCore_pb.GetEntityRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.GetEntityReply|null) => void
  ): UnaryResponse;
  getTable(
    requestMessage: src_app_protos_dbmsCore_pb.GetEntityRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.GetEntityReply|null) => void
  ): UnaryResponse;
}

export class GrpcTableServiceClient {
  readonly serviceHost: string;

  constructor(serviceHost: string, options?: grpc.RpcOptions);
  addColumn(
    requestMessage: src_app_protos_dbmsCore_pb.AddColumnRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.BaseReply|null) => void
  ): UnaryResponse;
  addColumn(
    requestMessage: src_app_protos_dbmsCore_pb.AddColumnRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.BaseReply|null) => void
  ): UnaryResponse;
  dropColumn(
    requestMessage: src_app_protos_dbmsCore_pb.DropColumnRequst,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.BaseReply|null) => void
  ): UnaryResponse;
  dropColumn(
    requestMessage: src_app_protos_dbmsCore_pb.DropColumnRequst,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.BaseReply|null) => void
  ): UnaryResponse;
  insert(
    requestMessage: src_app_protos_dbmsCore_pb.InsertRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.BaseReply|null) => void
  ): UnaryResponse;
  insert(
    requestMessage: src_app_protos_dbmsCore_pb.InsertRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.BaseReply|null) => void
  ): UnaryResponse;
  delete(
    requestMessage: src_app_protos_dbmsCore_pb.DeleteRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.BaseReply|null) => void
  ): UnaryResponse;
  delete(
    requestMessage: src_app_protos_dbmsCore_pb.DeleteRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.BaseReply|null) => void
  ): UnaryResponse;
  update(
    requestMessage: src_app_protos_dbmsCore_pb.UpdateRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.BaseReply|null) => void
  ): UnaryResponse;
  update(
    requestMessage: src_app_protos_dbmsCore_pb.UpdateRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.BaseReply|null) => void
  ): UnaryResponse;
  select(
    requestMessage: src_app_protos_dbmsCore_pb.SelectRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.SelectReply|null) => void
  ): UnaryResponse;
  select(
    requestMessage: src_app_protos_dbmsCore_pb.SelectRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.SelectReply|null) => void
  ): UnaryResponse;
  union(
    requestMessage: src_app_protos_dbmsCore_pb.UnionRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.SelectReply|null) => void
  ): UnaryResponse;
  union(
    requestMessage: src_app_protos_dbmsCore_pb.UnionRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbmsCore_pb.SelectReply|null) => void
  ): UnaryResponse;
}

