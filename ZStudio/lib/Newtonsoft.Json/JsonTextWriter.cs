#region License
// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
  /// <summary>
  /// Represents a writer that provides a fast, non-cached, forward-only way of generating Json data.
  /// </summary>
	public class JsonTextWriter : JsonWriter
  {
	  /// <summary>
	  /// Represents a writer that provides a fast, non-cached, forward-only way of generating Json data.
	  /// </summary>
	  protected TextWriter m_writer;	
    private Base64Encoder _base64Encoder;
    private char _indentChar;
    private int _indentation;
    private char _quoteChar;
    private bool _quoteName;

    private Base64Encoder Base64Encoder
    {
      get
      {
        if (_base64Encoder == null)
          _base64Encoder = new Base64Encoder(m_writer);

        return _base64Encoder;
      }
    }

    /// <summary>
    /// Gets or sets how many IndentChars to write for each level in the hierarchy when <see cref="Formatting"/> is set to <c>Formatting.Indented</c>.
    /// </summary>
    public int Indentation
    {
      get { return _indentation; }
      set
      {
        if (value < 0)
          throw new ArgumentException("Indentation value must be greater than 0.");

        _indentation = value;
      }
    }

    /// <summary>
    /// Gets or sets which character to use to quote attribute values.
    /// </summary>
    public char QuoteChar
    {
      get { return _quoteChar; }
      set
      {
        if (value != '"' && value != '\'')
          throw new ArgumentException(@"Invalid JavaScript string quote character. Valid quote characters are ' and "".");

        _quoteChar = value;
      }
    }

    /// <summary>
    /// Gets or sets which character to use for indenting when <see cref="Formatting"/> is set to <c>Formatting.Indented</c>.
    /// </summary>
    public char IndentChar
    {
      get { return _indentChar; }
      set { _indentChar = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether object names will be surrounded with quotes.
    /// </summary>
    public bool QuoteName
    {
      get { return _quoteName; }
      set { _quoteName = value; }
    }

    /// <summary>
    /// Creates an instance of the <c>JsonWriter</c> class using the specified <see cref="TextWriter"/>. 
    /// </summary>
    /// <param name="textWriter">The <c>TextWriter</c> to write to.</param>
    public JsonTextWriter(TextWriter textWriter)
    {
      if (textWriter == null)
        throw new ArgumentNullException("textWriter");

      m_writer = textWriter;
      _quoteChar = '"';
      _quoteName = true;
      _indentChar = ' ';
      _indentation = 2;
    }
	/// <summary>
	/// Converts contents of underlying text writer to the string
	/// </summary>
    /// <param></param>
	public override string ToString()
	{
		return m_writer.ToString();
	}
    /// <summary>
    /// Flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.
    /// </summary>
    public override void Flush()
    {
      m_writer.Flush();
    }

    /// <summary>
    /// Closes this stream and the underlying stream.
    /// </summary>
    public override void Close()
    {
      base.Close();

      if (CloseOutput && m_writer != null)
        m_writer.Close();
    }

    /// <summary>
    /// Writes the beginning of a Json object.
    /// </summary>
    public override void WriteStartObject()
    {
      base.WriteStartObject();

      m_writer.Write("{");
    }

    /// <summary>
    /// Writes the beginning of a Json array.
    /// </summary>
    public override void WriteStartArray()
    {
      base.WriteStartArray();

      m_writer.Write("[");
    }

    /// <summary>
    /// Writes the start of a constructor with the given name.
    /// </summary>
    /// <param name="name">The name of the constructor.</param>
    public override void WriteStartConstructor(string name)
    {
      base.WriteStartConstructor(name);

      m_writer.Write("new ");
      m_writer.Write(name);
      m_writer.Write("(");
    }

    /// <summary>
    /// Writes the specified end token.
    /// </summary>
    /// <param name="token">The end token to write.</param>
    protected override void WriteEnd(JsonToken token)
    {
      switch (token)
      {
        case JsonToken.EndObject:
          m_writer.Write("}");
          break;
        case JsonToken.EndArray:
          m_writer.Write("]");
          break;
        case JsonToken.EndConstructor:
          m_writer.Write(")");
          break;
        default:
          throw new JsonWriterException("Invalid JsonToken: " + token);
      }
    }

    /// <summary>
    /// Writes the property name of a name/value pair on a Json object.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    public override void WritePropertyName(string name)
    {
      base.WritePropertyName(name);

      JavaScriptUtils.WriteEscapedJavaScriptString(m_writer, name, _quoteChar, _quoteName);

      m_writer.Write(':');
    }

    /// <summary>
    /// Writes indent characters.
    /// </summary>
    protected override void WriteIndent()
    {
      if (Formatting == Formatting.Indented)
      {
        m_writer.Write(Environment.NewLine);

        // levels of indentation multiplied by the indent count
        int currentIndentCount = Top * _indentation;

        for (int i = 0; i < currentIndentCount; i++)
        {
          m_writer.Write(_indentChar);
        }
      }
    }

    /// <summary>
    /// Writes the JSON value delimiter.
    /// </summary>
    protected override void WriteValueDelimiter()
    {
      m_writer.Write(',');
    }

    /// <summary>
    /// Writes an indent space.
    /// </summary>
    protected override void WriteIndentSpace()
    {
      m_writer.Write(' ');
    }

    private void WriteValueInternal(string value, JsonToken token)
    {
      m_writer.Write(value);
    }

    #region WriteValue methods
    /// <summary>
    /// Writes a null value.
    /// </summary>
    public override void WriteNull()
    {
      base.WriteNull();
      WriteValueInternal(JsonConvert.Null, JsonToken.Null);
    }

    /// <summary>
    /// Writes an undefined value.
    /// </summary>
    public override void WriteUndefined()
    {
      base.WriteUndefined();
      WriteValueInternal(JsonConvert.Undefined, JsonToken.Undefined);
    }

    /// <summary>
    /// Writes raw JSON.
    /// </summary>
    /// <param name="json">The raw JSON to write.</param>
    public override void WriteRaw(string json)
    {
      base.WriteRaw(json);

      m_writer.Write(json);
    }

    /// <summary>
    /// Writes a <see cref="String"/> value.
    /// </summary>
    /// <param name="value">The <see cref="String"/> value to write.</param>
    public override void WriteValue(string value)
    {
      base.WriteValue(value);
      if (value == null)
        WriteValueInternal(JsonConvert.Null, JsonToken.Null);
      else
        JavaScriptUtils.WriteEscapedJavaScriptString(m_writer, value, _quoteChar, true);
    }

    /// <summary>
    /// Writes a <see cref="Int32"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Int32"/> value to write.</param>
    public override void WriteValue(int value)
    {
      base.WriteValue(value);
      WriteValueInternal(JsonConvert.ToString(value), JsonToken.Integer);
    }

    /// <summary>
    /// Writes a <see cref="UInt32"/> value.
    /// </summary>
    /// <param name="value">The <see cref="UInt32"/> value to write.</param>
    [CLSCompliant(false)]
    public override void WriteValue(uint value)
    {
      base.WriteValue(value);
      WriteValueInternal(JsonConvert.ToString(value), JsonToken.Integer);
    }

    /// <summary>
    /// Writes a <see cref="Int64"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Int64"/> value to write.</param>
    public override void WriteValue(long value)
    {
      base.WriteValue(value);
      WriteValueInternal(JsonConvert.ToString(value), JsonToken.Integer);
    }

    /// <summary>
    /// Writes a <see cref="UInt64"/> value.
    /// </summary>
    /// <param name="value">The <see cref="UInt64"/> value to write.</param>
    [CLSCompliant(false)]
    public override void WriteValue(ulong value)
    {
      base.WriteValue(value);
      WriteValueInternal(JsonConvert.ToString(value), JsonToken.Integer);
    }

    /// <summary>
    /// Writes a <see cref="Single"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Single"/> value to write.</param>
    public override void WriteValue(float value)
    {
      base.WriteValue(value);
      WriteValueInternal(JsonConvert.ToString(value), JsonToken.Float);
    }

    /// <summary>
    /// Writes a <see cref="Double"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Double"/> value to write.</param>
    public override void WriteValue(double value)
    {
      base.WriteValue(value);
      WriteValueInternal(JsonConvert.ToString(value), JsonToken.Float);
    }

    /// <summary>
    /// Writes a <see cref="Boolean"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Boolean"/> value to write.</param>
    public override void WriteValue(bool value)
    {
      base.WriteValue(value);
      WriteValueInternal(JsonConvert.ToString(value), JsonToken.Boolean);
    }

    /// <summary>
    /// Writes a <see cref="Int16"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Int16"/> value to write.</param>
    public override void WriteValue(short value)
    {
      base.WriteValue(value);
      WriteValueInternal(JsonConvert.ToString(value), JsonToken.Integer);
    }

    /// <summary>
    /// Writes a <see cref="UInt16"/> value.
    /// </summary>
    /// <param name="value">The <see cref="UInt16"/> value to write.</param>
    [CLSCompliant(false)]
    public override void WriteValue(ushort value)
    {
      base.WriteValue(value);
      WriteValueInternal(JsonConvert.ToString(value), JsonToken.Integer);
    }

    /// <summary>
    /// Writes a <see cref="Char"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Char"/> value to write.</param>
    public override void WriteValue(char value)
    {
      base.WriteValue(value);
      WriteValueInternal(JsonConvert.ToString(value), JsonToken.Integer);
    }

    /// <summary>
    /// Writes a <see cref="Byte"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Byte"/> value to write.</param>
    public override void WriteValue(byte value)
    {
      base.WriteValue(value);
      WriteValueInternal(JsonConvert.ToString(value), JsonToken.Integer);
    }

    /// <summary>
    /// Writes a <see cref="SByte"/> value.
    /// </summary>
    /// <param name="value">The <see cref="SByte"/> value to write.</param>
    [CLSCompliant(false)]
    public override void WriteValue(sbyte value)
    {
      base.WriteValue(value);
      WriteValueInternal(JsonConvert.ToString(value), JsonToken.Integer);
    }

    /// <summary>
    /// Writes a <see cref="Decimal"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Decimal"/> value to write.</param>
    public override void WriteValue(decimal value)
    {
      base.WriteValue(value);
      WriteValueInternal(JsonConvert.ToString(value), JsonToken.Float);
    }

    /// <summary>
    /// Writes a <see cref="DateTime"/> value.
    /// </summary>
    /// <param name="value">The <see cref="DateTime"/> value to write.</param>
    public override void WriteValue(DateTime value)
    {
      base.WriteValue(value);
      JsonConvert.WriteDateTimeString(m_writer, value);
    }

    /// <summary>
    /// Writes a <see cref="T:Byte[]"/> value.
    /// </summary>
    /// <param name="value">The <see cref="T:Byte[]"/> value to write.</param>
    public override void WriteValue(byte[] value)
    {
      base.WriteValue(value);

      if (value != null)
      {
        m_writer.Write(_quoteChar);
        Base64Encoder.Encode(value, 0, value.Length);
        Base64Encoder.Flush();
        m_writer.Write(_quoteChar);
      }
    }

#if !PocketPC && !NET20
    /// <summary>
    /// Writes a <see cref="DateTimeOffset"/> value.
    /// </summary>
    /// <param name="value">The <see cref="DateTimeOffset"/> value to write.</param>
    public override void WriteValue(DateTimeOffset value)
    {
      base.WriteValue(value);
      WriteValueInternal(JsonConvert.ToString(value), JsonToken.Date);
    }
#endif

    /// <summary>
    /// Writes a <see cref="Guid"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Guid"/> value to write.</param>
    public override void WriteValue(Guid value)
    {
      base.WriteValue(value);
      WriteValueInternal(JsonConvert.ToString(value), JsonToken.String);
    }

    /// <summary>
    /// Writes a <see cref="TimeSpan"/> value.
    /// </summary>
    /// <param name="value">The <see cref="TimeSpan"/> value to write.</param>
    public override void WriteValue(TimeSpan value)
    {
      base.WriteValue(value);
      WriteValueInternal(JsonConvert.ToString(value), JsonToken.Date);
    }

    /// <summary>
    /// Writes a <see cref="Uri"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Uri"/> value to write.</param>
    public override void WriteValue(Uri value)
    {
      base.WriteValue(value);
      WriteValueInternal(JsonConvert.ToString(value), JsonToken.Date);
    }
    #endregion

    /// <summary>
    /// Writes out a comment <code>/*...*/</code> containing the specified text. 
    /// </summary>
    /// <param name="text">Text to place inside the comment.</param>
    public override void WriteComment(string text)
    {
      base.WriteComment(text);

      m_writer.Write("/*");
      m_writer.Write(text);
      m_writer.Write("*/");
    }

    /// <summary>
    /// Writes out the given white space.
    /// </summary>
    /// <param name="ws">The string of white space characters.</param>
    public override void WriteWhitespace(string ws)
    {
      base.WriteWhitespace(ws);

      m_writer.Write(ws);
    }
  }
}