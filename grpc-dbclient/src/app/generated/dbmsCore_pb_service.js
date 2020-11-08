// package: dbmsCore
// file: src/app/protos/dbmsCore.proto

var src_app_protos_dbmsCore_pb = require("./dbmsCore_pb");
var grpc = require("@improbable-eng/grpc-web").grpc;

var GrpcDBService = (function () {
  function GrpcDBService() {}
  GrpcDBService.serviceName = "dbmsCore.GrpcDBService";
  return GrpcDBService;
}());

GrpcDBService.CreateDatabase = {
  methodName: "CreateDatabase",
  service: GrpcDBService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbmsCore_pb.CreateDbRequest,
  responseType: src_app_protos_dbmsCore_pb.BaseReply
};

GrpcDBService.CreateTable = {
  methodName: "CreateTable",
  service: GrpcDBService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbmsCore_pb.TableRequest,
  responseType: src_app_protos_dbmsCore_pb.BaseReply
};

GrpcDBService.DeleteTable = {
  methodName: "DeleteTable",
  service: GrpcDBService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbmsCore_pb.TableRequest,
  responseType: src_app_protos_dbmsCore_pb.BaseReply
};

GrpcDBService.GetTableList = {
  methodName: "GetTableList",
  service: GrpcDBService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbmsCore_pb.GetTableListRequest,
  responseType: src_app_protos_dbmsCore_pb.GetTableListReply
};

GrpcDBService.GetTable = {
  methodName: "GetTable",
  service: GrpcDBService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbmsCore_pb.GetEntityRequest,
  responseType: src_app_protos_dbmsCore_pb.GetEntityReply
};

exports.GrpcDBService = GrpcDBService;

function GrpcDBServiceClient(serviceHost, options) {
  this.serviceHost = serviceHost;
  this.options = options || {};
}

GrpcDBServiceClient.prototype.createDatabase = function createDatabase(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(GrpcDBService.CreateDatabase, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

GrpcDBServiceClient.prototype.createTable = function createTable(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(GrpcDBService.CreateTable, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

GrpcDBServiceClient.prototype.deleteTable = function deleteTable(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(GrpcDBService.DeleteTable, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

GrpcDBServiceClient.prototype.getTableList = function getTableList(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(GrpcDBService.GetTableList, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

GrpcDBServiceClient.prototype.getTable = function getTable(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(GrpcDBService.GetTable, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

exports.GrpcDBServiceClient = GrpcDBServiceClient;

var GrpcTableService = (function () {
  function GrpcTableService() {}
  GrpcTableService.serviceName = "dbmsCore.GrpcTableService";
  return GrpcTableService;
}());

GrpcTableService.AddColumn = {
  methodName: "AddColumn",
  service: GrpcTableService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbmsCore_pb.AddColumnRequest,
  responseType: src_app_protos_dbmsCore_pb.BaseReply
};

GrpcTableService.DropColumn = {
  methodName: "DropColumn",
  service: GrpcTableService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbmsCore_pb.DropColumnRequst,
  responseType: src_app_protos_dbmsCore_pb.BaseReply
};

GrpcTableService.Insert = {
  methodName: "Insert",
  service: GrpcTableService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbmsCore_pb.InsertRequest,
  responseType: src_app_protos_dbmsCore_pb.BaseReply
};

GrpcTableService.Delete = {
  methodName: "Delete",
  service: GrpcTableService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbmsCore_pb.DeleteRequest,
  responseType: src_app_protos_dbmsCore_pb.BaseReply
};

GrpcTableService.Update = {
  methodName: "Update",
  service: GrpcTableService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbmsCore_pb.UpdateRequest,
  responseType: src_app_protos_dbmsCore_pb.BaseReply
};

GrpcTableService.Select = {
  methodName: "Select",
  service: GrpcTableService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbmsCore_pb.SelectRequest,
  responseType: src_app_protos_dbmsCore_pb.SelectReply
};

GrpcTableService.Union = {
  methodName: "Union",
  service: GrpcTableService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbmsCore_pb.UnionRequest,
  responseType: src_app_protos_dbmsCore_pb.SelectReply
};

exports.GrpcTableService = GrpcTableService;

function GrpcTableServiceClient(serviceHost, options) {
  this.serviceHost = serviceHost;
  this.options = options || {};
}

GrpcTableServiceClient.prototype.addColumn = function addColumn(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(GrpcTableService.AddColumn, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

GrpcTableServiceClient.prototype.dropColumn = function dropColumn(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(GrpcTableService.DropColumn, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

GrpcTableServiceClient.prototype.insert = function insert(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(GrpcTableService.Insert, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

GrpcTableServiceClient.prototype.delete = function pb_delete(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(GrpcTableService.Delete, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

GrpcTableServiceClient.prototype.update = function update(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(GrpcTableService.Update, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

GrpcTableServiceClient.prototype.select = function select(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(GrpcTableService.Select, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

GrpcTableServiceClient.prototype.union = function union(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(GrpcTableService.Union, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

exports.GrpcTableServiceClient = GrpcTableServiceClient;

