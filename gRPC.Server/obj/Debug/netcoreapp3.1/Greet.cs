// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: greet.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace gRPC.Server {

  /// <summary>Holder for reflection information generated from greet.proto</summary>
  public static partial class GreetReflection {

    #region Descriptor
    /// <summary>File descriptor for greet.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static GreetReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgtncmVldC5wcm90bxIFZ3JlZXQiLAoKRmlsZUJ1ZmZlchIQCghmaWxlbmFt",
            "ZRgBIAEoCRIMCgRpbmZvGAIgASgMIiYKCUlzV3JpdHRlbhIZChFpc1dyaXR0",
            "ZW5JblNlcnZlchgBIAEoCCIcCghXaGF0RmlsZRIQCghmaWxlbmFtZRgBIAEo",
            "CSIZCghVc2VyTGlzdBINCgV1c2VycxgBIAEoCSIZCghGaWxlTGlzdBINCgVm",
            "aWxlcxgBIAEoCSIbCghDbGVhckFsbBIPCgdpc0NsZWFyGAEgASgIIhwKDEhl",
            "bGxvUmVxdWVzdBIMCgRuYW1lGAEgASgJIh0KCkhlbGxvUmVwbHkSDwoHbWVz",
            "c2FnZRgBIAEoCTL3AgoNQ3J5cHRvX1NlcnZlchIyCghTYXlIZWxsbxITLmdy",
            "ZWV0LkhlbGxvUmVxdWVzdBoRLmdyZWV0LkhlbGxvUmVwbHkSMAoGU2F5Qnll",
            "EhMuZ3JlZXQuSGVsbG9SZXF1ZXN0GhEuZ3JlZXQuSGVsbG9SZXBseRIvCghT",
            "ZW5kRmlsZRIRLmdyZWV0LkZpbGVCdWZmZXIaEC5ncmVldC5Jc1dyaXR0ZW4S",
            "LgoIVGFrZUZpbGUSDy5ncmVldC5XaGF0RmlsZRoRLmdyZWV0LkZpbGVCdWZm",
            "ZXISMwoLV2hvQXRTZXJ2ZXISEy5ncmVldC5IZWxsb1JlcXVlc3QaDy5ncmVl",
            "dC5Vc2VyTGlzdBI4ChBUYWtlQWxsRmlsZU5hbWVzEhMuZ3JlZXQuSGVsbG9S",
            "ZXF1ZXN0Gg8uZ3JlZXQuRmlsZUxpc3QSMAoIQ2xlYXJEaXISEy5ncmVldC5I",
            "ZWxsb1JlcXVlc3QaDy5ncmVldC5DbGVhckFsbEIOqgILZ1JQQy5TZXJ2ZXJi",
            "BnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::gRPC.Server.FileBuffer), global::gRPC.Server.FileBuffer.Parser, new[]{ "Filename", "Info" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::gRPC.Server.IsWritten), global::gRPC.Server.IsWritten.Parser, new[]{ "IsWrittenInServer" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::gRPC.Server.WhatFile), global::gRPC.Server.WhatFile.Parser, new[]{ "Filename" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::gRPC.Server.UserList), global::gRPC.Server.UserList.Parser, new[]{ "Users" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::gRPC.Server.FileList), global::gRPC.Server.FileList.Parser, new[]{ "Files" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::gRPC.Server.ClearAll), global::gRPC.Server.ClearAll.Parser, new[]{ "IsClear" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::gRPC.Server.HelloRequest), global::gRPC.Server.HelloRequest.Parser, new[]{ "Name" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::gRPC.Server.HelloReply), global::gRPC.Server.HelloReply.Parser, new[]{ "Message" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class FileBuffer : pb::IMessage<FileBuffer> {
    private static readonly pb::MessageParser<FileBuffer> _parser = new pb::MessageParser<FileBuffer>(() => new FileBuffer());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<FileBuffer> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::gRPC.Server.GreetReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public FileBuffer() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public FileBuffer(FileBuffer other) : this() {
      filename_ = other.filename_;
      info_ = other.info_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public FileBuffer Clone() {
      return new FileBuffer(this);
    }

    /// <summary>Field number for the "filename" field.</summary>
    public const int FilenameFieldNumber = 1;
    private string filename_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Filename {
      get { return filename_; }
      set {
        filename_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "info" field.</summary>
    public const int InfoFieldNumber = 2;
    private pb::ByteString info_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString Info {
      get { return info_; }
      set {
        info_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as FileBuffer);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(FileBuffer other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Filename != other.Filename) return false;
      if (Info != other.Info) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Filename.Length != 0) hash ^= Filename.GetHashCode();
      if (Info.Length != 0) hash ^= Info.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Filename.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Filename);
      }
      if (Info.Length != 0) {
        output.WriteRawTag(18);
        output.WriteBytes(Info);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Filename.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Filename);
      }
      if (Info.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Info);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(FileBuffer other) {
      if (other == null) {
        return;
      }
      if (other.Filename.Length != 0) {
        Filename = other.Filename;
      }
      if (other.Info.Length != 0) {
        Info = other.Info;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Filename = input.ReadString();
            break;
          }
          case 18: {
            Info = input.ReadBytes();
            break;
          }
        }
      }
    }

  }

  public sealed partial class IsWritten : pb::IMessage<IsWritten> {
    private static readonly pb::MessageParser<IsWritten> _parser = new pb::MessageParser<IsWritten>(() => new IsWritten());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<IsWritten> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::gRPC.Server.GreetReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public IsWritten() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public IsWritten(IsWritten other) : this() {
      isWrittenInServer_ = other.isWrittenInServer_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public IsWritten Clone() {
      return new IsWritten(this);
    }

    /// <summary>Field number for the "isWrittenInServer" field.</summary>
    public const int IsWrittenInServerFieldNumber = 1;
    private bool isWrittenInServer_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool IsWrittenInServer {
      get { return isWrittenInServer_; }
      set {
        isWrittenInServer_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as IsWritten);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(IsWritten other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (IsWrittenInServer != other.IsWrittenInServer) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (IsWrittenInServer != false) hash ^= IsWrittenInServer.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (IsWrittenInServer != false) {
        output.WriteRawTag(8);
        output.WriteBool(IsWrittenInServer);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (IsWrittenInServer != false) {
        size += 1 + 1;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(IsWritten other) {
      if (other == null) {
        return;
      }
      if (other.IsWrittenInServer != false) {
        IsWrittenInServer = other.IsWrittenInServer;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            IsWrittenInServer = input.ReadBool();
            break;
          }
        }
      }
    }

  }

  public sealed partial class WhatFile : pb::IMessage<WhatFile> {
    private static readonly pb::MessageParser<WhatFile> _parser = new pb::MessageParser<WhatFile>(() => new WhatFile());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<WhatFile> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::gRPC.Server.GreetReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public WhatFile() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public WhatFile(WhatFile other) : this() {
      filename_ = other.filename_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public WhatFile Clone() {
      return new WhatFile(this);
    }

    /// <summary>Field number for the "filename" field.</summary>
    public const int FilenameFieldNumber = 1;
    private string filename_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Filename {
      get { return filename_; }
      set {
        filename_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as WhatFile);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(WhatFile other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Filename != other.Filename) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Filename.Length != 0) hash ^= Filename.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Filename.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Filename);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Filename.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Filename);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(WhatFile other) {
      if (other == null) {
        return;
      }
      if (other.Filename.Length != 0) {
        Filename = other.Filename;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Filename = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class UserList : pb::IMessage<UserList> {
    private static readonly pb::MessageParser<UserList> _parser = new pb::MessageParser<UserList>(() => new UserList());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<UserList> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::gRPC.Server.GreetReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UserList() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UserList(UserList other) : this() {
      users_ = other.users_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UserList Clone() {
      return new UserList(this);
    }

    /// <summary>Field number for the "users" field.</summary>
    public const int UsersFieldNumber = 1;
    private string users_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Users {
      get { return users_; }
      set {
        users_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as UserList);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(UserList other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Users != other.Users) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Users.Length != 0) hash ^= Users.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Users.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Users);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Users.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Users);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(UserList other) {
      if (other == null) {
        return;
      }
      if (other.Users.Length != 0) {
        Users = other.Users;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Users = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class FileList : pb::IMessage<FileList> {
    private static readonly pb::MessageParser<FileList> _parser = new pb::MessageParser<FileList>(() => new FileList());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<FileList> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::gRPC.Server.GreetReflection.Descriptor.MessageTypes[4]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public FileList() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public FileList(FileList other) : this() {
      files_ = other.files_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public FileList Clone() {
      return new FileList(this);
    }

    /// <summary>Field number for the "files" field.</summary>
    public const int FilesFieldNumber = 1;
    private string files_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Files {
      get { return files_; }
      set {
        files_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as FileList);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(FileList other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Files != other.Files) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Files.Length != 0) hash ^= Files.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Files.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Files);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Files.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Files);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(FileList other) {
      if (other == null) {
        return;
      }
      if (other.Files.Length != 0) {
        Files = other.Files;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Files = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class ClearAll : pb::IMessage<ClearAll> {
    private static readonly pb::MessageParser<ClearAll> _parser = new pb::MessageParser<ClearAll>(() => new ClearAll());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ClearAll> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::gRPC.Server.GreetReflection.Descriptor.MessageTypes[5]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ClearAll() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ClearAll(ClearAll other) : this() {
      isClear_ = other.isClear_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ClearAll Clone() {
      return new ClearAll(this);
    }

    /// <summary>Field number for the "isClear" field.</summary>
    public const int IsClearFieldNumber = 1;
    private bool isClear_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool IsClear {
      get { return isClear_; }
      set {
        isClear_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ClearAll);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ClearAll other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (IsClear != other.IsClear) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (IsClear != false) hash ^= IsClear.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (IsClear != false) {
        output.WriteRawTag(8);
        output.WriteBool(IsClear);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (IsClear != false) {
        size += 1 + 1;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ClearAll other) {
      if (other == null) {
        return;
      }
      if (other.IsClear != false) {
        IsClear = other.IsClear;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            IsClear = input.ReadBool();
            break;
          }
        }
      }
    }

  }

  /// <summary>
  /// The request message containing the user's name.
  /// </summary>
  public sealed partial class HelloRequest : pb::IMessage<HelloRequest> {
    private static readonly pb::MessageParser<HelloRequest> _parser = new pb::MessageParser<HelloRequest>(() => new HelloRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<HelloRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::gRPC.Server.GreetReflection.Descriptor.MessageTypes[6]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public HelloRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public HelloRequest(HelloRequest other) : this() {
      name_ = other.name_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public HelloRequest Clone() {
      return new HelloRequest(this);
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 1;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as HelloRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(HelloRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(HelloRequest other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Name = input.ReadString();
            break;
          }
        }
      }
    }

  }

  /// <summary>
  /// The response message containing the greetings.
  /// </summary>
  public sealed partial class HelloReply : pb::IMessage<HelloReply> {
    private static readonly pb::MessageParser<HelloReply> _parser = new pb::MessageParser<HelloReply>(() => new HelloReply());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<HelloReply> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::gRPC.Server.GreetReflection.Descriptor.MessageTypes[7]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public HelloReply() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public HelloReply(HelloReply other) : this() {
      message_ = other.message_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public HelloReply Clone() {
      return new HelloReply(this);
    }

    /// <summary>Field number for the "message" field.</summary>
    public const int MessageFieldNumber = 1;
    private string message_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Message {
      get { return message_; }
      set {
        message_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as HelloReply);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(HelloReply other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Message != other.Message) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Message.Length != 0) hash ^= Message.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Message.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Message);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Message.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Message);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(HelloReply other) {
      if (other == null) {
        return;
      }
      if (other.Message.Length != 0) {
        Message = other.Message;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Message = input.ReadString();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
